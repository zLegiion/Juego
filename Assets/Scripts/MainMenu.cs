using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    
    public void Quitgame()
    {
        Application.Quit();
        Debug.Log("Juego Cerrado Con Exito");
    }

    public GameObject creditsPanel;
    public GameObject mainMenuPanel;
 
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
