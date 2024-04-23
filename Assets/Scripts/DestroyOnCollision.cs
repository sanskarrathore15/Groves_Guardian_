using System.Collections;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    public int collisionCount = 3; // Number of collisions required to destroy the GameObject
    private int currentCollisions = 0; // Counter for the collisions
    public GameObject destroyVfx; // Destruction VFX prefab
    public GameObject collideVfx; // Collision VFX prefab
    private Animator animator;
    private bool death = false;
    public static int death_count=0;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Attack"))
        {
            death_count++;
            currentCollisions++;
            Destroy(other.gameObject);
            Instantiate(collideVfx, transform.position, Quaternion.identity);

            if (currentCollisions >= collisionCount)
            {
              //  animator.SetBool("shield_parry", false); animator.SetBool("Claw_Attack", false); animator.SetBool("Run", false);
                //animator.SetBool("death", true);
               Destroy(gameObject);
                Instantiate(destroyVfx, transform.position, Quaternion.identity);
                Debug.Log("Destroyed itself");
            }
            else
            {
                Debug.Log("Collision with Enemy. " + (collisionCount - currentCollisions) + " more collision(s) required.");
            }
        }
        else if (other.gameObject.CompareTag("CombatAttack"))
        {
            death_count += 2;
            currentCollisions += 2;
            Instantiate(collideVfx, transform.position, Quaternion.identity);

            if (currentCollisions >= collisionCount)
            {
                Destroy(gameObject);
                Instantiate(destroyVfx, transform.position, Quaternion.identity);
                Debug.Log("Destroyed itself");
            }
            else
            {
                Debug.Log("Collision with Enemy. " + (collisionCount - currentCollisions) + " more collision(s) required.");
            }
        }
        else if (other.gameObject.CompareTag("JumpAttack"))
        {
            death_count += 3;
            currentCollisions += 3;
            Instantiate(collideVfx, transform.position, Quaternion.identity);

            if (currentCollisions >= collisionCount)
            {
                Destroy(gameObject);
                Instantiate(destroyVfx, transform.position, Quaternion.identity);
                Debug.Log("Destroyed itself");
            }
            else
            {
                Debug.Log("Collision with Enemy. " + (collisionCount - currentCollisions) + " more collision(s) required.");
            }
        }

        if(death_count == collisionCount-2)
        {
    //   animator.SetBool("shield_parry", false); animator.SetBool("Claw_Attack", false); animator.SetBool("Run", false);
      //    animator.SetBool("death", true);
        }
    }
}
