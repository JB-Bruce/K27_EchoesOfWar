using UnityEngine;
using UnityEngine.Audio;

public class RandomsoundsScript : MonoBehaviour
{
  [SerializeField]int _minimumDaily;
    [SerializeField]int _dailest;
      float _NoiseCounter=0;
    int _NoiseDele=10;
    void Update()
    {
        if((_NoiseCounter+=Time.deltaTime)>=_NoiseDele)
        {
                
         AudioManageur.Instance.NandomNoise();
         
         _NoiseDele=Random.Range(_minimumDaily,_dailest);
         _NoiseCounter=0;
                


        }
    }

    
}
