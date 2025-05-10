using UnityEngine;
using UnityEngine.InputSystem;

using TMPro;

public class UI_MenusManager : MonoBehaviour
{

    [SerializeField]
    LayerMask _robotLayerMask;

    [SerializeField]    
    CameraRaycast _cameraRaycast;

    [SerializeField]
    GameObject _chooseRobotTaskPanel;


    RobotController _currentRobotSelected= null;

    [SerializeField]
    TMP_Dropdown _chooseTaskDropdown;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onPlayerClick(InputAction.CallbackContext context)
    {
        if (context.started) {
            Debug.Log("click pressed");

            Collider robotCollider = _cameraRaycast.MakeRaycast(_robotLayerMask);

            RobotController robotController = robotCollider.GetComponent<RobotController>();

            if (robotController != null) { 
            
                _currentRobotSelected = robotController;

                //update UI
                updateRobotTaskPanel();
            }
        }
       
    }


    void updateRobotTaskPanel()
    {
        _chooseRobotTaskPanel.SetActive(true);  

        //_chooseTaskDropdown.value = 


    }


    public void onRobotTaskPanelValueChange(int state)
    {
        //cambiar y actualizar el estado del robot

    }

}
