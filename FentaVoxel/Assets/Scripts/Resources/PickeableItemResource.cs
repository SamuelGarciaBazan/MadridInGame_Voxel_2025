using UnityEngine;


/*

este componente lo llevan todos los pickeable items resources, con informacion de 
su tipo y cantidad
 
*/
public class PickeableItemResource : MonoBehaviour
{
    [SerializeField]    
    ResourcesManager.ResourcesType _resourceType;

    [SerializeField]
    float _resourceAmount;


    public void setResourceAmount(float amount)
    {
        _resourceAmount = amount;
    }
}
