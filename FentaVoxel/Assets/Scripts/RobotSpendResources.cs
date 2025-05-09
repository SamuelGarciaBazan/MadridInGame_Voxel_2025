using UnityEngine;



/*
 
se encarga de ir gastando los recursos que consume el robot 
*/
public class RobotSpendResources : MonoBehaviour
{
    [SerializeField]
    float _spentAmount;

    [SerializeField]
    float _spentTimeRate;


    float _elapsedTime = 0;


    //referencia al ResourcesManager
    ResourcesManager _resourcesManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         _resourcesManager = GameManager.getInstance().gameObject.GetComponent<ResourcesManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //spendResources();



    }



    void spendResources()
    {
        //if robot active...

        _elapsedTime += Time.deltaTime;
        if(_elapsedTime > _spentTimeRate)
        {
            _elapsedTime -= _spentTimeRate;

            //restar la cantidad _spentAmount al _resourcesManager

        }

    }

}
