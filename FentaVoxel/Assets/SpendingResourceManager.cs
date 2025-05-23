using UnityEngine;

public class SpendingResourceManager : MonoBehaviour
{
    public enum ResourcesType
    {
        WATER, ELECTRICITY
    }

    [SerializeField]
    public float _water;

    [SerializeField]
    public float _electricity;

    [SerializeField] public float _maxWater;
    [SerializeField] public float _maxElectricity;
    [SerializeField] public float _waterSpending;
    [SerializeField] public float _electricitySpending;

    [SerializeField] public float _currentRobotsAmount; 

    //referencias a los elementos de la UI...

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if(_water > 0)
        {
            _water -= _waterSpending * _currentRobotsAmount * Time.deltaTime;
        }
            
        if(_electricity > 0)
        {
            _electricity -= _electricitySpending * _currentRobotsAmount * Time.deltaTime;
        }     

    }

    public void SetRobotsAmount(int _robots)
    {
        _currentRobotsAmount = _robots;
    }

    public void addResources(ResourcesType type, float amount)
    {
        Debug.Log("Rellena");

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

    public float addMaxResources(ResourcesType type, float amount)
    {
        float vuelta = 0;

        switch (type)
        {
            case ResourcesType.ELECTRICITY:

                    _electricity += amount;

                    if (_electricity > _maxElectricity)
                    {
                        vuelta = _electricity - _maxElectricity;
                        _electricity = _maxElectricity;
                    }

                break;

            case ResourcesType.WATER:

                    _water += amount;

                    if (_water > _maxWater)
                    {
                        vuelta = _water - _maxWater;
                        _water = _maxWater;
                    }

                break;

            default:
                Debug.LogWarning("Tipo de recurso no reconocido: " + type);
                break;
        }   

        return vuelta;
        
    }

}
