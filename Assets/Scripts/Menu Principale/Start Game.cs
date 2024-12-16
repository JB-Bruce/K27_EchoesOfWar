using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public int _scenes;
    public void GoScenes()
    {
        Debug.Log("StartGame");
        SceneManager.LoadScene( _scenes);
        
    }
}
