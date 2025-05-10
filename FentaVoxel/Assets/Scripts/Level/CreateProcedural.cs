using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.AI.Navigation;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class CreateProcedural : MonoBehaviour
{

    [SerializeField] private GameObject _resource;
    [SerializeField] private string _layer;

    [SerializeField] private int _resourceNumberX;
    [SerializeField] private int _resourceNumberY;
    [SerializeField] private float _minOffset;
    [SerializeField] private float _maxOffset;
    [SerializeField] private float _percerntAmount;



    public NavMeshSurface surface;
    void Start()
    {
        GenerateFromFile();
    }

    void GenerateFromFile()
    {
        int _numResources = _resourceNumberX * _resourceNumberY;

        for (int i = 0; i < _numResources; ++i)
        {

            float offsetX = Random.Range(_minOffset, _maxOffset);

            float rateInstantiation = Random.Range(0.0f, 1.0f);

            int column = i % _resourceNumberX;
            int row = i / _resourceNumberX;

            if(rateInstantiation < _percerntAmount)
            {
                Vector3 position = new Vector3(transform.position.x - column * offsetX, transform.position.y, transform.position.z - row * offsetX);

                if (Physics.OverlapSphere(position, 2, LayerMask.GetMask(_layer)).Length == 0){

                    Instantiate(_resource, position, Quaternion.identity);

                }

                
            } 
            
        }



        reBakeNavMesh();
    }

    void reBakeNavMesh()
    {
        if (surface != null) { 
            surface.BuildNavMesh();
        }
    }
 
}
