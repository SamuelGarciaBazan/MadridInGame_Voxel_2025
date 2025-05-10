using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class CreateProcedural : MonoBehaviour
{

    [SerializeField] private GameObject _resource;

    [SerializeField] private TextAsset _outlineText;

    [SerializeField] private int _resourceNumberX;
    [SerializeField] private int _resourceNumberY;
    [SerializeField] private float _minOffset;
    [SerializeField] private float _maxOffset;
    [SerializeField] private float _percerntAmount;

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

            Debug.Log(rateInstantiation);

            int column = i % _resourceNumberX;
            int row = i / _resourceNumberX;

            if(rateInstantiation < _percerntAmount)
            {
                Instantiate(_resource, new Vector3(transform.position.x - column * offsetX, transform.position.y, transform.position.z - row * offsetX), Quaternion.identity);
            } 
            
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
