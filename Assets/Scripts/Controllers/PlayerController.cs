using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float turnSpeed;

    [Header("Hands")]
    [SerializeField]
    private HandController[] hands;

    [Header("Items")]
    [SerializeField]
    private WeaponObject[] weapons;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        UpdateHands();
    }

    private void Update()
    {
        if(Input.GetButtonDown("RightHand"))
        {
            hands[0].Attack();
        }

        if (Input.GetButtonDown("LeftHand"))
        {
            hands[1].Attack();
        }
    }

    private void FixedUpdate()
    {
        rb.MoveRotation(Quaternion.Euler(0f, rb.rotation.eulerAngles.y + Input.GetAxisRaw("Horizontal") * turnSpeed, 0f));

        Vector3 movement = (Input.GetAxisRaw("Vertical") * movementSpeed) * transform.forward;
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
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
}
