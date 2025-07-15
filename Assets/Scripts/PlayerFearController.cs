using UnityEngine;
using UnityEngine.UI;

public class PlayerFearController : MonoBehaviour
{
    [Header("Configuración Base del Miedo")]
    public float baseMaxFear = 150f;
    [SerializeField] private float currentFear;

    [Header("UI del Miedo")]
    public Slider fearSlider;

    [Header("Aumento de Miedo")]
    public float darkFearIncreaseRate = 1f;
    public float damageFearIncreaseAmount = 25f;

    [Header("Disminución de Miedo")]
    public float safeZoneFearDecreaseRate = 10f;

    [Header("Capacidad Máxima Dinámica")]
    public float maxFearReductionInterval = 180f; // 3 minutos
    public float maxFearReductionPercentage = 0.10f; // 10%
    public float lanternUseToResetMaxFearTime = 10f;

    private float currentMaxFear;
    private bool isInDarkZone = false;
    private bool isInSafeZone = false;
    private bool isLanternOn = false;
    private float lanternOnTimer = 0f;
    private float maxFearReductionTimer;

    private void Awake()
    {
        currentMaxFear = baseMaxFear;
        currentFear = 0f;
        maxFearReductionTimer = maxFearReductionInterval;
    }

    private void Update()
    {
        if (isInDarkZone && !isLanternOn)
        {
            IncreaseFear(darkFearIncreaseRate * Time.deltaTime);
        }
        else if (isInSafeZone)
        {
            DecreaseFear(safeZoneFearDecreaseRate * Time.deltaTime);
        }

        if (!isLanternOn)
        {
            maxFearReductionTimer -= Time.deltaTime;
            if (maxFearReductionTimer <= 0)
            {
                ReduceMaxFearCapacity();
                maxFearReductionTimer = maxFearReductionInterval;
            }
        }
        else
        {
            lanternOnTimer += Time.deltaTime;
            if (lanternOnTimer >= lanternUseToResetMaxFearTime)
            {
                ResetMaxFearCapacity();
            }
        }

        UpdateFearUI();
        CheckFearConsequences();
    }

    public void IncreaseFear(float amount)
    {
        currentFear += amount;
        currentFear = Mathf.Clamp(currentFear, 0, currentMaxFear);
        Debug.Log("Miedo aumentado. Miedo actual: " + currentFear + "/" + currentMaxFear);
    }

    public void TakeDamageFromEnemy()
    {
        IncreaseFear(damageFearIncreaseAmount);
        Debug.Log("¡Recibiste daño! Miedo aumentado en " + damageFearIncreaseAmount);
    }

    public void DecreaseFear(float amount)
    {
        currentFear -= amount;
        currentFear = Mathf.Clamp(currentFear, 0, currentMaxFear);
        Debug.Log("Miedo disminuido. Miedo actual: " + currentFear + "/" + currentMaxFear);
    }

    public void SetInDarkZone(bool inDark)
    {
        isInDarkZone = inDark;
        if (inDark) Debug.Log("Entró en zona oscura.");
        else Debug.Log("Salió de zona oscura.");
    }

    public void SetInSafeZone(bool inSafe)
    {
        isInSafeZone = inSafe;
        if (inSafe) Debug.Log("Entró en zona segura (fogata).");
        else Debug.Log("Salió de zona segura.");
    }

    public void SetLanternState(bool on)
    {
        isLanternOn = on;
        if (!on)
        {
            lanternOnTimer = 0f;
        }
        Debug.Log("Lámpara: " + (on ? "ENCENDIDA" : "APAGADA"));
    }

    private void ReduceMaxFearCapacity()
    {
        float reductionAmount = baseMaxFear * maxFearReductionPercentage;
        currentMaxFear = Mathf.Max(currentMaxFear - reductionAmount, 25f);
        Debug.LogWarning("¡Capacidad máxima de miedo reducida! Nueva capacidad: " + currentMaxFear);

        if (currentFear > currentMaxFear)
        {
            currentFear = currentMaxFear;
        }
    }

    private void ResetMaxFearCapacity()
    {
        if (currentMaxFear < baseMaxFear)
        {
            currentMaxFear = baseMaxFear;
            Debug.Log("Capacidad máxima de miedo restaurada a: " + currentMaxFear);
            lanternOnTimer = 0f;
        }
    }

    private void UpdateFearUI()
    {
        if (fearSlider != null)
        {
            fearSlider.maxValue = currentMaxFear;
            fearSlider.value = currentFear;
        }
    }

    private void CheckFearConsequences()
    {
        if (currentFear >= currentMaxFear)
        {
            Debug.LogWarning("¡El miedo ha llegado al máximo! El jugador ha tenido un ataque de pánico");
            this.enabled = false;
        }
    }
}
