using System.Collections.Generic;
using System.Linq;
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

    WorkerComponent _workerComponent;

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

        if(_currentResourceTarget != newTarget)
        {
            _currentResourceTarget = newTarget;

            if(newTarget == RobotManager.RobotResourceTarget.NONE)
            {
                _currentState = RobotState.DESACTIVE;
            }
            else
            {
                _currentState = RobotState.SEARCH_TARGET;   
            }

            //si nuestro worker estaba trabajando, lo liberamos
            _workerComponent.setTarget(null);
        }

    }

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();   
        _workerComponent = GetComponent<WorkerComponent>();
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
                        //Debug.Log("no se encontro objetivo de la layer indicada en el radio de busqueda");
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
                        _workerComponent.setTarget(_objectTarget);
                    }
                }
                break;

            case RobotState.WORKING:
                {
                    if (_workerComponent.getTarget() == null) { 
                    
                        _currentState = RobotState.SEARCH_TARGET;
                        _workerComponent.setTarget(null);
                    }
                }
                break;
        }
    }

    Transform searchNearestTarget()
    {
        Transform nearest = null;

        Collider[] hits = Physics.OverlapSphere(transform.position, _searchTargetRadious, getCurrentLayer());

        //ordenar la lista por distancia
        List<Transform> sortedTargets = hits
            .OrderBy(hit => Vector3.Distance(hit.transform.position, transform.position))
            .Select(hit => hit.transform)
            .ToList();



        foreach (var target in sortedTargets)
        {
            var resourceComp = target.GetComponent<ResourceComponente>();
            
            //si es un recurso
            if(resourceComp != null)
            {
                //recorremos sus posiciones
                for(int i = 0; i < resourceComp._workablePositions.Count; i++)
                {
                    //si la posicion no esta ocupada
                    if (!resourceComp._positionsOcupeds[i])
                    {
                        //TODO: comprobar si no colisiona con otro recurso 





                        //IMPORTANTE : marcamos la posicion como ocupada
                        resourceComp._positionsOcupeds[i] = true;
                        _workerComponent._indexResourceOcupped = i;

                        nearest = resourceComp._workablePositions[i]; 


                        break;  
                    }
                }
            }

            //si lo hemos encontrado
            if(nearest != null)
            {
                break;
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
