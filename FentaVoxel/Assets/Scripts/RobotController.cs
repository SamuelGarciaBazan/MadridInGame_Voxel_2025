using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


/*
Esta clase controla el funcionamiento de un robot, teniendo en cuenta 
su estado actual , mandando sobre su comportamiento de movimiento y de ejecucion de scripts
 
*/
public class RobotController : MonoBehaviour
{
    [SerializeField]
    RobotManager.RobotResourceTarget _currentResourceTarget;

    [SerializeField]
    float _searchTargetRadious;

    [SerializeField]
    float _umbralDistance;


    RobotManager _robotManager;

    NavMeshAgent _navMeshAgent;

    Transform _objectTarget;

    enum RobotState
    {
        DESACTIVE,
        SEARCH_TARGET,
        GO_TO_TARGET,
        WORKING //obteniendo el recurso


    }

    [SerializeField]
    RobotState _currentState;


    public void setResourceTarget(RobotManager.RobotResourceTarget newTarget) { 


    }

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();   
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _robotManager = GameManager.getInstance().GetComponent<RobotManager>();
    }

    // Update is called once per frame
    void Update()
    {
        updateStateMachine();

    }

    void updateStateMachine()
    {

        switch (_currentState)
        {
            case RobotState.DESACTIVE:
                {
                    return;
                }
                break;

            case RobotState.SEARCH_TARGET:
                {
                    _objectTarget = searchNearestTarget();

                    if(_objectTarget != null)
                    {
                        _navMeshAgent.SetDestination(_objectTarget.position);
                        _currentState = RobotState.GO_TO_TARGET;
                    }
                    else
                    {
                        Debug.Log("no se encontro objetivo de la layer indicada en el radio de busqueda");
                        //posicion aleatoria en el rango?
                    }
                }
                break;

            case RobotState.GO_TO_TARGET:
                {
                    if (_objectTarget == null)
                    {
                        _currentState = RobotState.SEARCH_TARGET;

                    }
                    else if (_navMeshAgent.remainingDistance < _umbralDistance)
                    {
                        _currentState = RobotState.WORKING;
                        _navMeshAgent.SetDestination(transform.position);//para que deje de moverse
                    }
                }
                break;

            case RobotState.WORKING:
                {

                }
                break;
        }
    }

    Transform searchNearestTarget()
    {
        Transform nearest = null;

        float minDistance = Mathf.Infinity;

        Collider[] hits = Physics.OverlapSphere(transform.position, _searchTargetRadious, getCurrentLayer());

        foreach (var hit in hits)
        {
            float distance = Vector3.Distance(hit.transform.position, transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = hit.transform;
            } 
        }

        return nearest;
    }

    LayerMask getCurrentLayer()
    {
        if (_currentResourceTarget == RobotManager.RobotResourceTarget.NONE)
        {
            Debug.Log("error intententando obtener la layer cuando el target es NONE");
        }

        if (_currentResourceTarget == RobotManager.RobotResourceTarget.GET_WOOD)
        {
            return _robotManager._woodLayer;
        }
        else if (_currentResourceTarget == RobotManager.RobotResourceTarget.GET_WATER)
        {
            return _robotManager._waterLayer;
        }
        else if (_currentResourceTarget == RobotManager.RobotResourceTarget.GET_IRON)
        {
            return _robotManager._ironLayer;
        }
        else //else cooper
        {
            return _robotManager._copperLayer;
        }
    }


}
