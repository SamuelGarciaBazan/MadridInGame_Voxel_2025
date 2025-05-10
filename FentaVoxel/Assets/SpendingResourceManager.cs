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
            case ResourcesType.ELECTRICITY:
                _electricity += amount;
                break;

            case ResourcesType.WATER:
                _water += amount;
                break;

            default:
                Debug.LogWarning("Tipo de recurso no reconocido: " + type);
                break;
        }
    }

}
