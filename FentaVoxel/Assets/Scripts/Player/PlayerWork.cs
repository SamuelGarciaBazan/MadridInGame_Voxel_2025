using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWork : MonoBehaviour
{
    [SerializeField]
    WorkerComponent _workerComponent;

    [SerializeField]
    LayerMask _resourcesMask;

    [SerializeField]
    float outLineOnHover;

    [SerializeField]    
    float outLineOffHover;


    float _nearestDistance;

    GameObject _lastNearest;


    private List<GameObject> _hoveredResources = new List<GameObject>();
    private void FixedUpdate()
    {
        //_lastNearest = null;    
        //_nearestDistance = Mathf.Infinity;

        if (_hoveredResources.Count == 0)
        {
            _lastNearest = null;
            return;
        }

        GameObject nearest = null;
        float nearestDist = float.MaxValue;

        foreach (var obj in _hoveredResources)
        {
            if (obj == null) continue;

            float dist = Vector3.Distance(transform.position, obj.transform.position);
            if (dist < nearestDist)
            {
                nearestDist = dist;
                nearest = obj;
            }
        }

        foreach (var obj in _hoveredResources)
        {
            if (obj == null) continue;

            var outline = obj.GetComponentInChildren<Outline>();
            if (outline == null) continue;

            outline.setOutlineWidht(obj == nearest ? outLineOnHover : outLineOffHover);
        }

        _lastNearest = nearest;

        // Limpia la lista para el siguiente frame
        _hoveredResources.Clear();

    }

    private void OnTriggerEnter(Collider other)
    {
        return;

        //si es un recurso
        if ( ((1 << other.gameObject.layer) & _resourcesMask.value) != 0)
        {

            float distance = Vector3.Distance(transform.position, other.transform.position);

            if (distance < _nearestDistance) { 
            
                _nearestDistance = distance;

                if (_lastNearest != null) {
                    
                    var lastOutline = _lastNearest.GetComponentInChildren<Outline>();


                    lastOutline.setOutlineWidht(outLineOffHover);

                }

                _lastNearest = other.gameObject;

                var outline = other.GetComponentInChildren<Outline>();


                outline.setOutlineWidht(outLineOnHover);

            }


 
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //si es un recurso
        if (((1 << other.gameObject.layer) & _resourcesMask.value) != 0)
        {
            if (!_hoveredResources.Contains(other.gameObject))
            {
                _hoveredResources.Add(other.gameObject);
            }
                

        }
    }




    private void OnTriggerExit(Collider other)
    {
        //si es un recurso
        if (((1 << other.gameObject.layer) & _resourcesMask.value) != 0)
        {
            var outline = other.GetComponentInChildren<Outline>();


            outline.setOutlineWidht(outLineOffHover);
        }

    }

    public void Work(InputAction.CallbackContext context)
    {

        if (context.canceled)
        {
            _workerComponent.setTarget(null);
        }
        else
        {
            if (_lastNearest != null) { 
                _workerComponent.setTarget(_lastNearest.transform);
            }
            else
            {
                _workerComponent.setTarget(null);
            }

        }

    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
