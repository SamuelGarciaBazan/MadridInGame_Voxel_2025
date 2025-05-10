using System.Collections.Generic;
using System.Xml;
using UnityEngine;


/*
Este componente es el que usan el player y los robots para craftear, cada uno guarda la info del ataque hacia
cada tipo de recurso    
 
 */
public class WorkerComponent : MonoBehaviour
{

    [System.Serializable]
    public struct ResourceData
    {
        public float damage;
        public float atackRate;
        public LayerMask layer;
        public GameObject pickableItemPrefab;

    }

    //IMPORTANTE: la lista debe seguir el orden de los nombres de los recursos
    [SerializeField]
    List<ResourceData> _resourcesData;


    //target
    ResourceComponente _target = null;


    float _elapsedTime = 0;

    float _resourceCount = 0;


    //changeTarget
    public void setTarget(Transform newTarget)
    {
        if (newTarget.GetComponent<ResourceComponente>() == null) {

            Debug.Log("error intententando insertar un target que no tiene el componente de resourceComponente");
        
        }

        if(_target == null){
            _target = newTarget.GetComponent<ResourceComponente>();
        }
        else if ( newTarget == null)
        {
            //drop
            dropItem();
        }
        else if(_target.GetResourcesType() != newTarget.GetComponent<ResourceComponente>().GetResourcesType())
        {
            //drop
            dropItem();
        }
    }

    public ResourceComponente getTarget()
    {
        return _target;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        work();
    }

    void work()
    {
        if (_target != null)
        {
            _elapsedTime += Time.deltaTime;

            if(_elapsedTime > getResourceData(_target.GetResourcesType()).atackRate)
            {
                _elapsedTime -= getResourceData(_target.GetResourcesType()).atackRate;
            }
        }
    }

    ResourceData getResourceData(ResourcesManager.ResourcesType resourcesType)
    {
        return _resourcesData[(int)(resourcesType)];
    }
    void dropItem()
    {
        Instantiate(getResourceData(_target.GetResourcesType()).pickableItemPrefab);
    }

}
