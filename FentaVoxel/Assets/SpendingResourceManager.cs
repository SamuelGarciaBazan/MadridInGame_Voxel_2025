using UnityEngine;

public class SpendingResourceManager : MonoBehaviour
{
    public enum ResourcesType
    {
        WATER, ELECTRICITY
    }

    [SerializeField]
    float _water;

    [SerializeField]
    float _electricity;

    [SerializeField] float _maxWater;
    [SerializeField] float _maxElectricity;
    [SerializeField] float _waterSpending;
    [SerializeField] float _electricitySpending;

    [SerializeField] float _currentRobotsAmount;

    float _elapsedTime = 0;

    //referencias a los elementos de la UI...

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(_elapsedTime >= 1) 
        {
            if(_water > 0)
            {
                _water -= _maxWater * _currentRobotsAmount;
            }
            
            if(_electricity > 0)
            {
                _electricity -= _electricitySpending * _currentRobotsAmount;
            }
            
        }
        else{

            _elapsedTime += Time.deltaTime;

        }
        

    }

    public void addResources(ResourcesType type, float amount)
    {
        switch (type)
        {
            case ResourcesType.ELECTRICITY:

                if(amount + _electricity <= _maxElectricity) 
                {
                    _electricity += amount;
                }
                
                break;

            case ResourcesType.WATER:

                if (amount + _water <= _maxWater)
                {
                    _water += amount;
                }

                break;

            default:
                Debug.LogWarning("Tipo de recurso no reconocido: " + type);
                break;
        }
    }

}
