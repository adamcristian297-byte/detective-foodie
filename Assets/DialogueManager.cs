using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public GameObject dialoguePanel;     // Panel for dialogue
    public TextMeshProUGUI dialogueText; // Main dialogue text
    public TextMeshProUGUI clueText;     // Optional clue text

    private Coroutine hideCoroutine;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        dialoguePanel.SetActive(false);
    }

    /// <summary>
    /// Shows dialogue on screen.
    /// npcName is optional — pass empty string "" if you don’t want it.
    /// </summary>
    public void ShowDialogue(string npcName, string dialogueMessage, string clueMessage = "", float duration = 30f)
    {
        dialoguePanel.SetActive(true);

        // Display dialogue
        if (dialogueText != null)
            dialogueText.text = string.IsNullOrEmpty(npcName) ? dialogueMessage : npcName + "\n" + dialogueMessage;

        // Display clue if provided
        if (clueText != null)
            clueText.text = string.IsNullOrEmpty(clueMessage) ? "" : "Clue: " + clueMessage;

        // Stop previous hide coroutine if any
        if (hideCoroutine != null) StopCoroutine(hideCoroutine);
        hideCoroutine = StartCoroutine(HideAfterSeconds(duration));
    }

    private IEnumerator HideAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        dialoguePanel.SetActive(false);

        if (dialogueText != null)
            dialogueText.text = "";

        if (clueText != null)
            clueText.text = "";
    }
}
