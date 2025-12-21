using UnityEngine;

public class PaperNoteTrigger : MonoBehaviour
{
    [TextArea]
    public string noteText;

    public float displayTime = 10f;

    [Header("Trigger After Reading")]
    public GameObject objectToEnable;   // The bar button
    public string newObjective = "Go and press the button at the bar";

    private bool read = false;

    public void Interact()
    {
        if (read) return;
        read = true;

        // Show note text
        if (DialogueManager.instance != null)
        {
            DialogueManager.instance.ShowDialogue("", noteText, "", displayTime);
        }

        // Enable the new object
        if (objectToEnable != null)
        {
            objectToEnable.SetActive(true);
        }

        // Add new objective
        if (ObjectiveManager.instance != null)
        {
            ObjectiveManager.instance.AddObjective(newObjective);
        }
    }
}
