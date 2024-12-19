using UnityEngine;
using UnityEngine.Video;

public class ReverseVideoPlayer : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Assignez le VideoPlayer dans l'Inspector
    public float playbackSpeed = 1f; // Vitesse de lecture inversée

    private bool isReversing = false;

    void Start()
    {
        if (videoPlayer != null)
        {
            // Désactive la lecture automatique
            videoPlayer.playOnAwake = false;
            videoPlayer.isLooping = false;
            videoPlayer.Prepare();

        }
    }

    void Update()
    {
        if (isReversing && videoPlayer.isPrepared)
        {
            // Reculer dans les frames
            videoPlayer.playbackSpeed = 0; // Arrêter la lecture normale
            videoPlayer.frame -= Mathf.RoundToInt(playbackSpeed * Time.deltaTime * videoPlayer.frameRate);

            // Si la vidéo atteint le début
            if (videoPlayer.frame <= 0)
            {
                videoPlayer.Pause();
                isReversing = false;
            }
        }
    }

    public void PlayReverse()
    {
        if (videoPlayer.isPrepared)
        {
            videoPlayer.frame = (long)videoPlayer.frameCount - 1;
            isReversing = true;
        }
    }

    public void StopReverse()
    {
        isReversing = false;
    }
}
