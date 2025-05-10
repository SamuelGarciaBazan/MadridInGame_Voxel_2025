using UnityEngine;

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
    
    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject); 


        //si es un recurso
        if ( ((1 << other.gameObject.layer) & _resourcesMask.value) != 0)
        {
            var outline = other.GetComponentInChildren<Outline>();

           
            outline.setOutlineWidht(outLineOnHover);
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
