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
    float _wood;

    [SerializeField]
    float _water;

    [SerializeField]
    float _iron;

    [SerializeField]
    float _copper;

 


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

}
