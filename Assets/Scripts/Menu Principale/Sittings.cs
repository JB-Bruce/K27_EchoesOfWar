using UnityEngine;
using UnityEngine.Video;

public class Sittings : MonoBehaviour
{
    public GameObject _MenuSittings;
     public GameObject _vidéoFons;

   
    [SerializeField] VideoPlayer _videoPlays;
    [SerializeField] VideoClip _videoPclip;

    
    public void StarteSittings()
    {
        _videoPlays.clip=_videoPclip;
        _videoPlays.Play();

        _vidéoFons.SetActive(false);
        _MenuSittings.SetActive(true);
    } 


}
