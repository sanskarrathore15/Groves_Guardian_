using UnityEngine;

public class DroneDashAttack : MonoBehaviour
{
    public Transform player; // Reference to the player object
    public float dashForce = 10f; // Force applied to dash towards the player
    public float dashCooldown = .2f; // Cooldown time between each dash
    public float detectionRange = 10f; // Range within which the dash attack can be initiated
    public float followDistance = 3f; // Distance to maintain from the player
    public float followSpeed = 5f; // Speed at which the drone follows the player
    private float force;
    private Rigidbody rb;
    private bool canDash = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (canDash && distanceToPlayer <= detectionRange)
        {
            Dash();
        }
        else if (!canDash)
        {
            FollowPlayer();
        }
    }

    void Dash()
    {
        // Perform dash attack towards the player
        Vector3 direction = (player.position - transform.position).normalized;
        rb.AddForce(direction * dashForce, ForceMode.Impulse);
        canDash = false;
        Invoke("EnableDash", dashCooldown); // Enable dash after the cooldown
    }

    void FollowPlayer()
    {
        // Calculate the target position to maintain distance from the player
        Vector3 targetPosition = player.position - (player.forward * followDistance);

        // Smoothly move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);
      
        // Rotate to look at the player
        transform.LookAt(player);
    }

    void EnableDash()
    {
        canDash = true;
    }
}
