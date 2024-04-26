using System.Collections;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    

    public AudioClip backgroundMusic;
    //public AudioClip powerUpSound;
    //public AudioClip combatSound;

    public AudioSource musicSource;
    public AudioSource soundSource;
    public AudioClip Jump, powerUp, run, dash;


    Rigidbody rb;
    public float jumpForce = 28f;
    private bool jump = false;
    private bool dead = false;
    private bool attack = false;
    private bool jumpattack = false;
    private bool ComboAttack = false;
    private bool CombatAttack = false;
    private int jumpsLeft = 2; // Number of jumps allowed
    public Transform respawn_pt;
    // private bool move = false;
    public GameObject JumpAttackVfx;
    private GameObject currentJumpAttackVfx; // Reference to the currently spawned jump attack VFX


    // float jumpTranslation = 20f;
    public Animator animator;
    private float translation = 5f;


    public void PlayBackgroundMusic()
    {
        // Play the background music on loop
        musicSource.loop = true;
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //transform.Translate(0,0,0);
        PlayBackgroundMusic();

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
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                soundSource.clip = run;
                soundSource.Play();
            }


            // Check for jump input
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && jumpsLeft > 0)
            {

                jump = true;
                soundSource.clip = Jump;
                soundSource.Play();
            }
            else
            {
                jump = false;
            }


            // Check for attack input
            if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButton(0))
            {
                attack = true;
                soundSource.clip = powerUp;
                soundSource.Play();

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
            if (Input.GetKeyDown(KeyCode.S))
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
                rb.velocity = Vector3.zero; // Reset velocity to prevent additive jumps
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                animator.SetBool("isJump", true);
                jumpsLeft--; // Decrease jumps left after jumping
            }
            else
            {
                animator.SetBool("isJump", false);
            }
            if (attack)
            {
                soundSource.PlayOneShot(powerUp);

                animator.SetBool("attack", true);
            }
            else
            {

                soundSource.clip = null;
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
                //StartCoroutine(JumpAttackCoroutine());
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
            translation = 0;
        }
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            jumpsLeft = 2; // Reset jumps when player lands on the ground
        }
       if (other.gameObject.CompareTag("Obstacle"))
        {
            transform.position = respawn_pt.position;
        }

    }
}
