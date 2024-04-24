using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DestroyOnCollision : MonoBehaviour
{
    public int collisionCount = 3; // Number of collisions required to destroy the GameObject
    private int currentCollisions = 0; // Counter for the collisions
    public GameObject destroyVfx; // Destruction VFX prefab
    public GameObject collideVfx; // Collision VFX prefab
    //private Animator animator;
    //private bool death = false;
    //public static int death_count=0;

    // Reference to the health bar UI Image component
    public Image healthBar;
    public GameObject bar;

    private void Start()
    {
        // Ensure healthBar is not null
        if (healthBar == null)
        {
            Debug.LogError("Health Bar Image reference is not set!");
        }

        bar.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Attack"))
        {
            //death_count++;
            currentCollisions++;
            Destroy(other.gameObject);
            Instantiate(collideVfx, transform.position, Quaternion.identity);

            UpdateHealthBar();

            bar.SetActive(true);

            StartCoroutine(bgCoroutine());

            if (currentCollisions >= collisionCount)
            {
              //  animator.SetBool("shield_parry", false); animator.SetBool("Claw_Attack", false); animator.SetBool("Run", false);
                //animator.SetBool("death", true);
               Destroy(gameObject);
                Instantiate(destroyVfx, transform.position, Quaternion.identity);
                Debug.Log("Destroyed itself");
                Destroy(bar);
            }
            else
            {
                Debug.Log("Collision with Enemy. " + (collisionCount - currentCollisions) + " more collision(s) required.");
            }
        }
        else if (other.gameObject.CompareTag("CombatAttack"))
        {
            //death_count += 2;
            currentCollisions += 2;
            Instantiate(collideVfx, transform.position, Quaternion.identity);

            UpdateHealthBar();

            bar.SetActive(true);

            StartCoroutine(bgCoroutine());

            if (currentCollisions >= collisionCount)
            {
                Destroy(gameObject);
                Instantiate(destroyVfx, transform.position, Quaternion.identity);
                Debug.Log("Destroyed itself");
                Destroy(bar);
            }
            else
            {
                Debug.Log("Collision with Enemy. " + (collisionCount - currentCollisions) + " more collision(s) required.");
            }
        }
        else if (other.gameObject.CompareTag("JumpAttack"))
        {
            //death_count += 3;
            currentCollisions += 3;
            Instantiate(collideVfx, transform.position, Quaternion.identity);

            UpdateHealthBar();

            bar.SetActive(true);

            StartCoroutine(bgCoroutine());

            if (currentCollisions >= collisionCount)
            {
                Destroy(gameObject);
                Instantiate(destroyVfx, transform.position, Quaternion.identity);
                Debug.Log("Destroyed itself");
                Destroy(bar);
            }
            else
            {
                Debug.Log("Collision with Enemy. " + (collisionCount - currentCollisions) + " more collision(s) required.");
            }
        }

        void UpdateHealthBar()
        {
            // Ensure healthBar is not null
            if (healthBar == null)
            {
                Debug.LogError("Health Bar Image reference is not set!");
                return;
            }

            // Calculate the fill amount based on currentCollisions and collisionCount
            float fillAmount = 1f - ((float)currentCollisions / collisionCount);
            healthBar.fillAmount = fillAmount;
        }

        IEnumerator bgCoroutine()
        {
            yield return new WaitForSeconds(15f);

            bar.SetActive(false);
        }

        //    if(death_count == collisionCount-2)
        //    {
        ////   animator.SetBool("shield_parry", false); animator.SetBool("Claw_Attack", false); animator.SetBool("Run", false);
        //  //    animator.SetBool("death", true);
        //    }
    }
}
