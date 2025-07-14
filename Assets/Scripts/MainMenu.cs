using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private GameObject video;
    [SerializeField] public string sceneToLoad;


    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject mainMenuPanel;

    //Método para iniciar el juego o cargar una escena específica.

    public void Start()
    {
        video.SetActive(false);
    }
    public void PlayGame(int sceneIndex)
    {
        //SceneManager.LoadScene(sceneIndex);
        mainMenuPanel.SetActive(false);
        video.SetActive(true);
        videoPlayer.Play();
        videoPlayer.loopPointReached += OnVideoEnd;

    }

    void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    // Método para salir del juego.
    public void Quitgame()
    {
        Application.Quit();
        Debug.Log("Juego Cerrado Con Exito");
    }

    

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
