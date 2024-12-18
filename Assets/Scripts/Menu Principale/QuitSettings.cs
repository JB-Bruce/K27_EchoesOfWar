using UnityEngine;
using UnityEngine.Video;

public class QuitSettings : MonoBehaviour
{
    public GameObject _vidéoFons;
    public GameObject _MenuSittings;
    public void QuitSittings()
    {
        _vidéoFons.SetActive(true);
        _MenuSittings.SetActive(false);
    } 
}
