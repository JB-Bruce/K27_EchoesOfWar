using UnityEngine;

public class Dysplay : MonoBehaviour
{
    bool _Full;
    public void DysplayFullSCreen()
    {
        _Full=true;
        Screen.fullScreen =_Full;
        Debug.Log("DysplayFullSCreen");
       

    }
    public void DysplayWindo()
    {
        _Full=false;
        Screen.fullScreen =_Full;
         Debug.Log("DysplayWindo");

    }
    public void DysplayWindoFullScrine()
    {
        _Full=false;
        Screen.fullScreen =_Full;
        Screen.SetResolution(1920, 1080,true);
         Debug.Log("DysplayWindoFullScrine");
       

    }
}
