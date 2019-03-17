using UnityEngine;

public class ChaseEnemyController : EnemyController
{
    [Header("Chase")]
    [SerializeField]
    private float speed;
    [SerializeField]
    [Range(0f, 1f)]
    private float acceleration;

    private GameObject playerObject;

    private void FixedUpdate()
    {
        if(playerObject == null)
        {
            playerObject = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
            if(!invincible)
            {
                Vector3 calculatedVelocity = Vector3.Normalize(playerObject.transform.position - transform.position) * speed;
                Vector3 targetVelocity = new Vector3(calculatedVelocity.x, rb.velocity.y, calculatedVelocity.z);

                rb.velocity = Vector3.Lerp(rb.velocity, targetVelocity, acceleration);
            }   
        }
    }
}
