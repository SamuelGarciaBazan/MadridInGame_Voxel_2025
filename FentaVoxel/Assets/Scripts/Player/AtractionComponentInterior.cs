using UnityEngine;



/*

Para coger los pickeable items, el player usa 2 colliders, el exterior se encarga de 
mover los pickeableItems hacia el player, y el interior de detectar la colision y sumarseLos recursos

Ambos son triggers con la layer del pickeableItem
 
*/
public class AtractionComponentInterior : MonoBehaviour
{
    [SerializeField]
    LayerMask _pickeableItemLayer;


    ResourcesManager _resourcesManager;


    private void Start()
    {
        _resourcesManager = GameManager.getInstance().GetComponent<ResourcesManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //print("interior enter, gameObject "+  other.gameObject.name);   

        if ( ((1 << other.gameObject.layer) & _pickeableItemLayer.value) != 0)
        {
            //print("dentroooo");
            //sumar puntos y destruir objet
            float resourceAmount = other.GetComponentInChildren<PickeableItemResource>().getResourceAmount();

            ResourcesManager.ResourcesType type = other.GetComponent<PickeableItemResource>().getResourcesType();

            _resourcesManager.addResources(type, resourceAmount);

            Destroy(other.gameObject.transform.parent.gameObject);
        }
    }
}
