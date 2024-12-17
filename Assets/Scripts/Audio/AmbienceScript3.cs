using UnityEngine;
using UnityEngine.Audio;

public class AmbienceeScript: MonoBehaviour
{
      public int  ListeAudio;
      public int ListMusic;
    public void Start()
    {
       StarteAnbiance();
    }
    public void StarteAnbiance()
    {
      AudioManageur1.instance.GoAmbience(ListeAudio,ListMusic);
    } 
}
