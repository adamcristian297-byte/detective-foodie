using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager instance;

    public TextMeshProUGUI objectiveText; 
    [HideInInspector] public List<string> objectives = new List<string>();

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        UpdateUI();
    }

    void Start()
    {
        // Add initial objective only in the main scene
        if (SceneManager.GetActiveScene().name == "oras") // Replace with your scene name
        {
            AddObjective("Interrogate the people");
        }
    }

    public void AddObjective(string objective)
    {
        if (!objectives.Contains(objective))
        {
            objectives.Add(objective);
            UpdateUI();
        }
    }

    public void CompleteObjective(string objective)
    {
        for (int i = 0; i < objectives.Count; i++)
        {
            if (objectives[i].Trim().ToLower() == objective.Trim().ToLower())
            {
                objectives.RemoveAt(i);
                UpdateUI();
                Debug.Log("Completed objective: " + objective);
                return;
            }
        }
    }

    void UpdateUI()
    {
        if (objectiveText == null) return;

        objectiveText.text = "";
        foreach (string obj in objectives)
        {
            objectiveText.text += "• " + obj + "\n";
        }
    }
}
