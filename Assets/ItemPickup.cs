using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public string itemName = "Samir's Key"; // Name of the item
    public string pickupObjective = "Pick up Samir's Key"; // Objective to complete
    public string nextObjective = "Deliver the key to Samir"; // Objective to add

    private bool pickedUp = false;

    // This method is called by PlayerInteract's raycast
    public void Interact()
    {
        if (pickedUp) return; // Prevent picking up multiple times
        pickedUp = true;

        // Add item to inventory
        if (InventoryManager.instance != null)
        {
            InventoryManager.instance.AddItem(itemName);
        }

        // Complete pickup objective and add next objective
        if (ObjectiveManager.instance != null)
        {
            ObjectiveManager.instance.CompleteObjective(pickupObjective);
            ObjectiveManager.instance.AddObjective(nextObjective);
        }

        // Optional: show a small dialogue or notification
        if (DialogueManager.instance != null)
        {
            DialogueManager.instance.ShowDialogue("", "You picked up: " + itemName, "", 5f);
        }

        // Remove key from scene
        Destroy(gameObject);
    }
}
