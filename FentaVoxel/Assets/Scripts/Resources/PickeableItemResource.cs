using UnityEngine;
using static UnityEngine.GraphicsBuffer;


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


    [SerializeField]
    float _movementVelocity;

    Transform _target = null;

    public void setResourceAmount(float amount)
    {
        _resourceAmount = amount;
    }

    public float getResourceAmount()
    {
        return _resourceAmount;
    }

    public ResourcesManager.ResourcesType getResourcesType()
    {
        return _resourceType;
    }

    //deberia llamarse solo con el transform del player
    public void followTransform(Transform target)
    {
        _target = target;
    }

    private void Update()
    {
        if (_target != null) {

            transform.parent.position = Vector3.MoveTowards(
            transform.parent.position,
            _target.position,
            _movementVelocity * Time.deltaTime
            );

        }
    }



}
