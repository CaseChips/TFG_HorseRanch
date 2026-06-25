using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HoofMinigame : MonoBehaviour
{
    public static HoofMinigame instance;

    [Header("UI Elements")]
    public GameObject minigamePanel;
    public Texture2D hoofPickCursor; 

    [Header("Game Settings")]
    public int totalDirtPieces = 3; 
    public int maxMistakes = 3;

    [Header("Mistake Feedback")]
    public AudioSource audioSource;
    public AudioClip mistakeSound;
    public float shakeDuration = 0.3f;
    public float shakeMagnitude = 0.1f;

    private HorseStats currentHorse;
    private int dirtRemaining;
    private int mistakesMade;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        minigamePanel.SetActive(false);
    }

    public void OpenMinigame(HorseStats horse)
    {
        currentHorse = horse;
        minigamePanel.SetActive(true);
        Time.timeScale = 0f;

        dirtRemaining = totalDirtPieces;
        mistakesMade = 0;

        MissionManager.instance.UpdateProgressText($"({dirtRemaining} dirt remaining)");
 
        if (hoofPickCursor != null)
        {
            Cursor.SetCursor(hoofPickCursor, Vector2.zero, CursorMode.Auto);
        }
    }

    public void DirtPicked()
    {
        dirtRemaining--;

        MissionManager.instance.UpdateProgressText($"({dirtRemaining} dirt remaining)");

        if (dirtRemaining <= 0)
        {
            WinGame();
        }
    }

    public void FrogPoked()
    {
        if (mistakeSound != null && audioSource != null) audioSource.PlayOneShot(mistakeSound);
        
        StartCoroutine(ShakeCamera());

        mistakesMade++;

        if (mistakesMade >= maxMistakes)
        {
            LoseGame();
        }
    }

   private IEnumerator ShakeCamera()
    {
        Transform camTransform = Camera.main.transform;
        Vector3 originalPos = camTransform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            camTransform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);

            elapsed += Time.unscaledDeltaTime;
            
            yield return null; 
        }

        camTransform.localPosition = originalPos;
    }

    private void WinGame()
    {
        MissionManager.instance.AdvanceMission();
        if (currentHorse != null)
        {
            currentHorse.IncreaseStat("hygiene", 40f);
            currentHorse.IncreaseStat("comfort", 10f);
        }
        CloseMinigame();
    }

    private void LoseGame()
    {
        MissionManager.instance.AdvanceMission();
        if (currentHorse != null)
        {
            currentHorse.IncreaseStat("comfort", -50f);
        }
        CloseMinigame();
    }

    public void CloseMinigame()
    {
        minigamePanel.SetActive(false);
        Time.timeScale = 1f;

        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}