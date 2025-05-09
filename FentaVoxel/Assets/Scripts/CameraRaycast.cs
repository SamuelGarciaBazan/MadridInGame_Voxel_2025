using UnityEngine;

public class CameraRaycast : MonoBehaviour
{

    private Camera _mainCamera;

    public Collider MakeRaycast(LayerMask layer)
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 200;

        Ray ray = _mainCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 200, layer))
        {
            return hit.collider;
        }
        else
        {
            return null;
        }

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        _mainCamera = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 200;
        mousePos = _mainCamera.ScreenToWorldPoint(mousePos);

        Debug.DrawRay(transform.position,  mousePos - transform.position, Color.red);



    }
}
