using UnityEngine;

/*

esta clase se encarga de llevar la cuenta de los robots activos con una lista,
para luego poder recorrerla y usarla para ver los robots activos, los elementos de la UI la usan para
saber cuantos robots mostrar


este componente esta en el objeto de GameManager
 
 */
public class RobotManager : MonoBehaviour
{
    public LayerMask _woodLayer;
    public LayerMask _waterLayer;
    public LayerMask _ironLayer;
    public LayerMask _copperLayer;


    public enum RobotResourceTarget
    {
        NONE,
        GET_WOOD,
        GET_WATER,
        GET_IRON,
        GET_COPPER
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
