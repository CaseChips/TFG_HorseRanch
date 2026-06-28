using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Yarn.Unity; 

public class MainMenuController : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject mainMenuPanel;
    public GameObject cutscenePanel;

    [Header("Fade Settings")]
    public CanvasGroup fadeScreen; 
    public float fadeDuration = 1.5f;

    [Header("Scene Transition")]
    public string mainGameSceneName = "MainScene"; 

    [Header("Yarn Integration")]
    public DialogueRunner dialogueRunner;
    public string cutsceneNode = "Intro_Story";

    private bool isCutsceneRunning = false;

    void Start()
    {
        mainMenuPanel.SetActive(true);
        cutscenePanel.SetActive(false);

        fadeScreen.alpha = 1f;
        fadeScreen.gameObject.SetActive(true);
        AudioListener.volume = 0f;

        StartCoroutine(FadeInMainMenu());
    }

    private IEnumerator FadeInMainMenu()
    {
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
    }

    public void OnPlayButtonClicked()
    {
        StartCoroutine(TransitionToCutscene());
    }

    private IEnumerator TransitionToCutscene()
    {
        fadeScreen.gameObject.SetActive(true);
        float elapsed = 0f;
        float quickFade = fadeDuration / 2f; 

        while (elapsed < quickFade)
        {
            fadeScreen.alpha = Mathf.Lerp(0f, 1f, elapsed / quickFade);
            elapsed += Time.deltaTime;
            yield return null;
        }
        fadeScreen.alpha = 1f;

        mainMenuPanel.SetActive(false);
        cutscenePanel.SetActive(true);

        elapsed = 0f;
        
        while (elapsed < quickFade)
        {
            fadeScreen.alpha = Mathf.Lerp(1f, 0f, elapsed / quickFade);
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        fadeScreen.alpha = 0f;
        fadeScreen.gameObject.SetActive(false);

        dialogueRunner.StartDialogue(cutsceneNode);
        isCutsceneRunning = true;
    }

    void Update()
    {
        if (isCutsceneRunning && !dialogueRunner.IsDialogueRunning)
        {
            isCutsceneRunning = false;
            StartCoroutine(FadeToBlackAndLoad());
        }
    }

    private IEnumerator FadeToBlackAndLoad()
    {
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

        SceneManager.LoadScene(mainGameSceneName);
    }
}