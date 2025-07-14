using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    public PlayerCombat.WeaponType weaponType;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            PlayerCombat combat = other.GetComponent<PlayerCombat>();
            if (combat != null)
            {
                combat.SwapWeapon(weaponType);
                Destroy(gameObject); // Desaparece el arma recogida
            }
        }
    }
}