using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Yarn.Unity;

public class FadeOut : MonoBehaviour
{
    [Header("References")]
    public DialogueRunner dialogueRunner;
    public CanvasGroup fadeScreen; 
    public string endingSceneName = "EndingScene";
    public float fadeDuration = 1.5f;

    public void TriggerEndingSequence()
    {
        StartCoroutine(EndGameTransition());
    }

    private IEnumerator EndGameTransition()
    {
        yield return new WaitUntil(() => !dialogueRunner.IsDialogueRunning);

        fadeScreen.gameObject.SetActive(true);
        float elapsed = 0f;
        
        float startVolume = AudioListener.volume;

        while (elapsed < fadeDuration)
        {
            float normalizedTime = elapsed / fadeDuration;

            fadeScreen.alpha = Mathf.Lerp(0f, 1f, normalizedTime);
            AudioListener.volume = Mathf.Lerp(startVolume, 0f, normalizedTime);

            elapsed += Time.deltaTime;
            yield return null;
        }

        fadeScreen.alpha = 1f;
        AudioListener.volume = 0f;

        SceneManager.LoadScene(endingSceneName);
    }
}