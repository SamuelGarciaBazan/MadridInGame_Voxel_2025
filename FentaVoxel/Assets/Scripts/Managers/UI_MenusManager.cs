using UnityEngine;
using UnityEngine.InputSystem;

using TMPro;

public class UI_MenusManager : MonoBehaviour
{

    [SerializeField]
    LayerMask _robotLayerMask;

    [SerializeField]    
    CameraRaycast _cameraRaycast;


    RobotController _currentRobotSelected= null;

  

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onPlayerClick(InputAction.CallbackContext context)
    {
        if (context.started) {
           
        }
       
    }

  

}
