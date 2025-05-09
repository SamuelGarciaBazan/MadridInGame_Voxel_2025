using UnityEngine;

public class CameraRaycast : MonoBehaviour
{
    [SerializeField] float _distance;

    private Camera _mainCamera;


    public Collider MakeRaycast(LayerMask layer)
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = _distance;

        Ray ray = _mainCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, _distance, layer))
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
        mousePos.z = _distance;
        mousePos = _mainCamera.ScreenToWorldPoint(mousePos);

        Debug.DrawRay(_mainCamera.transform.position,  mousePos - _mainCamera.transform.position, Color.red);



    }
}
