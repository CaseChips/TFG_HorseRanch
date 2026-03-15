using UnityEngine;
using TMPro; 

public class FeedMixerMinigame : MonoBehaviour
{
    public static FeedMixerMinigame instance;

    [Header("UI Elements")]
    public GameObject mixerPanel;
    public TextMeshProUGUI recipeText; 

    [Header("Game Data")]
    public int maxScoops = 5;

    private int hayScoops = 0;
    private int grainScoops = 0;
    private int treatScoops = 0;
    private int totalScoops = 0;

    private HorseStats currentHorse;

    private bool hasBeenFedToday;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        mixerPanel.SetActive(false);
    }

    public void OpenMinigame(HorseStats horse)
    {
        if (hasBeenFedToday)
        {
            Debug.Log("This horse has already been fed!");
        }
        currentHorse = horse;

        hayScoops = 0;
        grainScoops = 0;
        treatScoops = 0;
        totalScoops = 0;

        UpdateUI();

        mixerPanel.SetActive(true);
        Time.timeScale = 0f; 
    }

    // --- BUTTON FUNCTIONS ---

    public void AddHay()
    {
        if (totalScoops < maxScoops) { hayScoops++; totalScoops++; UpdateUI(); }
    }

    public void AddGrain()
    {
        if (totalScoops < maxScoops) { grainScoops++; totalScoops++; UpdateUI(); }
    }

    public void AddTreat()
    {
        if (totalScoops < maxScoops) { treatScoops++; totalScoops++; UpdateUI(); }
    }

    private void UpdateUI()
    {
        recipeText.text = $"Bucket Contents ({totalScoops}/{maxScoops}):\n" +
                          $"Hay: {hayScoops}\n" +
                          $"Grain: {grainScoops}\n" +
                          $"Treats: {treatScoops}";
    }

    // --- THE EDUCATIONAL LOGIC ---

    public void ServeMeal()
    {
        if (totalScoops == 0)
        {
            Debug.Log("The bucket is empty!");
            return;
        }

        // EVALUATION: The 80/20 Rule
        if (grainScoops > 2)
        {
            Debug.Log("DANGER: Too much grain! Horses have sensitive stomachs and excess grain can cause Colic or Laminitis.");
            if (currentHorse != null) currentHorse.IncreaseStat("comfort", -20f); // Tummy ache!
        }
        else if (treatScoops > 1)
        {
            Debug.Log("Too many treats! Apples and carrots have too much sugar. Keep it to one per meal.");
        }
        else if (hayScoops >= 3 && grainScoops <= 1)
        {
            Debug.Log("PERFECT MEAL: High in forage fiber, low in sugar/starch. Excellent for gut health!");
            if (currentHorse != null)
            {
                currentHorse.IncreaseStat("hunger", 50f);
                currentHorse.IncreaseStat("comfort", 10f);
            }
        }
        else
        {
            Debug.Log("Okay meal, but try to make the bulk of the diet Hay next time.");
            if (currentHorse != null) currentHorse.IncreaseStat("hunger", 20f);
        }

        CloseMinigame();
    }

    public void CloseMinigame()
    {
        mixerPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}