using System.Collections;
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
    private bool isShieldActive = false;
    private float defaultYPosition;
    private Rigidbody rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        defaultYPosition = transform.position.y;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!isAttacking)
        {
            // Check distance to the player
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= followRange)
            {
                animator.SetBool("Run", true);
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
                  //  transform.position = Vector3.MoveTowards(transform.position, transform.position, 0f * Time.deltaTime);
                    StartCoroutine(Attack());
                }
                else
                {
                    // Move towards the player
                    Vector3 targetPosition = new Vector3(player.position.x, defaultYPosition, player.position.z);
                    animator.SetBool("Run", true);
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

        // Check for shield activation
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(ActivateShield());
        }
    }

    IEnumerator Attack()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        float currentSpeed = (distanceToPlayer <= attackRange) ? attackSpeed : moveSpeed;

        // Set attacking state
        isAttacking = true;

        // Play attack animation
        animator.SetBool("Claw_Attack", true);
        yield return new WaitForSeconds(.3f);

        // Reset attack state
        isAttacking = false;

        // Reset attack animation
        animator.SetBool("Claw_Attack", false);

        // Resume moving
        animator.SetBool("Run", true);
    }

    IEnumerator ActivateShield()
    {
        // Set shield active state
        isShieldActive = true;

        // Play shield animation
        animator.SetBool("shield_parry", true);

        // Wait for the shield duration
        yield return new WaitForSeconds(2.0f);

        // Reset shield state
        isShieldActive = false;

        // Reset shield animation
        animator.SetBool("shield_parry", false);
    }
}
