using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public int health = 30;
    public static int enemiesDefeatedCount = 0;
    public EnemyDrop enemyDrop;

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log($"{gameObject.name} recibi {amount} de dao. Vida restante: {health}");

        if (health <= 0)
        {
            enemiesDefeatedCount++;
            enemyDrop.TryDrop();
            Debug.Log($"Enemigo derrotado. Total: {enemiesDefeatedCount}");

            if (enemiesDefeatedCount == 1)
            {
                TutoSignals.Instance.ShowCassettesHint();
            }

            Destroy(gameObject);
        }
    }

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private bool isBlinded = false;

    public bool IsBlinded
    {
        get { return isBlinded; }
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    public void Blind(float duration)
    {
        if (isBlinded) return;

        isBlinded = true;
        Debug.Log(gameObject.name + " ha sido cegado.");

        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.blue;
        }

        StartCoroutine(UnblindAfterDelay(duration));
    }

    private IEnumerator UnblindAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        isBlinded = false;
        Debug.Log(gameObject.name + " ya no est cegado.");

        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;
        }
    }
}
