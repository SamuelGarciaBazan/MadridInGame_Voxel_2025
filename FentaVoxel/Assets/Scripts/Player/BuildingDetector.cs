using UnityEngine;
using UnityEngine.InputSystem;
using static Edificio;

public class BuildingDetector : MonoBehaviour
{
    public GameObject _robotsMenu;
    public GameObject _estatuaMenu;
    public GameObject _hornoMenu;
    public GameObject _tanqueMenu;


    Edificio _nearestBuilding;

    //TODO: mostra E y outline


    private void OnTriggerEnter(Collider other)
    {
        var edificio = other.GetComponent<Edificio>();
        if(edificio != null)
        {
            _nearestBuilding = edificio;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<Edificio>() != null && other.GetComponent<Edificio>() == _nearestBuilding)
        {
            setPanelActive(_nearestBuilding.tipoEdificio, false);
            _nearestBuilding = null;
        }
    }
  
    public void InteractBuilding(InputAction.CallbackContext context)
    {
        if (context.started) { 

            if (_nearestBuilding != null) {
                setPanelActive(_nearestBuilding.tipoEdificio, !(getCurrentEdificio(_nearestBuilding.tipoEdificio).activeSelf));   
            }
        }
    }


    GameObject getCurrentEdificio(Edificio.TipoEdificio tipoEdificio)
    {
        switch (tipoEdificio)
        {
            case TipoEdificio.ROBOTS_MENU:
                return _robotsMenu;

            case TipoEdificio.ESTATUA_MENU:
                return _estatuaMenu;

            case TipoEdificio.HORNO_MENU:
                return _hornoMenu;

            case TipoEdificio.TANQUE_MENU:
                return _tanqueMenu; ;

            default:
                Debug.LogWarning("Tipo de edificio no reconocido: " + tipoEdificio);
                return null;
        }
    }

    void setPanelActive(Edificio.TipoEdificio tipoEdificio,bool active)
    {
        getCurrentEdificio(tipoEdificio).SetActive(active);
    }


}
