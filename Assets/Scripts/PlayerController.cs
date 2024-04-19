using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public float jumpForce = 28f;
    private bool jump = false;
    private bool slide = false;
    private bool dead = false;
    private bool attack = false;
    private bool jumpattack = false;
    private bool ComboAttack = false;
    private bool CombatAttack = false;
    private int jumpsLeft = 2; // Number of jumps allowed

    public Animator animator;
    private float translation = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!dead)
        {
            // Check for left and right movement
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.forward * translation * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                animator.SetBool("move", true); // Set move to true
            }
            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.forward * translation * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, 180, 0); // Rotate the player 180 degrees around the Y-axis
                animator.SetBool("move", true); // Set move to true
            }
            else
            {
                animator.SetBool("move", false); // Set move to false if no movement key is pressed
            }

            // Check for jump input
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && jumpsLeft > 0)
            {
                jump = true;
            }
            else
            {
                jump = false;
            }

            // Perform actions based on inputs
            if (jump)
            {
                rb.velocity = Vector3.zero; // Reset velocity to prevent additive jumps
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                animator.SetBool("isJump", true);
                jumpsLeft--; // Decrease jumps left after jumping
            }
            else
            {
                animator.SetBool("isJump", false);
            }
        }
        else
        {
            // If the player is dead, stop all movement
            transform.Translate(0, 0, 0);
            animator.SetBool("isDead", dead);
            translation = 0;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            jumpsLeft = 2; // Reset jumps when player lands on the ground
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            dead = true;
        }
    }
}
