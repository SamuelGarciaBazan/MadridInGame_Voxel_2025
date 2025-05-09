using UnityEngine;


/*
este componente lo llevan todos los obejtos que son recursos, tienen su vida, y cada cuanto deben dropear un item de los que dan recursos
*/
public class ResourceComponente : MonoBehaviour
{

    [SerializeField]
    float _maxLife;

    [SerializeField]
    [Tooltip("Cada cuanta cantidad extraida de este recurso se suelta un item del recurso")]
    float _packageDropRate;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
