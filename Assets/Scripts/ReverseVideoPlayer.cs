using UnityEngine;
using UnityEngine.Video;

public class ReverseVideoPlayer : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Assignez le VideoPlayer dans l'Inspector
    public float playbackSpeed = 1f; // Vitesse de lecture invers�e

    private bool isReversing = false;

    void Start()
    {
        if (videoPlayer != null)
        {
            // D�sactive la lecture automatique
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
            videoPlayer.playbackSpeed = 0; // Arr�ter la lecture normale
            videoPlayer.frame -= Mathf.RoundToInt(playbackSpeed * Time.deltaTime * videoPlayer.frameRate);

            // Si la vid�o atteint le d�but
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
