using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Gestiona el HUD de robots: muestra contadores y gestiona botones para asignar y crear robots.
/// </summary>
public class RobotsMenuManager : MonoBehaviour
{
    [Header("Indicadores de robots")]
    [SerializeField] private Text txtFreeRobots;
    [SerializeField] private Text txtWoodRobots;
    [SerializeField] private Text txtWaterRobots;
    [SerializeField] private Text txtIronRobots;

    [Header("Botones de asignación WOOD")]
    [SerializeField] private Button btnWoodPlus;
    [SerializeField] private Button btnWoodMinus;

    [Header("Botones de asignación WATER")]
    [SerializeField] private Button btnWaterPlus;
    [SerializeField] private Button btnWaterMinus;

    [Header("Botones de asignación IRON")]
    [SerializeField] private Button btnIronPlus;
    [SerializeField] private Button btnIronMinus;

    [Header("Botón de Spawn Robot")]
    [SerializeField] private Button btnSpawnRobot;

    [Header("Managers")]
    [SerializeField] private RobotManager robotManager;
    [SerializeField] private ResourcesManager resourcesManager;

    private void Awake() {
        // Suscribir listeners a botones
        btnWoodPlus.onClick.AddListener(() => ChangeAssignment(ResourcesManager.ResourcesType.WOOD, +1));
        btnWoodMinus.onClick.AddListener(() => ChangeAssignment(ResourcesManager.ResourcesType.WOOD, -1));

        btnWaterPlus.onClick.AddListener(() => ChangeAssignment(ResourcesManager.ResourcesType.WATER, +1));
        btnWaterMinus.onClick.AddListener(() => ChangeAssignment(ResourcesManager.ResourcesType.WATER, -1));

        btnIronPlus.onClick.AddListener(() => ChangeAssignment(ResourcesManager.ResourcesType.IRON, +1));
        btnIronMinus.onClick.AddListener(() => ChangeAssignment(ResourcesManager.ResourcesType.IRON, -1));

        btnSpawnRobot.onClick.AddListener(OnSpawnRobot);
    }

    private void OnEnable() {
        UpdateHUD();
        robotManager.OnRobotCountsChanged += UpdateHUD;
    }

    private void OnDisable() {
        robotManager.OnRobotCountsChanged -= UpdateHUD;
    }

    /// <summary>
    /// Asigna o desasigna un robot de un recurso.
    /// </summary>
    private void ChangeAssignment(ResourcesManager.ResourcesType type, int delta) {
        if (robotManager.AssignRobots(type, delta)) {
            UpdateHUD();
        } else {
            Debug.LogWarning($"No se pudo cambiar asignación de robots para {type} (delta {delta})");
        }
    }

    /// <summary>
    /// Instancia un nuevo robot y lo añade a libres.
    /// </summary>
    private void OnSpawnRobot() {
        // Opcional: puedes especificar posición de spawn
        robotManager.CreateRobot();
        UpdateHUD();
    }

    /// <summary>
    /// Actualiza los textos e interactuabilidad de botones.
    /// </summary>
    private void UpdateHUD() {
        // Actualizar textos
        txtFreeRobots.text = robotManager.FreeRobots.ToString();
        txtWoodRobots.text = robotManager.GetRobots(ResourcesManager.ResourcesType.WOOD).ToString();
        txtWaterRobots.text = robotManager.GetRobots(ResourcesManager.ResourcesType.WATER).ToString();
        txtIronRobots.text = robotManager.GetRobots(ResourcesManager.ResourcesType.IRON).ToString();

        // Habilitar/deshabilitar botones según disponibilidad
        btnWoodPlus.interactable = robotManager.FreeRobots > 0;
        btnWoodMinus.interactable = robotManager.GetRobots(ResourcesManager.ResourcesType.WOOD) > 0;

        btnWaterPlus.interactable = robotManager.FreeRobots > 0;
        btnWaterMinus.interactable = robotManager.GetRobots(ResourcesManager.ResourcesType.WATER) > 0;

        btnIronPlus.interactable = robotManager.FreeRobots > 0;
        btnIronMinus.interactable = robotManager.GetRobots(ResourcesManager.ResourcesType.IRON) > 0;

        // El botón de spawn siempre está activo (o puedes condicionar con recursos)
        btnSpawnRobot.interactable = true;
    }
}
