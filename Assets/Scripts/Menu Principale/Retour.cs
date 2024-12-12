using UnityEngine;
using UnityEngine.InputSystem;
public class Retour : MonoBehaviour
{
   public GameObject _fermeMenu;
     public void Quit()
    {
        
        
            _fermeMenu.SetActive(false);
            Time.timeScale=1;
            Cursor.lockState=CursorLockMode.Locked;
            
        
    }
}
