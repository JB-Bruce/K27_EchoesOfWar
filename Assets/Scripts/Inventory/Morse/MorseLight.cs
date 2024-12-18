using System.Collections;
using UnityEngine;

public class MorseLight : MonoBehaviour
{
    public Light lightSource;
    public float dotDuration = 0.4f;
    public float dashDuration = 0.8f;
    public float elementGap = 0.4f;
    public float characterGap = 2f;

    private string morseCode = "--..."; // 7
    private bool isPlaying = false;

    void Start()
    {
        if (lightSource == null)
            lightSource = GetComponent<Light>();

        StartCoroutine(PlayMorseCode());

    }

    public void StartMorse()
    {
        if (lightSource == null)
            lightSource = GetComponent<Light>();

        StartCoroutine(PlayMorseCode());
    }

    public void StopMorse()
    {
        if (lightSource == null)
            lightSource = GetComponent<Light>();

        StopCoroutine(PlayMorseCode());
    }

    private IEnumerator PlayMorseCode()
    {
        if (isPlaying) yield break;
        isPlaying = true;

        while (true)
        {
            foreach (char symbol in morseCode)
            {
                if (symbol == '-')
                {
                    yield return FlashLight(dashDuration);
                }
                else if (symbol == '.')
                {
                    yield return FlashLight(dotDuration);
                }

                yield return new WaitForSeconds(elementGap);
            }

            yield return new WaitForSeconds(characterGap);
        }
    }

    private IEnumerator FlashLight(float duration)
    {
        lightSource.enabled = true;
        yield return new WaitForSeconds(duration);
        lightSource.enabled = false;
    }
}