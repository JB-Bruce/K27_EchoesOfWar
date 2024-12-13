using UnityEngine;
using UnityEngine.Audio;

public class AudioManageur : MonoBehaviour
{
  public AudioClip[] _musicSounde,_sfxSounde,_ambienceSounde,_UISounde,_noiseAléatoirSounde;
  public static AudioManageur instance;
  public AudioSource _audioSourceNoise;
  public AudioSource _audioSourceMusic;
  public AudioSource _audioSourceSfx;
  public AudioSource _audioSourceAmbiencee;
  public AudioSource _audioSourceUI;
  public AudioSource _audioSourceMaster;
 

  
    public AudioMixerGroup _mixerNoise;
    public AudioMixerGroup _mixerMusic;
    public AudioMixerGroup _mixerSfx;
    public AudioMixerGroup _mixerAmbiencee;
    public AudioMixerGroup _mixerUI;
    public AudioMixerGroup _Master;
    int _noise;
   float _NoiseCounter=0;
    int _NoiseDele=10;



    [SerializeField]int _minimumDaily;
    [SerializeField]int _dailest;
  private void Awake()
    {
      instance=this;
    }
    public void Start()
    {
      _audioSourceAmbiencee.clip=_ambienceSounde[0];
      _audioSourceAmbiencee.Play();
      
    }
    
    public void NandomNoise()
    {
        
        
        _noise=Random.Range(0,6);
        _audioSourceNoise.clip=_noiseAléatoirSounde[_noise];
        _audioSourceNoise.Play();
        
    }
    void Update()
    {
        if((_NoiseCounter+=Time.deltaTime)>=_NoiseDele)
        {
                
         NandomNoise();
         
         _NoiseDele=Random.Range(_minimumDaily,_dailest);
         _NoiseCounter=0;
                


        }
        _audioSourceNoise.outputAudioMixerGroup=_mixerNoise;
        
      //   _audioSourceMaster.outputAudioMixerGroup=_Master;
         _audioSourceMusic.outputAudioMixerGroup=_mixerMusic;
      //   _audioSourceSfx.outputAudioMixerGroup=_mixerSfx;
       //_audioSourceAmbiencee.outputAudioMixerGroup=_mixerAmbiencee;
      //   _audioSourceUI.outputAudioMixerGroup=_mixerUI;
       

    }
    
  
  

}
