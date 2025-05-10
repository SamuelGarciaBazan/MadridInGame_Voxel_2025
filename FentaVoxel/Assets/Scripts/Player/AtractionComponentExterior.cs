using UnityEngine;


/*

Para coger los pickeable items, el player usa 2 colliders, el exterior se encarga de 
mover los pickeableItems hacia el player, y el interior de detectar la colision y sumarseLos recursos

Ambos son triggers con la layer del pickeableItem
 
*/
public class AtractionComponentExterior : MonoBehaviour
{

    [SerializeField]
    LayerMask _pickeableItemLayer;

    private void OnTriggerEnter(Collider other)
    {

        //print(other.gameObject + "layer" +other.gameObject.layer + "pick layer " + _pickeableItemLayer.value);

        if (((1 << other.gameObject.layer) & _pickeableItemLayer.value) != 0)
        {
            //print("atraeerrr");
            //mandar atraer hacia el player
            other.GetComponentInChildren<PickeableItemResource>().followTransform(transform);
        }
    }


}
