using UnityEngine;

public class NPC1 : MonoBehaviour
{
    [Header("NPC Info")]
    public string npcName = "Witness";          // Name shown in dialogue
    [TextArea] public string dialogue = "Hello detective!";  // Dialogue text
    public string clueName = "";                // Optional clue / item given
    private bool clueGiven = false;             // Prevent giving the clue multiple times
    public float displayTime = 30f;             // How long the dialogue stays on screen

    public void Interact()
    {
        // Show dialogue
        if (DialogueManager.instance != null)
            DialogueManager.instance.ShowDialogue(npcName, dialogue, clueName, displayTime);

        // Give clue to player only once
        if (!string.IsNullOrEmpty(clueName) && !clueGiven)
        {
            clueGiven = true;

            if (InventoryManager.instance != null)
                InventoryManager.instance.AddItem(clueName);
        }

        // Complete objective if it exists
        if (ObjectiveManager.instance != null)
            ObjectiveManager.instance.CompleteObjective("Talk to " + npcName);
    }
}
