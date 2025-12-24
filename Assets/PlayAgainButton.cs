using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgainButton : MonoBehaviour
{
    // Set this to your main game scene name
    public string mainGameScene = "SampleScene";

    public void PlayAgain()
    {
        SceneManager.LoadScene(mainGameScene);
    }
}
