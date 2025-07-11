using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject SettingsMenu;
    public AudioSource musicSource;  // Fuente de música de fondo
    private bool isPaused = false;
    void Start()
    {
        PauseMenu.SetActive(false);
        SettingsMenu.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SettingsMenu.activeSelf)
            {
                // Si estamos en settings, volver al menú de pausa
                BackToPauseMenu();
            }
            else if (PauseMenu.activeSelf)
            {
                // Si ya está el menú de pausa abierto, cerrar todo
                Resume();
            }
            else
            {
                // Si no hay menús abiertos, pausar el juego
                Pause();
            }
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

    public void BackToPauseMenu()
    {
        PauseMenu.SetActive(true);
        SettingsMenu.SetActive(false);
    }

    public void OpenSettingsPanel()
    {
        PauseMenu.SetActive(false);
        SettingsMenu.SetActive(true);
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
