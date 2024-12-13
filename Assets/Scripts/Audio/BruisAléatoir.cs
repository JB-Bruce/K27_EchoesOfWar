using UnityEngine;

public class BruisAléatoir : MonoBehaviour
{
    

   
     int musique;
    
    float repliqueCounter=0;
    int repliqueDele=10;
 void Update()
    {
        if((repliqueCounter+=Time.deltaTime)>=repliqueDele)
        {
                
         //SoudeManadjeur.instance.BruisAleatoir();
         
         repliqueDele=Random.Range(30,60);
         repliqueCounter=0;
                


        }
        Debug.Log(repliqueDele);
        Debug.Log(repliqueCounter);

        
            
    }
    
    
    
}
