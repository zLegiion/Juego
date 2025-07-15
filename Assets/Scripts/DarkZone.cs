using UnityEngine;

public class ZoneTrigger : MonoBehaviour
{
    // Enum para definir el tipo de zona
    public enum ZoneType
    {
        Dark,
        Safe
    }

    [Header("Configuración de la Zona")]
    public ZoneType zoneType; // Selecciona si es una zona Oscura o Segura
    private PlayerFearController playerFearController;

    private void Start()
    {
        playerFearController = FindAnyObjectByType<PlayerFearController>();
        if (playerFearController == null)
        {
            Debug.LogError($"ZoneTrigger ({zoneType.ToString()}): PlayerFearController no encontrado en la escena.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && playerFearController != null)
        {
            if (zoneType == ZoneType.Dark)
            {
                playerFearController.SetInDarkZone(true);
            }
            else if (zoneType == ZoneType.Safe)
            {
                playerFearController.SetInSafeZone(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && playerFearController != null)
        {
            if (zoneType == ZoneType.Dark)
            {
                playerFearController.SetInDarkZone(false);
            }
            else if (zoneType == ZoneType.Safe)
            {
                playerFearController.SetInSafeZone(false);
            }
        }
    }
}