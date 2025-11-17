using UnityEngine;

/**
 * Simple pause system:
 * - Press Esc or P to toggle pause.
 * - When paused, Time.timeScale = 0 and a "PAUSED" UI text is shown.
 */
public class PauseManager : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseTextObject;

    private bool isPaused = false;

    private void Start()
    {
        // Make sure the game starts unpaused
        Time.timeScale = 1f;

        if (pauseTextObject != null)
        {
            pauseTextObject.SetActive(false);
        }
    }

    private void Update()
    {
        // Toggle pause with Escape or P
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        isPaused = !isPaused;

        // Freeze or resume time
        Time.timeScale = isPaused ? 0f : 1f;

        // Show/hide the "PAUSED" UI
        if (pauseTextObject != null)
        {
            pauseTextObject.SetActive(isPaused);
        }
    }
}
