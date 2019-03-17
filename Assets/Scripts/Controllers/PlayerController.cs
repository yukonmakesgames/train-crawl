using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Juice")]
    [SerializeField]
    private float juiceMax = 1f;
    [SerializeField]
    private float juiceCooldown = 1f;
    [SerializeField]
    private float juiceRegen = 0.125f;

    [Header("Properties")]
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float turnSpeed;
    [SerializeField]
    private float groundCheckDistance;
    [SerializeField]
    private float itemYeetForce = 1f;
    [SerializeField]
    private float itemSideYeetForceMax = 0.5f;

    [Header("Hands")]
    [SerializeField]
    private HandController hand;

    [Header("Reference")]
    [SerializeField]
    private UIManager uiManager;
    [SerializeField]
    private GameObject itemPrefab;

    [HideInInspector]
    public float juice = 1f;
    private bool juiced = false;

    private Rigidbody rb;
    private Animator animator;
    private float currentJuiceTime = 0;
    private bool chillBro = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        UpdateHands();
    }

    private void Update()
    {
        if(juice > 0f && !juiced && !chillBro)
        {
            if (Input.GetButtonDown("Attack"))
            {
                hand.Attack();
            }
        }

        if(juice < juiceMax)
        {
            if(juice <= 0f)
            {
                if(!juiced)
                {
                    juiced = true;

                    juice = 0f;

                    animator.SetBool("Dead", true);

                    hand.Die();
                }   
            }
            else
            {
                if(Time.time > currentJuiceTime)
                {
                    juice += juiceRegen * Time.deltaTime;
                }
            }
        }
        else if(juice > juiceMax)
        {
            juice = juiceMax;
        }
    }

    private void FixedUpdate()
    {
        if(!juiced && !chillBro)
        {
            rb.MoveRotation(Quaternion.Euler(0f, rb.rotation.eulerAngles.y + Input.GetAxisRaw("Horizontal") * turnSpeed, 0f));

            if (Input.GetAxisRaw("Vertical") != 0)
            {
                if (Physics.Raycast(transform.position + Vector3.up * groundCheckDistance, Vector3.down, groundCheckDistance * 2f))
                {
                    Vector3 idealVelocity = ((Input.GetAxisRaw("Vertical") * movementSpeed) * transform.forward);
                    Vector3 targetVelocity = new Vector3(idealVelocity.x, rb.velocity.y, idealVelocity.z);
                    rb.AddForce(targetVelocity - rb.velocity, ForceMode.VelocityChange);
                }
                else
                {
                    Debug.DrawRay(transform.position + Vector3.up * groundCheckDistance, Vector3.down, Color.red);
                }
            }
        }
    }

    private void UpdateHands()
    {
        if(GameManager.Instance.Weapon != null)
        {
            hand.gameObject.SetActive(true);
            hand.UpdateWeapon(GameManager.Instance.Weapon);
        }
        else
        {
            hand.gameObject.SetActive(false);
        }
    }

    public void JuicePlayer(float _juiceAmount)
    {
        juice += _juiceAmount;
        juice = Mathf.Clamp(juice, 0f, juiceMax);

        currentJuiceTime = Time.time + juiceCooldown;
    }

    public void Knockback(Vector3 _position, float _knockback)
    {
        if(!juiced && !chillBro)
        {
            Vector3 direction = transform.position - _position;

            direction = new Vector3(direction.normalized.x, 0.5f, direction.normalized.z);
            rb.AddForce(direction.normalized * _knockback, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Killzone"))
        {
            juice = 0f;
            DeathScreen(true);
        }

        if (other.CompareTag("Goal"))
        {
            GameManager.Instance.NextCar(gameObject.scene);
            chillBro = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Item"))
        {
            if(Input.GetButtonUp("Cancel"))
            {
                GameObject newItem = Instantiate(itemPrefab, transform.position, Quaternion.identity);
                newItem.GetComponentInChildren<ItemController>().SetItem(GameManager.Instance.Weapon);
                newItem.GetComponent<Rigidbody>().AddForce(rb.velocity + ((transform.forward + Vector3.up) * itemYeetForce) + (transform.right * Random.Range(-itemSideYeetForceMax, itemSideYeetForceMax)), ForceMode.Impulse);

                GameManager.Instance.Weapon = other.GetComponent<ItemController>().Pickup();
                UpdateHands();
            }
        }
    }

    public void DeathScreen(bool _instant)
    {
        if(uiManager.gameState != GameStateType.IdkProbablyDead)
        {
            uiManager.ChangeState(GameStateType.IdkProbablyDead, _instant);
        }
    }

    public void Death()
    {
        DeathScreen(false);
    }
}
