using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float moveSpeed = 1.5f; // Base speed when out of attack range
    public float attackSpeed = 2f; // Reduced speed when in attack range
    public float attackRange = 1.5f; // Distance at which the enemy attacks the player
    public float followRange = 5f; // Distance at which the enemy starts following the player

    private Animator animator;
    private bool isAttacking = false;
    private float defaultYPosition;

    void Start()
    {
        animator = GetComponent<Animator>();
        defaultYPosition = transform.position.y;
    }

    void Update()
    {
        // Check distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= followRange)
        {
            // Rotate towards the player's direction
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

            // Set movement speed based on distance
            float currentSpeed = (distanceToPlayer <= attackRange) ? attackSpeed-.5f : moveSpeed-.5f;

            Vector3 targetPosition = new Vector3(player.position.x, defaultYPosition, player.position.z);

            // Move towards the player
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, (currentSpeed-.5f) * Time.deltaTime);

            // Check if the enemy is close enough to attack
            if (distanceToPlayer <= attackRange && !isAttacking)
            {
                // Attack the player
                StartCoroutine(Attack());
            }
            else
            {
                // Move animation
                animator.SetBool("canWalk", true);
            }
        }
        else
        {
            // Stop moving if player is out of follow range
            animator.SetBool("canWalk", false);
        }
    }

    IEnumerator Attack()
    {     // Set attacking state
        isAttacking = true;

        // Stop moving
        animator.SetBool("canWalk", false);

        // Play attack animation
        animator.SetBool("attack", true);

        // Wait for the duration of the attack animation
        yield return new WaitForSeconds(0.5f);

        // Reset attacking state
        isAttacking = false;

        // Reset attack animation
        animator.SetBool("attack", false);

        // Resume walking
        animator.SetBool("canWalk", true);
    }
}
