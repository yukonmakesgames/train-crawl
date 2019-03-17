using UnityEngine;

public class HandController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Collider hitbox;
    private PlayerController playerController;

    private WeaponObject weaponObject;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
        hitbox = GetComponent<Collider>();
        playerController = GetComponentInParent<PlayerController>();

        HitUnactive();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyController enemyController = other.gameObject.GetComponent<EnemyController>();

            enemyController.Knockback(transform.position, weaponObject.Knockback);
            enemyController.TakeDamage(weaponObject.Damage, weaponObject.Stun);
        }
    }

    public void UpdateWeapon(WeaponObject _weaponObject)
    {
        weaponObject = _weaponObject;

        spriteRenderer.sprite = weaponObject.Sprite;
        animator.SetFloat("Speed", weaponObject.Speed);
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
    }

    public void Die()
    {
        animator.SetTrigger("Dead");
    }

    public void HitActive()
    {
        hitbox.enabled = true;
        playerController.JuicePlayer(weaponObject.JuiceCost);
    }

    public void HitUnactive()
    {
        hitbox.enabled = false;
    }
}
