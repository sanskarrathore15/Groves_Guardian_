using System.Collections;
using TMPro;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float moveSpeed = 60f; // Base speed when out of attack range
    public float attackSpeed = 2f; // Reduced speed when in attack range
    public float attackRange = 1f; // Distance at which the enemy attacks the player
    public float followRange = 15f; // Distance at which the enemy starts following the player

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
        if (!isAttacking)
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
                float currentSpeed = (distanceToPlayer <= attackRange) ? attackSpeed : moveSpeed;

                // Move towards the player if not attacking and player is in range
                if (distanceToPlayer <= attackRange)
                {
                    // Stop moving if the player is in attack range
                    animator.SetBool("Run", false);
                    transform.position = Vector3.MoveTowards(transform.position, transform.position, currentSpeed * Time.deltaTime);
                    StartCoroutine(Attack());
                }
                else
                {
                    // Move animation
                    animator.SetBool("Run", true);

                    // Move towards the player
                    Vector3 targetPosition = new Vector3(player.position.x, defaultYPosition, player.position.z);
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, currentSpeed * Time.deltaTime);
                }

                // Check if the enemy is close enough to attack
              
            }
            else
            {
                // Stop moving if player is out of follow range
                animator.SetBool("Run", false);
            }
        }
    }

  
        IEnumerator Attack()
        {
            // Set attacking state
            isAttacking = true;

            // Play attack animation
            animator.SetBool("Claw_Attack", true);

            // Wait for the duration of the attack animation (or 2 seconds, whichever is longer)
            float attackDuration = Mathf.Max(0.5f, 1.0f); // 0.5f is your animation duration, 2.0f is the desired stop time
            yield return new WaitForSeconds(attackDuration);

            // Reset attack state
            isAttacking = false;

            // Reset attack animation
            animator.SetBool("Claw_Attack", false);

            // Resume moving
            animator.SetBool("Run", true);
        }
    
}