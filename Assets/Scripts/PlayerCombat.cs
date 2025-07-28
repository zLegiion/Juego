using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    public enum WeaponType { Ramita, Needleblade }
    public WeaponType currentWeapon = WeaponType.Ramita;

    public float attackRange = 1f;
    public LayerMask enemyLayer;
    public float attackCooldown = 0.5f;
    private float lastAttackTime;

    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public PlayerMovement movementController;

    public Image currentWeaponIconUI;
    public Sprite ramitaUISprite;
    public Sprite needlebladeUISprite;

    private void Start()
    {
        if (currentWeaponIconUI != null && ramitaUISprite != null)
        {
            currentWeaponIconUI.sprite = ramitaUISprite;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");

        Vector2 attackDirection = movementController.GetFacingDirection();
        Vector2 attackOrigin = (Vector2)transform.position + attackDirection;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackOrigin, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            int damage = currentWeapon == WeaponType.Ramita ? 10 : 20;
            enemy.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
        }
    }

    public void SwapWeapon(WeaponType newWeapon)
    {
        currentWeapon = newWeapon;
        Debug.Log("Arma equipada: " + currentWeapon.ToString());

        if (animator != null)
        {
            if (currentWeapon == WeaponType.Ramita)
            {
                animator.SetInteger("WeaponType", 0);
            }
            else if (currentWeapon == WeaponType.Needleblade)
            {
                animator.SetInteger("WeaponType", 1);
            }
        }

        if (currentWeaponIconUI != null)
        {
            if (currentWeapon == WeaponType.Ramita && ramitaUISprite != null)
            {
                currentWeaponIconUI.sprite = ramitaUISprite;
            }
            else if (currentWeapon == WeaponType.Needleblade && needlebladeUISprite != null)
            {
                currentWeaponIconUI.sprite = needlebladeUISprite;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (movementController == null) return;
        Vector2 dir = Application.isPlaying ? movementController.GetFacingDirection() : Vector2.right;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + dir, attackRange);
    }
}