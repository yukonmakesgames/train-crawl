using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float turnSpeed;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.MoveRotation(Quaternion.Euler(0f, rb.rotation.eulerAngles.y + Input.GetAxisRaw("Horizontal") * turnSpeed, 0f));

        Vector3 movement = (Input.GetAxisRaw("Vertical") * movementSpeed) * transform.forward;
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
    }
}
