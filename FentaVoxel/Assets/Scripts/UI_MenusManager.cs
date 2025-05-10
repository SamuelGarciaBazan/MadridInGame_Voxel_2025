using UnityEngine;
using UnityEngine.InputSystem;

public class UI_MenusManager : MonoBehaviour
{
    [SerializeField]    
    CameraRaycast cameraRaycast;

    [SerializeField]
    GameObject _chooseRobotTaskPanel;


 
    // Update is called once per frame
    void Update()
    {
        
    }

    public void onPlayerClick(InputAction.CallbackContext context)
    {
        if (context.started) {
            Debug.Log("started");
        }
        if (context.performed) {

            Debug.Log("performed");
        }
    }


}
