using UnityEngine;
using UnityEngine.Audio;

public class Volume : MonoBehaviour
{
    //public AudioMixer _audioMixer;
    public void SetVolume(float Volume)
    {
        //_audioMixer.SetFloat("Volume",Volume);
        Debug.Log(Volume);
    }
}
