using UnityEngine;
using UnityEngine.Audio;

public class RandomsoundsScript : MonoBehaviour
{
      public int  ListeAudio;
      public int ListMusic;
    float _NoiseCounter=0;
    int _NoiseDele=10;



    [SerializeField]int _minimumDaily;
    [SerializeField]int _dailest;
   
    
      void Update()
    {
        if((_NoiseCounter+=Time.deltaTime)>=_NoiseDele)
        {
                
        AudioManageur1.instance.NandomNoise();
         
         _NoiseDele=Random.Range(_minimumDaily,_dailest);
         _NoiseCounter=0;
                


        }
        
        
      

      
       

    }
      
    
}
