using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject SettingsMenu;
    public AudioSource musicSource;  // Fuente de música de fondo
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        PauseMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        Time.timeScale = 1f;
        musicSource.UnPause();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
    }

    public void Pause()
    {
        PauseMenu.SetActive(true);
        SettingsMenu.SetActive(false);
        Time.timeScale = 0f;
        musicSource.Pause();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        isPaused = true;
    }

    public void OpenOptionsPanel()
    {
        PauseMenu.SetActive(false);
        SettingsMenu.SetActive(true);
    }

    public void OpenSettingsPanel()
    {
        PauseMenu.SetActive(true);
        SettingsMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Saliendo del juego");
        Application.Quit();
    }

    public void MainMenu()
    {
        // Asegúrate de que el menú principal esté en la escena 1
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
}
