using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

/// <summary>
/// Actualiza los indicadores de recursos (Madera, Agua, Metal) en el HUD comprobando en Update.
/// </summary>
public class HUDMenuManager : MonoBehaviour
{
    [Header("Textos HUD")]
    [SerializeField] private Text txtWood;
    [SerializeField] private Text txtWater;
    [SerializeField] private Text txtMetal;
    [SerializeField] private Text txtRobotsAgua;
    [SerializeField] private Text txtRobotsElectricidad;
    [SerializeField] private Sprite spriteRobotsAgua;
    [SerializeField] private Sprite spriteRobotsElectricidad;

    [Header("Manager de recursos")]
    [SerializeField] private ResourcesManager resourcesManager;
    [SerializeField] private RobotManager robotManager;
    [SerializeField] private SpendingResourceManager spendingResourceManager;

    void Update() {
        txtWood.text = ((int)(resourcesManager._wood)).ToString();
        txtWater.text = ((int)(resourcesManager._water)).ToString();
        txtMetal.text = ((int)(resourcesManager._iron)).ToString();

        txtRobotsAgua.text = ((int)(robotManager.WorkingRobotsAmount())).ToString();
        txtRobotsElectricidad.text = ((int)(robotManager.WorkingRobotsAmount())).ToString();

    }
}
