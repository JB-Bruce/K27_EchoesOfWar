using Unity.VisualScripting;
using UnityEngine;

public class SoundsObjesColition : MonoBehaviour
{
   public AudioClip audioClip;

    // Update is called once per frame
    public void OnCollisionEnter(Collision collision)
    {
        AudioManageur.Instance.PlayClipAt(audioClip,transform.position);
        Debug.Log("colition");
    }
}
