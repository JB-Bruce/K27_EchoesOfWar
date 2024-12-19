using UnityEngine;
using UnityEngine.Video;

public class MainmenuNavigation : MonoBehaviour
{
    public GameObject main;
    public GameObject settings;
    public GameObject credits;

    public VideoPlayer toSettings;
    public VideoPlayer toCredits;
    public ReverseVideoPlayer backFromSettings;
    public ReverseVideoPlayer backFromCredits;
    public VideoPlayer Main;
    public VideoPlayer Settings;
    public VideoPlayer Credits;

    private void Awake()
    {
        main.SetActive(true);
        settings.SetActive(false);
        credits.SetActive(false);
    }

    private void Start()
    {
        toSettings.Prepare();
        toCredits.Prepare();
        Main.targetCameraAlpha = 1f;
        Credits.targetCameraAlpha = 0f;
        Settings.targetCameraAlpha = 0f;
    }

    public void GoToCredits()
    {
        //toCredits.Play();
        Main.targetCameraAlpha = 0f;
        Credits.targetCameraAlpha = 1f;

        main.SetActive(false);
        credits.SetActive(true);
    }

    public void GoToSettings()
    {
        //toSettings.Play();
        Main.targetCameraAlpha = 0f;
        Settings.targetCameraAlpha = 1f;

        main.SetActive(false);
        settings.SetActive(true);
    }

    public void GoBackFromCredits()
    {
        //backFromCredits.PlayReverse();
        Main.targetCameraAlpha = 1f;
        Credits.targetCameraAlpha = 0f;

        main.SetActive(true);
        credits.SetActive(false);
    }

    public void GoBackFromSettings()
    {
        //backFromSettings.PlayReverse();
        Main.targetCameraAlpha = 1f;
        Settings.targetCameraAlpha = 0f;

        main.SetActive(true);
        settings.SetActive(false);
    }

    public void Play()
    {

    }
}
