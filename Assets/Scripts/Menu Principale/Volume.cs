using UnityEngine;
using UnityEngine.Audio;

public class Volume : MonoBehaviour
{
    public string _nomMixer;
    public AudioMixer _audioMixer;
    public void SetVolume(float Volume)
    {
        _audioMixer.SetFloat(_nomMixer,Volume);
        Debug.Log(Volume);
    }
}
