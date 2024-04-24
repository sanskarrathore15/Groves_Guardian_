using UnityEngine;

public class pauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;

     void Start()
    {pauseMenuUI.SetActive(false);
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f; // Pause the game by setting time scale to 0
        pauseMenuUI.SetActive(true); // Activate the pause menu UI
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f; // Resume the game by setting time scale back to 1
        pauseMenuUI.SetActive(false); // Deactivate the pause menu UI
    }
}
