using UnityEngine;
using System.Collections;

public class SceneFadeIn : MonoBehaviour
{
    [Header("Fade Settings")]
    public CanvasGroup fadeScreen;
    public float fadeDuration = 1.5f;
    
    [Header("Audio Settings")]
    public bool fadeAudio = true; 

    void Start()
    {
        if (fadeAudio)
        {
            AudioListener.volume = 0f;
        }

        if (fadeScreen != null)
        {
            fadeScreen.alpha = 1f;
            fadeScreen.gameObject.SetActive(true);
        }

        StartCoroutine(FadeInSequence());
    }

    private IEnumerator FadeInSequence()
    {
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            float normalizedTime = elapsed / fadeDuration; 

            if (fadeScreen != null)
            {
                fadeScreen.alpha = Mathf.Lerp(1f, 0f, normalizedTime);
            }

            if (fadeAudio)
            {
                AudioListener.volume = Mathf.Lerp(0f, 1f, normalizedTime);
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        if (fadeScreen != null)
        {
            fadeScreen.alpha = 0f;
            fadeScreen.gameObject.SetActive(false);
        }

        if (fadeAudio)
        {
            AudioListener.volume = 1f;
        }
    }
}