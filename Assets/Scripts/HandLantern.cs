using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.Universal;

public class HandLantern : MonoBehaviour
{
    public PlayerFearController playerFearController;
    public KeyCode activateKey = KeyCode.T;
    public KeyCode specialAbilityKey = KeyCode.B;
    public float lanternDuration = 300f;
    public int fireflySlotID = 1;

    public GameObject lanternLightObject;
    public float blindDuration = 3;

    public float flashAbilityRange = 2f;
    public float flashIntensity = 5f;
    public float flashDuration = 0.5f;
    public float flashCooldown = 5f;

    private ToolbarManager toolbarManager;
    private bool isLanternOn = false;
    private float currentLanternTimer = 0;
    private Light2D lanternLight2D;
    private float originalLanternIntensity;
    private float nextFlashTime = 0f;

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
            else
            {
                originalLanternIntensity = lanternLight2D.intensity;
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
                Debug.Log("La lmpara se ha quedado sin tiempo.");
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
            Debug.Log("Lmpara de mano apagada.");
        }
        else
        {
            if (toolbarManager != null)
            {
                if (toolbarManager.GetQuantityInSlot(fireflySlotID) >= 1)
                {
                    toolbarManager.RemoveQuantityFromSlot(fireflySlotID, 1);
                    SetLanternState(true);
                    Debug.Log("Lmpara de mano encendida. Lucirnaga consumida.");
                }
                else
                {
                    Debug.Log("No tienes lucirnagas.");
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
            lanternLight2D.intensity = state ? originalLanternIntensity : 0;
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
        if (isLanternOn && Time.time >= nextFlashTime)
        {
            Debug.Log("Destello de lmpara activado!");
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, flashAbilityRange);
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

            if (lanternLight2D != null)
            {
                StartCoroutine(FlashLightEffect());
            }

            nextFlashTime = Time.time + flashCooldown;
        }
        else if (isLanternOn && Time.time < nextFlashTime)
        {
            Debug.Log($"Destello en cooldown. Tiempo restante: {nextFlashTime - Time.time:F1} segundos.");
        }
        else
        {
            Debug.Log("La lmpara debe estar encendida para usar el destello.");
        }
    }

    private IEnumerator FlashLightEffect()
    {
        float timer = 0f;
        float startIntensity = lanternLight2D.intensity;

        while (timer < flashDuration / 2)
        {
            timer += Time.deltaTime;
            lanternLight2D.intensity = Mathf.Lerp(startIntensity, flashIntensity, timer / (flashDuration / 2));
            yield return null;
        }

        timer = 0f;
        float peakIntensity = lanternLight2D.intensity;
        while (timer < flashDuration / 2)
        {
            timer += Time.deltaTime;
            lanternLight2D.intensity = Mathf.Lerp(peakIntensity, originalLanternIntensity, timer / (flashDuration / 2));
            yield return null;
        }

        lanternLight2D.intensity = originalLanternIntensity;
    }
}