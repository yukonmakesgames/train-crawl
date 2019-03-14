using UnityEngine;

public class HandController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public void UpdateWeapon(WeaponObject _weaponObject)
    {
        spriteRenderer.sprite = _weaponObject.Sprite;
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
    }
}
