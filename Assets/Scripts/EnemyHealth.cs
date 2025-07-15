using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public int health = 30;

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log($"{gameObject.name} recibió {amount} de daño. Vida restante: {health}");

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Habilidad de la linterna / Ceguera
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private bool isBlinded = false;

    //inicia el sprite
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    // Ciega al enemigo
    public void Blind(float duration)
    {
        if (isBlinded) return;

        isBlinded = true;
        Debug.Log(gameObject.name + " ha sido cegado.");

        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.blue; // Efecto visual de ceguera
        }

        StartCoroutine(UnblindAfterDelay(duration));
    }

    // Quita la ceguera después de un tiempo
    private IEnumerator UnblindAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        isBlinded = false;
        Debug.Log(gameObject.name + " ya no está cegado.");

        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;
        }
    }
}