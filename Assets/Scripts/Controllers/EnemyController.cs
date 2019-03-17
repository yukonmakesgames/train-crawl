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
    private float touchStepback = 2.5f;
    [SerializeField]
    [Range(0f, 1f)]
    private float knockbackResistance = 0f;

    [Header("Drop Pool")]
    [SerializeField]
    [Range(0f, 1f)]
    private float itemDropChance = 0.5f;
    [SerializeField]
    private WeaponObject[] drops;

    [Header("References")]
    [SerializeField]
    private Animator spriteAnimator;
    [SerializeField]
    private GameObject explosionEffects;
    [SerializeField]
    private GameObject itemPrefab;

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
        Instantiate(explosionEffects, transform.position, Quaternion.identity);

        if(drops.Length > 0)
        {
            if (Random.Range(0f, 1f) <= itemDropChance)
            {
                ItemController itemController = Instantiate(itemPrefab, transform.position, Quaternion.identity).GetComponentInChildren<ItemController>();
                itemController.SetItem(drops[Random.Range(0, drops.Length)]);
            }
        }

        Destroy(gameObject);
    }

    public void BecomeMortal()
    {
        invincible = false;
        spriteAnimator.SetBool("Invincible", false);
    }

    public void Knockback(Vector3 _position, float _knockback)
    {
        if (!invincible)
        {
            Vector3 direction = transform.position - _position;

            direction = new Vector3(direction.normalized.x, 0.5f, direction.normalized.z);
            rb.AddForce(direction.normalized * _knockback, ForceMode.Impulse);
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

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Player"))
        {
            Knockback(collision.collider.gameObject.transform.position, touchStepback);
        }
    }
}
