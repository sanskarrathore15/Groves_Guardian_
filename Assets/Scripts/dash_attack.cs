using UnityEngine;

public class DroneDashAttack : MonoBehaviour
{
    public Transform player; // Reference to the player object
    public float dashForce = 5f; // Force applied to dash towards the player
    public float dashCooldown = .2f; // Cooldown time between each dash
    public float detectionRange = 10f; // Range within which the dash attack can be initiated
    public float followDistance = 1f; // Distance to maintain from the player
    public float followSpeed = 6f; // Speed at which the drone follows the player
    private Rigidbody rb;
    private bool canDash = true;
    private Vector3 dashDirection; // Cached direction for dashing

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
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    void Dash()
    {
        // Cache the direction for dashing only once
        if (canDash)
        {
            dashDirection = (player.position - transform.position).normalized;
            canDash = false;
            Invoke("EnableDash", dashCooldown); // Enable dash after the cooldown
        }

        // Perform dash attack towards the player
        rb.AddForce(dashDirection * dashForce, ForceMode.Impulse);
    }

    void FollowPlayer()
    {
        // Calculate the target position to maintain distance from the player
        Vector3 targetPosition = player.position - (player.forward * followDistance);

        // Apply y-axis constraint only when not dashing
        if (!canDash && transform.position.y < .5f)
        {
            Vector3 move = new Vector3(transform.position.x, 2.5f, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, move, 8f * Time.deltaTime);
            if (transform.position.y < 0.3f)
            {
                rb.velocity = Vector3.zero;
            }
        }

        // Smoothly move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // Smoothly rotate to look at the player
        Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 8f * Time.deltaTime);
    }

    void EnableDash()
    {
        canDash = true;
    }
}
