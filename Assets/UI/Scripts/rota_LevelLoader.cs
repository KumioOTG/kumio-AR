using UnityEngine;
using UnityEngine.SceneManagement;

public class rota_LevelLoader : MonoBehaviour
{
    private int clickCount = 0; // Counter for button clicks
    private float lastClickTime = 0; // Time of the last click
    private float maxTimeBetweenClicks = 0.5f; // Maximum allowed time between clicks

    // This method will be called from the button's click event
    public void LoadLevelByIndex(int sceneIndex)
    {
        float currentTime = Time.time;
        float timeSinceLastClick = currentTime - lastClickTime;

        // If time since last click is too long, reset click count
        if (timeSinceLastClick > maxTimeBetweenClicks)
        {
            clickCount = 0;
        }

        // Register the click
        clickCount++;

        // Check if there have been 3 rapid clicks
        if (clickCount == 3)
        {
            // Reset the click count
            clickCount = 0;

            // Load the specified scene by its build index
            SceneManager.LoadScene(sceneIndex);
        }

        // Update the last click time
        lastClickTime = currentTime;
    }
}
