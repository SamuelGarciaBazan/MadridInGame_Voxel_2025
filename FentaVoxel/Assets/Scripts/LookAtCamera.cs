using UnityEngine;



//script para los objetos 2D miren siempre hacia la camara
public class LookAtCamera : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.forward = Camera.main.transform.forward;
    }
}
