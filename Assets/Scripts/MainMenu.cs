using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Método para iniciar el juego o cargar una escena específica.
    public void PlayGame(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // Método para salir del juego.
    public void Quitgame()
    {
        Application.Quit();
        Debug.Log("Juego Cerrado Con Exito");
    }

    public GameObject creditsPanel;
    public GameObject mainMenuPanel;

    // Método para mostrar el panel de créditos y ocultar el menú principal.
    public void ShowCredits()
    {
        if (creditsPanel != null)
        {
            creditsPanel.SetActive(true); 
        }
        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(false); 
        }
    }

    // Método para ocultar el panel de créditos y mostrar el menú principal.
    public void HideCredits()
    {
        if (creditsPanel != null)
        {
            creditsPanel.SetActive(false); 
        }
        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(true); 
        }
    }
}
