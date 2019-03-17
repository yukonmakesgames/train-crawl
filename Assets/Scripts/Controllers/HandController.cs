using UnityEngine;

public class HandController : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    private Vector3 slashOffset;

    [Header("References")]
    [SerializeField]
    private GameObject slashPrefab;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private PlayerController playerController;

    private WeaponObject weaponObject;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
        playerController = GetComponentInParent<PlayerController>();
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

    public void Hit()
    {
        SlashController slashController = Instantiate(slashPrefab, transform).GetComponent<SlashController>();
        slashController.transform.localPosition += slashOffset;
        slashController.Setup(weaponObject, GetComponentInParent<Rigidbody>().velocity);

        playerController.JuicePlayer(weaponObject.JuiceCost);
    }
}
