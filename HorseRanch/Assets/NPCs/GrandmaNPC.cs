using UnityEngine;
using Yarn.Unity; 

public class GrandmaNPC : MonoBehaviour, IInteractable
{
    public DialogueRunner dialogueRunner;
    private bool isWaitingForDialogueToFinish = false;

    public void Interact()
    {
        if (dialogueRunner.IsDialogueRunning) return;

        int step = MissionManager.instance.currentStep;

        switch (step)
        {
            case 0:
                dialogueRunner.StartDialogue("Grandma_Intro");
                isWaitingForDialogueToFinish = true;
                break;
            case 2:
                dialogueRunner.StartDialogue("Grandma_Brush_Hint");
                isWaitingForDialogueToFinish = true;
                break;
            case 4:
                dialogueRunner.StartDialogue("Grandma_Hoof_Hint");
                isWaitingForDialogueToFinish = true;
                break;
            case 6:
                dialogueRunner.StartDialogue("Grandma_Water_Hint");
                isWaitingForDialogueToFinish = true;
                break;
            case 8:
                dialogueRunner.StartDialogue("Grandma_Feed_Hint");
                isWaitingForDialogueToFinish = true;
                break;
            case 10:
                dialogueRunner.StartDialogue("Grandma_Finish");
                MissionManager.instance.AdvanceMission();
                break;

            case 1:
                dialogueRunner.StartDialogue("Wait_Manure"); break;
            case 3:
                dialogueRunner.StartDialogue("Wait_Brush"); break;
            case 5:
                dialogueRunner.StartDialogue("Wait_Hooves"); break;
            case 7:
                dialogueRunner.StartDialogue("Wait_Water"); break;
            case 9:
                dialogueRunner.StartDialogue("Wait_Feed"); break;
        }
    }

    void Update()
    {
        if (isWaitingForDialogueToFinish && !dialogueRunner.IsDialogueRunning)
        {
            isWaitingForDialogueToFinish = false;
            MissionManager.instance.AdvanceMission();
        }
    }
}