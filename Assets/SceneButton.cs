using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButton : MonoBehaviour
{
    public string sceneToLoad = "NextScene";

    public void Interact()
    {
        // Optional: complete objective
        if (ObjectiveManager.instance != null)
        {
            ObjectiveManager.instance.CompleteObjective("Go and press the button at the bar");
        }

        SceneManager.LoadScene(sceneToLoad);
    }
}
