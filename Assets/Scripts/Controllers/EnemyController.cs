using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField]
    private float health = 1f;
    [SerializeField]
    private float touchDamage = 0.25f;
    [SerializeField]
    private float touchKnockback = 5f;
    [SerializeField]
    [Range(0f, 1f)]
    private float knockbackResistance = 0f;

    [Header("References")]
    [SerializeField]
    private Animator spriteAnimator;

    [HideInInspector]
    public bool invincible = false;
    private float currentHealth;

    [HideInInspector]
    public Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        currentHealth = health;
    }

    public void TakeDamage(float _damage, float _invincibleTime)
    {
        if(!invincible)
        {
            currentHealth -= _damage;

            if (currentHealth <= 0f)
            {
                Die();
            }
            else
            {
                invincible = true;
                spriteAnimator.SetBool("Invincible", true);

                Invoke("BecomeMortal", _invincibleTime);
            }
        }
    }

    public void Die()
    {
        //TODO explosion
        Destroy(gameObject);
    }

    public void BecomeMortal()
    {
        invincible = false;
        spriteAnimator.SetBool("Invincible", false);
    }

    public void Knockback(Vector3 _direction, float _knockback)
    {
        if (!invincible)
        {
            _direction = new Vector3(_direction.normalized.x, 0.5f, _direction.normalized.z);

            rb.AddForce(_direction.normalized * _knockback, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.CompareTag("Player"))
        {
            collision.collider.gameObject.GetComponent<PlayerController>().JuicePlayer(-touchDamage);
            collision.collider.gameObject.GetComponent<PlayerController>().Knockback(transform.position, touchKnockback);
        }
    }
}
