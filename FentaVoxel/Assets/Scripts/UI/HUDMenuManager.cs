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

    [Header("Manager de recursos")]
    [SerializeField] private ResourcesManager resourcesManager;

    void Update() {
        txtWood.text = ((int)(resourcesManager._wood)).ToString();
        txtWater.text = ((int)(resourcesManager._water)).ToString();
        txtMetal.text = ((int)(resourcesManager._iron)).ToString();
        
    }
}
