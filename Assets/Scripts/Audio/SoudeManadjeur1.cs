using JetBrains.Annotations;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManageur : Singleton<AudioManageur>
{
  public AudioClip[] _noiseAléatoirSounde, _musicSounde,_sfxSounde,_ambienceSounde;
  public AudioSource _audioSourceNoise;
  public AudioSource []_audioSourceMusic;
  public AudioSource[] _audioSourceSfx;
  public AudioSource[] _audioSourceAmbience;


 

  
    public AudioMixerGroup _mixerNoise;
    public AudioMixerGroup _mixerMusic;
    public AudioMixerGroup _mixerSfx;
    public AudioMixerGroup _mixerAmbiencee;
    
 
    int _noise;
   
    
    
    public void NandomNoise()
    {
        
        
        _noise=Random.Range(0,15);
        _audioSourceNoise.clip=_noiseAléatoirSounde[_noise];
        _audioSourceNoise.Play();
        _audioSourceNoise.outputAudioMixerGroup=_mixerNoise;
        
    }
    
        
        
      

      
       

    
    public void GoMusic(int audiolist,int listMusic)
    {
       _audioSourceMusic[audiolist].outputAudioMixerGroup=_mixerMusic;
       _audioSourceMusic[audiolist].clip=_musicSounde[listMusic];
       _audioSourceMusic[audiolist].Play();
    }
    public void GoSfx(int audiolist,int listSfx)
    {
      _audioSourceSfx[audiolist].outputAudioMixerGroup=_mixerSfx;
      _audioSourceSfx[audiolist].clip=_sfxSounde[listSfx];
      _audioSourceSfx[audiolist].Play();
    }
    public void GoAmbience(int audiolist,int listAmbience)
    {
      _audioSourceAmbience[audiolist].outputAudioMixerGroup=_mixerAmbiencee;
      _audioSourceAmbience[audiolist].clip=_ambienceSounde[listAmbience];
      _audioSourceAmbience[audiolist].Play();
    }
   public AudioSource PlayClipAt(AudioClip audio, Vector3 pos)
   {
    GameObject tempGO =new GameObject("TempAudio");
    tempGO.transform.position=pos;
    AudioSource audioSource = tempGO.AddComponent<AudioSource>();
    audioSource.outputAudioMixerGroup=_mixerSfx;
    audioSource.clip=audio;
    audioSource.Play();
    Destroy(tempGO,audio.length);
    return audioSource;
   }
    
    
  
  

}
