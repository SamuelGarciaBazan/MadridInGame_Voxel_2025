using UnityEngine;


/*
 Esta clase guarda los datos de la cantidad  de recursos que tiene  el jugador y se encarga de representar su informacion en la UI
 
este componente esta en el objeto de GameManager
 */
public class ResourcesManager : MonoBehaviour
{
    public enum ResourcesType
    {
        WOOD,   WATER,  IRON,   COPPER
    }


    [SerializeField]
    public float _wood;

    [SerializeField]
    public float _water;

    [SerializeField]
    public float _iron;

    [SerializeField]
    public float _copper;

 


    //referencias a los elementos de la UI...



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addResources(ResourcesType type, float amount)
    {
        switch (type)
        {
            case ResourcesType.WOOD:
                _wood += amount;
                break;

            case ResourcesType.WATER:
                _water += amount;
                break;

            case ResourcesType.IRON:
                _iron += amount;
                break;

            case ResourcesType.COPPER:
                _copper += amount;
                break;

            default:
                Debug.LogWarning("Tipo de recurso no reconocido: " + type);
                break;
        }
    }

    public float getResourceAmount(ResourcesType type)
    {
        float resourceAmount = 0;

        switch (type)
        {
            case ResourcesType.WOOD:
                resourceAmount = _wood;
                break;

            case ResourcesType.WATER:
                resourceAmount = _water;
                break;

            case ResourcesType.IRON:
                resourceAmount = _iron;
                break;

            default:
                Debug.LogWarning("Tipo de recurso no reconocido: " + type);
                break;
        }

        return resourceAmount;

    }


    public bool hasEnoughResource(ResourcesType type, float amount) 
    {

        bool hasEnough = false;

        switch (type)
        {
            case ResourcesType.WOOD:
                if (amount <= _wood)
                {
                    hasEnough = true;
                }
                break;

            case ResourcesType.WATER:
                if (amount <= _water)
                {
                    hasEnough = true;
                }
                break;

            case ResourcesType.IRON:
                if (amount <= _iron)
                {
                    hasEnough = true;
                }
                break;

            default:
                Debug.LogWarning("Tipo de recurso no reconocido: " + type);
                break;
        }

        return hasEnough;

    }

    public bool spendResource(ResourcesType type, float amount)
    {

        if(hasEnoughResource(type, amount))
        {
            switch (type)
            {
                case ResourcesType.WOOD:
                    _wood -= amount;
                    break;

                case ResourcesType.WATER:
                    _water -= amount;
                    break;

                case ResourcesType.IRON:
                    _iron -= amount;
                    break;

                default:
                    Debug.LogWarning("Tipo de recurso no reconocido: " + type);
                    break;
            }

            return true;
        }
        else
        {
            return false;
        }

    }

}
