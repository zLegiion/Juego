using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class DeathVideoHandler : MonoBehaviour
{
    [Header("Configuración del Video de Muerte")]
    [SerializeField] private VideoPlayer deathVideoPlayer;
    [SerializeField] private GameObject playerUI;

    [Header("Acción Post-Video: Reiniciar Nivel")]
    [Tooltip("El GameObject del jugador que contiene PlayerMovement, PlayerFearController, etc.")]
    [SerializeField] private GameObject playerGameObject;
    [Header("Objetos a Ocultar Durante el Video")]
    [Tooltip("Arrastra aquí los GameObjects (ej. enemigos, objetos del nivel) que deben desaparecer durante el video de muerte.")]
    [SerializeField] private List<GameObject> objectsToHideDuringDeathVideo;

    private PlayerMovement playerMovement;
    private PlayerFearController playerFearController;
    private PlayerCombat playerCombat;
    private SpriteRenderer playerSpriteRenderer;
    private Collider2D playerCollider;
    private Rigidbody2D playerRigidbody;

    private bool isVideoPlaying = false;

    void Start()
    {
        if (deathVideoPlayer == null)
        {
            return;
        }

        deathVideoPlayer.loopPointReached += OnVideoFinished;
        deathVideoPlayer.gameObject.SetActive(false);

        DeathManager.OnPlayerDeath += HandlePlayerDeath;

        if (playerGameObject == null)
        {
            playerGameObject = GameObject.FindGameObjectWithTag("Player");
            if (playerGameObject == null)
            {
                return;
            }
        }

        playerMovement = playerGameObject.GetComponent<PlayerMovement>();
        playerFearController = playerGameObject.GetComponent<PlayerFearController>();
        playerCombat = playerGameObject.GetComponent<PlayerCombat>();
        playerSpriteRenderer = playerGameObject.GetComponent<SpriteRenderer>();
        playerCollider = playerGameObject.GetComponent<Collider2D>();
        playerRigidbody = playerGameObject.GetComponent<Rigidbody2D>();

        if (playerMovement == null || playerFearController == null || playerCombat == null || playerSpriteRenderer == null || playerCollider == null || playerRigidbody == null)
        {

        }
    }

    private void HandlePlayerDeath()
    {
        if (isVideoPlaying) return;

        isVideoPlaying = true;

        if (playerMovement != null) playerMovement.enabled = false;
        if (playerCombat != null) playerCombat.enabled = false;
        if (playerRigidbody != null)
        {
            playerRigidbody.linearVelocity = Vector2.zero;
            playerRigidbody.bodyType = RigidbodyType2D.Kinematic;
        }
        if (playerSpriteRenderer != null) playerSpriteRenderer.enabled = false;
        if (playerCollider != null) playerCollider.enabled = false;

        if (playerUI != null)
        {
            playerUI.SetActive(false);
        }

        SetObjectsActive(false);

        if (deathVideoPlayer != null)
        {
            deathVideoPlayer.gameObject.SetActive(true);
            deathVideoPlayer.Play();
        }
        else
        {
            RestartLevelAtCheckpoint();
        }
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        if (deathVideoPlayer != null)
        {
            deathVideoPlayer.gameObject.SetActive(false);
        }

        RestartLevelAtCheckpoint();

        isVideoPlaying = false;
    }

    private void RestartLevelAtCheckpoint()
    {
        if (playerMovement != null) playerMovement.enabled = true;
        if (playerCombat != null) playerCombat.enabled = true;
        if (playerRigidbody != null)
        {
            playerRigidbody.bodyType = RigidbodyType2D.Kinematic;
            playerMovement.Kill();
            playerRigidbody.bodyType = RigidbodyType2D.Dynamic;
        }
        else if (playerMovement != null)
        {
            playerMovement.Kill();
        }

        if (playerSpriteRenderer != null) playerSpriteRenderer.enabled = true;
        if (playerCollider != null) playerCollider.enabled = true;

        if (playerFearController != null)
        {
            playerFearController.ResetFear();
        }

        if (playerUI != null)
        {
            playerUI.SetActive(true);
        }

        SetObjectsActive(true);
    }

    private void SetObjectsActive(bool active)
    {
        if (objectsToHideDuringDeathVideo != null)
        {
            foreach (GameObject obj in objectsToHideDuringDeathVideo)
            {
                if (obj != null)
                {
                    obj.SetActive(active);
                }
            }
        }
    }

    void OnDestroy()
    {
        if (deathVideoPlayer != null)
        {
            deathVideoPlayer.loopPointReached -= OnVideoFinished;
        }
        DeathManager.OnPlayerDeath -= HandlePlayerDeath;
    }
}