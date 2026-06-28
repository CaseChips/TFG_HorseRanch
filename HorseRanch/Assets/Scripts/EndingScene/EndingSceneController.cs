using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Yarn.Unity;

public class EndingSceneController : MonoBehaviour
{
    [Header("Fade Settings")]
    public CanvasGroup fadeScreen;
    public float fadeDuration = 1.5f;

    [Header("Scene Transition")]
    public string mainMenuSceneName = "MainMenu"; 

    [Header("Yarn Integration")]
    public DialogueRunner dialogueRunner;
    public string endingNode = "Ending_Story"; 

    void Start()
    {
        StartCoroutine(EndingSequence());
    }

    private IEnumerator EndingSequence()
    {
        fadeScreen.alpha = 1f;
        fadeScreen.gameObject.SetActive(true);
        AudioListener.volume = 0f;

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            float normalizedTime = elapsed / fadeDuration;
            
            fadeScreen.alpha = Mathf.Lerp(1f, 0f, normalizedTime);
            AudioListener.volume = Mathf.Lerp(0f, 1f, normalizedTime);
            
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        fadeScreen.alpha = 0f;
        fadeScreen.gameObject.SetActive(false);
        AudioListener.volume = 1f;

        dialogueRunner.StartDialogue(endingNode);
        yield return null; 

        yield return new WaitUntil(() => !dialogueRunner.IsDialogueRunning);

        fadeScreen.gameObject.SetActive(true);
        elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            float normalizedTime = elapsed / fadeDuration;
            
            fadeScreen.alpha = Mathf.Lerp(0f, 1f, normalizedTime);
            AudioListener.volume = Mathf.Lerp(1f, 0f, normalizedTime);
            
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        fadeScreen.alpha = 1f;
        AudioListener.volume = 0f;

        SceneManager.LoadScene(mainMenuSceneName);
    }
}