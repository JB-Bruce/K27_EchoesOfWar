using UnityEngine;
using UnityEngine.Audio;

public class Soud : MonoBehaviour
{
      public int  ListeAudio;
      public int ListMusic;
    public void Start()
    {
       StarteAnbiance();
    }
    public void StarteAnbiance()
    {
      AudioManageur1.instance.GoMusic(ListeAudio,ListMusic);
    } 
}
