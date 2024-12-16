using UnityEngine;

public class QuitSettings : MonoBehaviour
{
    public GameObject _MenuSittings;
    public void QuitSittings()
    {
        _MenuSittings.SetActive(false);
    } 
}
