using UnityEngine;

public class HandController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Collider hitbox;

    private WeaponObject weaponObject;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
        hitbox = GetComponent<Collider>();

        HitUnactive();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyController enemyController = other.gameObject.GetComponent<EnemyController>();

            enemyController.TakeDamage(weaponObject.Damage, weaponObject.Stun);
            enemyController.Knockback(other.transform.position - transform.position, weaponObject.Knockback);
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
        HitActive();
    }

    public void HitActive()
    {
        hitbox.enabled = true;
    }

    public void HitUnactive()
    {
        hitbox.enabled = false;
    }
}
