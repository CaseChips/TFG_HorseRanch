using UnityEngine;
using UnityEngine.UI;

public class HoofMinigame : MonoBehaviour
{
    public static HoofMinigame instance;

    [Header("UI Elements")]
    public GameObject minigamePanel;

    private HorseStats currentHorse;

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

        // Optional: Freeze the player so they can't walk away while picking hooves!
        Time.timeScale = 0f;

        Debug.Log("Hoof Minigame Opened!");

        // TODO: In Phase 2, we will spawn the dirt and reset the "Hurt" meter here
    }

    // Called when we finish or cancel the game
    public void CloseMinigame()
    {
        minigamePanel.SetActive(false);

        // Unfreeze the player
        Time.timeScale = 1f;

        Debug.Log("Hoof Minigame Closed!");
    }
}