using System.Collections.Generic;
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
        public LayerMask layer;

    }

    [SerializeField]
    List<ResourceData> _resourcesData;

    //target


    //farm

    //onChangeTarget

    //changeTarget


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
