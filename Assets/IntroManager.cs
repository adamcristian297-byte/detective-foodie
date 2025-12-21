using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public Image blackImage;          // Fullscreen black UI Image
    public float fadeSpeed = 1f;      // Fade speed
    public string sceneToLoad = "StoryScene";

    private bool fadeFinished = false;

    void Start()
    {
        // Start fully black
        Color c = blackImage.color;
        c.a = 1f;
        blackImage.color = c;
    }

    void Update()
    {
        // Fade in
        if (!fadeFinished)
        {
            Color c = blackImage.color;
            c.a -= fadeSpeed * Time.deltaTime;

            if (c.a <= 0f)
            {
                c.a = 0f;
                fadeFinished = true;
            }

            blackImage.color = c;
        }
        // Press any key to start
        else if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
