using UnityEngine;
using UnityEngine.SceneManagement;


/**
 * Allows restarting the game from a button.
 */
public class RestartGame : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Name of the first gameplay scene to load when restarting.")]
    private string firstSceneName = "level-1";

    // Called from the UI Button onClick event
    public void RestartGameFromBeginning()
    {
        // Just in case the game was paused
        Time.timeScale = 1f;

        if (!string.IsNullOrEmpty(firstSceneName))
        {
            SceneManager.LoadScene(firstSceneName);
        }
        else
        {
            Debug.LogWarning("RestartGame: firstSceneName is empty, please set it in the Inspector.");
        }
    }

    public void RestartCurrentScene()
    {
        Time.timeScale = 1f;
        var current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.name);
    }
}
