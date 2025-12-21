using UnityEngine;
using UnityEngine.SceneManagement;

public class StorySceneManager : MonoBehaviour
{
    // Call this function from the button
    public void StartGame()
    {
        SceneManager.LoadScene("CrimeScene"); // Replace with your main game scene name
    }
}
