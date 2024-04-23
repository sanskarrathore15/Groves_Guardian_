using System.Collections;
using TMPro;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public GameObject attackVFXPrefab; // Prefab of the attack VFX
    public Transform vfxPosition; // Transform of the GameObject providing VFX position
    public float moveSpeed = 60f; // Base speed when out of attack range
    public float attackSpeed = 2f; // Reduced speed when in attack range
    public float attackRange = 2f; // Distance at which the enemy attacks the player
    public float followRange = 15f; // Distance at which the enemy starts following the player
    public float backwardDistance = 30f; // Distance to move backward when triggered

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
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 8f);

                // Set movement speed based on distance
                float currentSpeed = (distanceToPlayer <= attackRange) ? attackSpeed : moveSpeed;

                // Move towards the player if not attacking and player is in range
                if (distanceToPlayer <= attackRange)
                {
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
        if (Input.GetKeyDown(KeyCode.E))
        {
            rb.velocity = Vector3.zero;
            Vector3 backwardPosition = transform.position - transform.forward * backwardDistance;
            transform.position = Vector3.MoveTowards(transform.position, backwardPosition, moveSpeed * Time.deltaTime);
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

        // Get the forward direction of the enemy
        Vector3 attackDirection = transform.forward;

        // Instantiate attack VFX at the provided position or slightly in front of the enemy
        Vector3 spawnPosition = (vfxPosition != null) ? vfxPosition.position : transform.position + attackDirection * 1.5f;
        yield return new WaitForSeconds(1.73f);
        GameObject vfxInstance = Instantiate(attackVFXPrefab, spawnPosition, Quaternion.identity);
        vfxInstance.transform.Rotate(180, -90, 0);

        // Wait for one second
        yield return new WaitForSeconds(1.0f);

        // Destroy the attack VFX instance
        Destroy(vfxInstance);

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
        yield return new WaitForSeconds(1.0f);

        // Reset shield state
        isShieldActive = false;

        // Reset shield animation
        animator.SetBool("shield_parry", false);
        animator.SetBool("Run", true);
    }
}
