using UnityEngine;

public class KeyDropOff : MonoBehaviour
{
    public string keyName = "Samir's Key";

    public void Interact()
    {
        if (InventoryManager.instance != null && InventoryManager.instance.HasItem(keyName))
        {
            InventoryManager.instance.RemoveItem(keyName);

            // Complete delivery objective
            if (ObjectiveManager.instance != null)
            {
                ObjectiveManager.instance.CompleteObjective("Deliver the key to the drop off spot");
                ObjectiveManager.instance.AddObjective("Go back to Samir for information");
            }

            if (DialogueManager.instance != null)
            {
                DialogueManager.instance.ShowDialogue("Samir", "Thank you! Now return to me for the information.", "", 5f);
            }
        }
    }
}
