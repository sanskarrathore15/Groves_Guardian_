using System.Collections;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    public int collisionCount = 3; // Number of collisions required to destroy the GameObject
    private int currentCollisions = 0; // Counter for the collisions
    public GameObject destroyVfx; // Destruction VFX prefab
    public GameObject collideVfx; // Collision VFX prefab

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Attack"))
        {
            currentCollisions++;
            Destroy(other.gameObject);
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
        else if (other.gameObject.CompareTag("CombatAttack"))
        {
            currentCollisions += 2;
            //Destroy(other.gameObject);
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
            currentCollisions += 3;
            //Destroy(other.gameObject);
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
    }
}
