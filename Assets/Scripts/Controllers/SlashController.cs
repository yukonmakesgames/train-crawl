using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashController : MonoBehaviour
{
    private WeaponObject weaponObject;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        AudioManager.Instance.Play("Attack");
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

    public void Setup(WeaponObject _weaponObject, Vector3 _velocity)
    {
        weaponObject = _weaponObject;

        transform.localScale = weaponObject.SlashSize;

        rb.AddForce(transform.forward * weaponObject.SlashSpeed + _velocity, ForceMode.Impulse);
    }

    public void Done()
    {
        Destroy(gameObject);
    }
}
