using UnityEngine;



/*
 El gameManager es el unico singleton, el resto de singletons, seran componentes del prefab del gameManager, y para acceder se hara 
GameManager.getInstance().getComponent<>
 
 
 */
public class GameManager : MonoBehaviour
{

    //referencia al time...

    //gestion del bucle de juego...


    private static GameManager _instance = null;

    public static GameManager getInstance()
    {
        return _instance;
    }


    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Debug.Log("ya existe un gameManager, destruyendo gameObject" + gameObject.name);
            Destroy(gameObject);    
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
