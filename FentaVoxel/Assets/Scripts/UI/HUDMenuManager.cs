using UnityEngine;
using TMPro;
using Unity.VisualScripting;

/// <summary>
/// Actualiza los indicadores de recursos (Madera, Agua, Metal) en el HUD comprobando en Update.
/// </summary>
public class HUDMenuManager : MonoBehaviour
{
    [Header("Textos HUD")]
    [SerializeField] private TextMeshProUGUI txtWood;
    [SerializeField] private TextMeshProUGUI txtWater;
    [SerializeField] private TextMeshProUGUI txtMetal;

    [Header("Manager de recursos")]
    [SerializeField] private ResourcesManager resourcesManager;

    void Update() {
        txtWood.text = resourcesManager._wood.ToString();
        txtWater.text = resourcesManager._water.ToString();
        txtMetal.text = resourcesManager._iron.ToString();
        
    }
}
