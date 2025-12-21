using UnityEngine;

public class PaperNote : MonoBehaviour
{
    [TextArea]
    public string noteText = "The note is torn and hard to read...";
    public float displayTime = 10f;

    private bool alreadyRead = false;

    public void Interact()
    {
        if (alreadyRead) return;

        alreadyRead = true;

        if (DialogueManager.instance != null)
        {
            DialogueManager.instance.ShowDialogue(
                "",
                noteText,
                "",
                displayTime
            );
        }
    }
}
