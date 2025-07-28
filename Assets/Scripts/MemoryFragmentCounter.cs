using TMPro;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class MemoryFragmentCounter : MonoBehaviour
{
    public TextMeshProUGUI fragmentText;
    public VideoPlayer cinematicVideoPlayer;

    public GameObject playerCharacter;
    public GameObject gameUI;
    public GameObject additionalScoreUI;

    private int fragmentsCollected = 0;
    private int maxFragments = 3;

    void Start()
    {
        UpdateUI();
        if (cinematicVideoPlayer != null)
        {
            cinematicVideoPlayer.Stop();
            cinematicVideoPlayer.loopPointReached += OnVideoEnd;
        }
    }

    void OnDestroy()
    {
        if (cinematicVideoPlayer != null)
        {
            cinematicVideoPlayer.loopPointReached -= OnVideoEnd;
        }
    }

    public void AddFragment()
    {
        if (fragmentsCollected < maxFragments)
        {
            fragmentsCollected++;
            UpdateUI();

            if (fragmentsCollected == maxFragments)
            {
                PlayCinematicVideo();
            }
        }
    }

    private void UpdateUI()
    {
        fragmentText.text = $"Memory Fragments collected: {fragmentsCollected}/{maxFragments}";
    }

    private void PlayCinematicVideo()
    {
        if (cinematicVideoPlayer != null)
        {
            if (cinematicVideoPlayer.clip != null || !string.IsNullOrEmpty(cinematicVideoPlayer.url))
            {
                if (playerCharacter != null)
                {
                    playerCharacter.SetActive(false);
                }
                if (gameUI != null)
                {
                    gameUI.SetActive(false);
                }
                if (additionalScoreUI != null)
                {
                    additionalScoreUI.SetActive(false);
                }

                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (GameObject enemy in enemies)
                {
                    enemy.SetActive(false);
                }

                cinematicVideoPlayer.gameObject.SetActive(true);
                cinematicVideoPlayer.Play();
            }
        }
    }
    //lo pongo provisional por que no se que debe ser despues del video.
    void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene(1);
    }
}