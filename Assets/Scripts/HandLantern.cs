using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.Universal;

public class HandLantern : MonoBehaviour
{
    public PlayerFearController playerFearController;
    public KeyCode activateKey = KeyCode.T;
    public KeyCode specialAbilityKey = KeyCode.B;
    public float lanternDuration = 300f; // Ajustar a 300f para 5 minutos
    public int fireflySlotID = 1;

    public GameObject lanternLightObject;
    public float blindDuration = 3;

    private ToolbarManager toolbarManager;
    private bool isLanternOn = false;
    private float currentLanternTimer = 0;
    private Light2D lanternLight2D;

    private void Start()
    {
        toolbarManager = FindAnyObjectByType<ToolbarManager>();
        if (toolbarManager == null)
        {
            Debug.LogError("ToolbarManager no encontrado.");
        }

        if (playerFearController == null)
        {
            playerFearController = FindAnyObjectByType<PlayerFearController>();
            if (playerFearController == null)
            {
                Debug.LogError("PlayerFearController no encontrado.");
            }
        }

        if (lanternLightObject != null)
        {
            lanternLight2D = lanternLightObject.GetComponent<Light2D>();
            if (lanternLight2D == null)
            {
                Debug.LogWarning("El objeto de luz no tiene Light2D.");
            }
        }
        else
        {
            Debug.LogWarning("No se ha asignado GameObject para la luz.");
        }

        SetLanternState(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(activateKey))
        {
            ToggleLantern();
        }

        if (isLanternOn)
        {
            currentLanternTimer -= Time.deltaTime;
            if (currentLanternTimer <= 0)
            {
                Debug.Log("La lámpara se ha quedado sin tiempo.");
                SetLanternState(false);
            }
        }

        if (Input.GetKeyDown(specialAbilityKey))
        {
            ActivateFlashAbility();
        }
    }

    public void ToggleLantern()
    {
        if (isLanternOn)
        {
            SetLanternState(false);
            Debug.Log("Lámpara de mano apagada.");
        }
        else
        {
            if (toolbarManager != null)
            {
                if (toolbarManager.GetQuantityInSlot(fireflySlotID) >= 1)
                {
                    toolbarManager.RemoveQuantityFromSlot(fireflySlotID, 1);
                    SetLanternState(true);
                    Debug.Log("Lámpara de mano encendida. Luciérnaga consumida.");
                }
                else
                {
                    Debug.Log("No tienes luciérnagas.");
                }
            }
            else
            {
                Debug.LogWarning("ToolbarManager no encontrado.");
            }
        }
    }

    private void SetLanternState(bool state)
    {
        isLanternOn = state;
        if (lanternLightObject != null)
        {
            lanternLightObject.SetActive(state);
        }
        if (lanternLight2D != null)
        {
            lanternLight2D.enabled = state;
        }

        if (playerFearController != null)
        {
            playerFearController.SetLanternState(state);
        }

        if (state)
        {
            currentLanternTimer = lanternDuration;
        }
        else
        {
            currentLanternTimer = 0;
        }
    }

    private void ActivateFlashAbility()
    {
        if (isLanternOn)
        {
            Debug.Log("¡Destello de lámpara activado!");
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 5f);
            foreach (Collider2D enemyCollider in hitEnemies)
            {
                if (enemyCollider.CompareTag("Enemy"))
                {
                    EnemyHealth enemyAI = enemyCollider.GetComponent<EnemyHealth>();
                    if (enemyAI != null)
                    {
                        enemyAI.Blind(blindDuration); 
                    }
                }
            }
        }
        else
        {
            Debug.Log("La lámpara debe estar encendida para usar el destello.");
        }
    }
}