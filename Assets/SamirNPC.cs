using UnityEngine;

public class SamirNPC : MonoBehaviour
{
    public string npcName = "Samir";
    [TextArea] public string dialogue = "Detective, I need my key back!";
    [TextArea] public string infoDialogue = "Thanks for the key! Here's the information you need.";

    private bool talkedTo = false;   
    private bool infoGiven = false;  

    public void Interact()
    {
        // First talk: trigger key pickup
        if (!talkedTo)
        {
            talkedTo = true;

            if (DialogueManager.instance != null)
                DialogueManager.instance.ShowDialogue(npcName, dialogue, "", 5f);

            // Complete interrogation objective
            if (ObjectiveManager.instance != null)
            {
                ObjectiveManager.instance.CompleteObjective("Interrogate the people");
                ObjectiveManager.instance.AddObjective("Pick up Samir's Key");
            }
            return;
        }

        // After key is delivered: give info
        if (!infoGiven && !InventoryManager.instance.HasItem("Samir's Key"))
        {
            infoGiven = true;

            if (DialogueManager.instance != null)
                DialogueManager.instance.ShowDialogue(npcName, infoDialogue, "", 5f);

            if (ObjectiveManager.instance != null)
            {
                ObjectiveManager.instance.CompleteObjective("Go back to Samir for information");
                ObjectiveManager.instance.AddObjective("Investigate the crime scene");
            }
            return;
        }

        // If talked again after info
        if (DialogueManager.instance != null)
            DialogueManager.instance.ShowDialogue(npcName, "Thanks again, detective.", "", 5f);
    }
}
