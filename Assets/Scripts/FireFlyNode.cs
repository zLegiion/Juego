using UnityEngine;
using System.Collections; // Necesario para Coroutines

// Estos atributos aseguran que el GameObject siempre tendrá estos componentes
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class FireflyNode : MonoBehaviour
{
    public int firefliesPerCollection = 5; // Luciernagas x Nodo
    public float respawnTime = 60; // El Cooldown

    private Collider2D nodeCollider;
    private SpriteRenderer nodeRenderer;
    private bool isCollected = false;

    // ¡¡¡FALTABA ESTE MÉTODO AWAKE!!!
    private void Awake()
    {
        nodeCollider = GetComponent<Collider2D>();
        nodeRenderer = GetComponent<SpriteRenderer>();
        // No necesitamos comprobaciones null aquí gracias a [RequireComponent]
        SetNodeActive(true); // Asegurarse de que esté activo al inicio
    }

    // Este método será llamado por el script del jugador cuando interactúe
    public int Collect()
    {
        if (!isCollected)
        {
            SetNodeActive(false);
            Debug.Log("Luciérnaga Recolectada");
            StartCoroutine(RespawnTimer());
            return firefliesPerCollection;
        }
        return 0; // Ya ha sido recolectada
    }

    private void SetNodeActive(bool active)
    {
        isCollected = !active;
        // Ya no necesitamos comprobaciones null aquí gracias a [RequireComponent]
        nodeCollider.enabled = active; // Activa/desactiva el collider
        nodeRenderer.enabled = active; // Activa/desactiva el renderizado del sprite
    }

    private IEnumerator RespawnTimer()
    {
        yield return new WaitForSeconds(respawnTime);
        SetNodeActive(true);
        Debug.Log("Luciérnaga reaparecida!");
    }
}