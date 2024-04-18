using System.Collections;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

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

    // private bool move = false;
    public GameObject JumpAttackVfx;

   // float jumpTranslation = 20f;
    public Animator animator;
    private float translation = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //transform.Translate(0,0,0);
        JumpAttackVfx.SetActive(false);
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
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                
                jump = true;
            }
            else
            {
                jump = false;
            }

            // Check for slide input
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                slide = true;
            }
            else
            {
                slide = false;
            }

            // Check for attack input
            if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButton(0))
            {
                attack = true;
            }
            else
            {
                attack = false;
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {

                jumpattack = true;
            }
            else
            {
                jumpattack = false;
            }
            if (Input.GetKeyDown(KeyCode.F))
            {

                CombatAttack = true;
            }
            else
            {
                CombatAttack = false;
            }
            if (Input.GetKeyDown(KeyCode.R))
            {

                ComboAttack = true;
            }
            else
            {
                ComboAttack = false;
            }

            // Perform actions based on inputs
            if (jump)
            {

                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                animator.SetBool("isJump", true);
               // transform.Translate(Vector3.up * jumpTranslation * Time.deltaTime);
            }
            else
            {
                animator.SetBool("isJump", false);
            }

            if (slide)
            {
                animator.SetBool("isSlide", true);
            }
            else
            {
                animator.SetBool("isSlide", false);
            }
            if (attack)
            {
                animator.SetBool("attack", true);
            }
            else
            {
                animator.SetBool("attack", false);
            }
            if (CombatAttack)
            {
                animator.SetBool("CombatAttack", true);
            }
            else
            {
                animator.SetBool("CombatAttack", false);
            }
            if (ComboAttack)
            {
                animator.SetBool("ComboAttack", true);
            }
            else
            {
                animator.SetBool("ComboAttack", false);
            }
            if (jumpattack)
            {
                StartCoroutine(JumpAttackCoroutine());
                animator.SetBool("jumpattack", true);
               
            }
            else
            {
                animator.SetBool("jumpattack", false);
            }

        }
        else
        {
            // If the player is dead, stop all movement
            transform.Translate(0, 0, 0);
            animator.SetBool("isDead", dead);
            //jumpTranslation = 0;
            translation = 0;
        }
    }
    IEnumerator JumpAttackCoroutine()
    {
        animator.SetBool("jumpattack", true);

        // Wait for 1 second
        yield return new WaitForSeconds(2.4f);

        // Apply force after the delay
        rb.AddForce(Vector3.forward * jumpForce, ForceMode.Impulse);


    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            dead = true;
        }
    }
}
