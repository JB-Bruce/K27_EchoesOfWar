using JetBrains.Annotations;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManageur1 : MonoBehaviour
{
  public AudioClip[] _noiseAléatoirSounde, _musicSounde,_sfxSounde,_ambienceSounde,_UISounde;
  public static AudioManageur1 instance;
  public AudioSource _audioSourceNoise;
  public AudioSource []_audioSourceMusic;
  public AudioSource[] _audioSourceSfx;
  public AudioSource[] _audioSourceAmbience;
  public AudioSource[] _audioSourceUI;
 // public AudioSource _audioSourceMaster;
 

  
    public AudioMixerGroup _mixerNoise;
    public AudioMixerGroup _mixerMusic;
    public AudioMixerGroup _mixerSfx;
    public AudioMixerGroup _mixerAmbiencee;
    public AudioMixerGroup _mixerUI;
    //public AudioMixerGroup _Master;
    int _noise;
   
  private void Awake()
    {
      instance=this;
    }
    
    
    public void NandomNoise()
    {
        
        
        _noise=Random.Range(0,6);
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
    public void GoUI(int audiolist,int listGoUI)
    {
      _audioSourceUI[audiolist].outputAudioMixerGroup=_mixerUI;
      _audioSourceUI[audiolist].clip=_UISounde[listGoUI];
      _audioSourceUI[audiolist].Play();
    }
    
    
  
  

}
