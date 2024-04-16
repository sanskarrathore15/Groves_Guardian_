using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    public int collisionCount = 3; // Number of collisions required to destroy the GameObject
    private int currentCollisions = 0; // Counter for the collisions

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            // Increment the collision count
           // currentCollisions++;

            // Destroy the other GameObject
            Destroy(gameObject);

            // Check if the required number of collisions has occurred
        //    if (currentCollisions >= collisionCount)
        //    {
        //        // Destroy this GameObject
        //        Destroy(other.gameObject);
        //    }
        //    else
        //    {
        //        // Play some effect or sound indicating a collision
        //        Debug.Log("Collision with Enemy. " + (collisionCount - currentCollisions) + " more collision(s) required.");
        //    }
        }
    }
}
