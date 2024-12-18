using UnityEngine;
using UnityEngine.Audio;

public class SfxScript: MonoBehaviour
{
      public int  ListeAudio;
      public int ListMusic;
    public void Start()
    {
       StarteAnbiance();
    }
    public void StarteAnbiance()
    {
      AudioManageur.Instance.GoSfx(ListeAudio,ListMusic);
    } 
}
