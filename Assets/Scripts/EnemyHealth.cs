using UnityEngine;

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
}