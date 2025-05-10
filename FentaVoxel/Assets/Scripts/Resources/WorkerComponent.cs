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

    [SerializeField]
    private float _minDropDistance = 1;

    [SerializeField]
    private float _maxDropDistance = 4;

    //target
    ResourceComponente _target = null;


    float _elapsedTime = 0;

    float _resourceCount = 0;

    public int _indexResourceOcupped = -1;

    //changeTarget
    public void setTarget(Transform newTarget)
    {
        /*
        if(newTarget == null)
        {
            Debug.Log("error intententando insertar un target null");

            _target = null;
            return;
        }
        */

        if (newTarget.GetComponent<ResourceComponente>() == null) {

            Debug.Log("error intententando insertar un target que no tiene el componente de resourceComponente");
        
            _target=null;

            return;
        }

        if(_target == null){
            _target = newTarget.GetComponent<ResourceComponente>();
        }
        else if ( newTarget == null)
        {
            //drop
            dropItem();


            //marcar como desocupado el slot
            _target._positionsOcupeds[_indexResourceOcupped] = false;

            _target = null;
        }
        else if(_target.GetResourcesType() != newTarget.GetComponent<ResourceComponente>().GetResourcesType())
        {
            //drop
            dropItem();

            //marcar como desocupado el slot
            _target._positionsOcupeds[_indexResourceOcupped] = false;

            _target = null;
        }
    }

    public ResourceComponente getTarget()
    {
        return _target;
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
            //actualizar timer
            _elapsedTime += Time.deltaTime;

            if(_elapsedTime > getResourceData(_target.GetResourcesType()).atackRate)
            {
                //restar timer
                _elapsedTime -= getResourceData(_target.GetResourcesType()).atackRate;


                //sumar recurso
                _resourceCount += getResourceData(_target.GetResourcesType()).damage;

                //restar recurso del otro objeto
                _target.subtractResources(getResourceData(_target.GetResourcesType()).damage);



                if(_resourceCount >= _target.getPackageDropRate())
                {
                    dropItem();
                }

            }
        }
    }

    ResourceData getResourceData(ResourcesManager.ResourcesType resourcesType)
    {
        return _resourcesData[(int)(resourcesType)];
    }
    void dropItem()
    {

        int angleDrop = Random.Range(0, 360);

        float _dropDistance = Random.Range(_minDropDistance, _maxDropDistance);

        Vector3 dropPos = transform.position + 
            (new Vector3(Mathf.Cos(Mathf.Deg2Rad * angleDrop), 0, Mathf.Sin(Mathf.Deg2Rad * angleDrop)) * _dropDistance);

        GameObject pickableItem =  Instantiate(getResourceData(_target.GetResourcesType()).pickableItemPrefab, dropPos, Quaternion.identity);

        float subtractAmount = _resourceCount >=  _target.getPackageDropRate() ? _target.getPackageDropRate() : _resourceCount;

        _resourceCount -= subtractAmount;

        //settear la cantidad de recurso del item que hemos instanciado
        pickableItem.GetComponentInChildren<PickeableItemResource>().setResourceAmount(subtractAmount);

    }

}
