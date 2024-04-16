using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.Timeline;

public class DestroyOnCollision : MonoBehaviour
{
    public int collisionCount = 3; // Number of collisions required to destroy the GameObject
    private int currentCollisions = 0; // Counter for the collisions
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            currentCollisions++;

            //Destroy(gameObject);

            if (currentCollisions >= collisionCount)
            {
                Destroy(other.gameObject);
            }
        }
    }
    

    //public void OnCollisionEnter(Collision other)
    //{
    //    if (other.gameObject.CompareTag("Enemy"))
    //    {
    //        // Increment the collision count
    //       // currentCollisions++;

    //        // Destroy the other GameObject
    //        Destroy(other.gameObject);

    //        // Check if the required number of collisions has occurred
    //    //    if (currentCollisions >= collisionCount)
    //    //    {
    //    //        // Destroy this GameObject
    //    //        Destroy(other.gameObject);
    //    //    }
    //    //    else
    //    //    {
    //    //        // Play some effect or sound indicating a collision
    //    //        Debug.Log("Collision with Enemy. " + (collisionCount - currentCollisions) + " more collision(s) required.");
    //    //    }
    //    }
    //}
}
