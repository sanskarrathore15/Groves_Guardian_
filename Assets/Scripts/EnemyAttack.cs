using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.Timeline;

public class EnemyAttack : MonoBehaviour
{
    public int collisionCount = 10; // Number of collisions required to destroy the GameObject
    private int currentCollisions = 0; // Counter for the collisions
    public GameObject Destroyvfx;
    public GameObject collidevfx;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyAttack") || other.gameObject.CompareTag("Enemy"))
        {

            currentCollisions++;

            //Destroy(other.gameObject);
            Debug.Log("destroy_other");
            Instantiate(collidevfx, transform.position, Quaternion.identity);

            if (currentCollisions >= collisionCount)
            {
                Instantiate(Destroyvfx, transform.position, Quaternion.identity);

                Destroy(gameObject);
                Debug.Log("Destroy_itself");
            }
            else
            {
                Debug.Log("Collision with Enemy. " + (collisionCount - currentCollisions) + " more collision(s) required.");
            }
        }
    }
}