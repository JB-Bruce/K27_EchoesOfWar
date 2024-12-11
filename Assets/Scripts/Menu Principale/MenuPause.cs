using UnityEngine;
using UnityEngine.InputSystem;
public class MenuPause : MonoBehaviour
{
    public GameObject _activeMenu;
     public void Pause(InputAction.CallbackContext context)
    {
        if (context.performed )
        {
            _activeMenu.SetActive(!_activeMenu.activeSelf);
            if (_activeMenu.activeSelf )
            {
                Time.timeScale=0;
            }
            if (!_activeMenu.activeSelf )
            {
                Time.timeScale=1;
            }
            
            
        }

        
        
    }
   
}
