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
    public string mainGameSceneName = "HorseRanch"; 

    [Header("Yarn Integration")]
    public DialogueRunner dialogueRunner;
    public string cutsceneNode = "Intro_Story";

    private bool isCutsceneRunning = false;

    void Start()
    {
        mainMenuPanel.SetActive(true);
        cutscenePanel.SetActive(false);
        
        fadeScreen.alpha = 0f;
        fadeScreen.gameObject.SetActive(false);
    }

    public void OnPlayButtonClicked()
    {
        mainMenuPanel.SetActive(false);
        cutscenePanel.SetActive(true);

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

        while (elapsed < fadeDuration)
        {
            fadeScreen.alpha = Mathf.Lerp(0f, 1f, elapsed / fadeDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        fadeScreen.alpha = 1f;

        SceneManager.LoadScene(mainGameSceneName);
    }
}