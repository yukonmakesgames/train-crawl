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
    private float juiceRegen = 0.125f;

    [Header("Properties")]
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float turnSpeed;
    [SerializeField]
    private float groundCheckDistance;

    [Header("Hands")]
    [SerializeField]
    private HandController[] hands;

    [Header("Items")]
    [SerializeField]
    private WeaponObject[] weapons;

    [HideInInspector]
    public float juice = 1f;
    private bool juiced = false;

    private Rigidbody rb;
    private Animator animator;

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
        if(juice > 0f)
        {
            if (Input.GetButtonDown("RightHand"))
            {
                hands[0].Attack();
            }

            if (Input.GetButtonDown("LeftHand"))
            {
                hands[1].Attack();
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

                    foreach (HandController hand in hands)
                    {
                        hand.Die();
                    }
                }   
            }
            else
            {
                juice += juiceRegen * Time.deltaTime;
            }
        }
        else if(juice > juiceMax)
        {
            juice = juiceMax;
        }
    }

    private void FixedUpdate()
    {
        rb.MoveRotation(Quaternion.Euler(0f, rb.rotation.eulerAngles.y + Input.GetAxisRaw("Horizontal") * turnSpeed, 0f));

        if(Input.GetAxisRaw("Vertical") != 0)
        {
            if(Physics.Raycast(transform.position + Vector3.up * groundCheckDistance, Vector3.down, groundCheckDistance * 2f))
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

    private void UpdateHands()
    {
        for(var i = 0; i < hands.Length; i++)
        { 
            if(i < weapons.Length)
            {
                hands[i].gameObject.SetActive(true);
                hands[i].UpdateWeapon(weapons[i]);
            }
            else
            {
                hands[i].gameObject.SetActive(false);
            }
        }
    }

    public void JuicePlayer(float _juiceAmount)
    {
        juice += _juiceAmount;
        juice = Mathf.Clamp(juice, 0f, juiceMax);
    }

    public void Knockback(Vector3 _position, float _knockback)
    {
        Vector3 direction = transform.position - _position;

        direction = new Vector3(direction.normalized.x, 0.5f, direction.normalized.z);
        rb.AddForce(direction.normalized * _knockback, ForceMode.Impulse);
    }
}
