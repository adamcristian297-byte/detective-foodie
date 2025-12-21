using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public GameObject inventoryPanel;
    public TextMeshProUGUI inventoryText;

    private List<string> items = new List<string>();

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        inventoryPanel.SetActive(false);
        UpdateUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        }
    }

    public void AddItem(string itemName)
    {
        if (!items.Contains(itemName))
        {
            items.Add(itemName);
            UpdateUI();
            Debug.Log("Added item: " + itemName);
        }
    }

    public bool HasItem(string itemName)
    {
        return items.Contains(itemName);
    }

    public void RemoveItem(string itemName)
    {
        if (items.Contains(itemName))
        {
            items.Remove(itemName);
            UpdateUI();
            Debug.Log("Removed item: " + itemName);
        }
    }

    void UpdateUI()
    {
        inventoryText.text = "INVENTORY:\n\n";
        foreach (string item in items)
        {
            inventoryText.text += "- " + item + "\n";
        }
    }
}
