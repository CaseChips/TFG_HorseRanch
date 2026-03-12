using UnityEngine;

public class HorseStats : MonoBehaviour
{
    [Header("Daily Tasks Status")]
    [Range(0, 100)] public float hunger = 0f;   
    [Range(0, 100)] public float hygiene = 0f;  
    [Range(0, 100)] public float comfort = 0f;  

    public void IncreaseStat(string statName, float amount)
    {
        switch (statName.ToLower())
        {
            case "hunger":
                hunger = Mathf.Clamp(hunger + amount, 0, 100);
                break;
            case "hygiene":
                hygiene = Mathf.Clamp(hygiene + amount, 0, 100);
                break;
            case "comfort":
                comfort = Mathf.Clamp(comfort + amount, 0, 100);
                break;
        }
        Debug.Log($"Updated {statName}. Current Total Score: {GetTotalScore()}/300");
    }

    public float GetTotalScore()
    {
        return hunger + hygiene + comfort;
    }
}