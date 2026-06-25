using UnityEngine;
using TMPro;

public class MissionManager : MonoBehaviour
{
    public static MissionManager instance;

    [Header("Mission State")]
    public int currentStep = 0; 

    [Header("Objective UI")]
    public TextMeshProUGUI objectiveTextUI; 
    [TextArea]
    public string[] stepDescriptions; 
    
    private string dynamicProgress = "";

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        UpdateObjectiveUI();
    }

    public void CheckManure()
    {
        ManurePile[] remainingManure = FindObjectsOfType<ManurePile>();

        if (currentStep == 1 && remainingManure.Length <= 1)
        {
            AdvanceMission();
        }
    }

    public void AdvanceMission()
    {
        currentStep++;
        dynamicProgress = "";
        UpdateObjectiveUI();
    }

    public void UpdateProgressText(string newProgress)
    {
        dynamicProgress = newProgress;
        UpdateObjectiveUI();
    }

    private void UpdateObjectiveUI()
    {
        if (objectiveTextUI == null) return;

        if (currentStep < stepDescriptions.Length)
        {
            objectiveTextUI.text = "<b>Current Objective:</b>\n" + stepDescriptions[currentStep] + " " + dynamicProgress;
        }
        else
        {
            objectiveTextUI.text = "<b>Current Objective:</b>\nAll tasks complete!";
        }
    }
}