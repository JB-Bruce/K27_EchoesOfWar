using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
public class MenuPause : MonoBehaviour
{
    public GameObject _activeMenu;
    public GameObject _fermePanel;
     public void Pause(InputAction.CallbackContext context)
    {
        if (context.performed )
        {
            _activeMenu.SetActive(!_activeMenu.activeSelf);
            if (_activeMenu.activeSelf )
            {
                Cursor.lockState=CursorLockMode.None;
                
                Time.timeScale=0;
            }
            if (!_activeMenu.activeSelf )
            {
                 _fermePanel.SetActive(false);
                Time.timeScale=1;
                Cursor.lockState=CursorLockMode.Locked;
                
            }
            
            
        }

        
        
    }
   
}
