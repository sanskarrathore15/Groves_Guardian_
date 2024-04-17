using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackMove : MonoBehaviour
{
    public float speed;
    public float fireRate;

    void Update()
    {
        if (speed != 0)
        {
            // Only move along the x-axis
            Vector3 newPosition = transform.position + transform.forward * (speed * Time.deltaTime);
            MoveTowardsEnemies(newPosition);
        }
        else
        {
            Debug.Log("No Speed");
        }
    }

    void MoveTowardsEnemies(Vector3 newPosition)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) < 5)
            {
                // Only modify the x coordinate of the position
                Vector3 direction = enemy.transform.position - newPosition;
               // direction.y = 0; // Lock movement along the y-axis
                direction.x = 0; // Lock movement along the z-axis
                newPosition = Vector3.MoveTowards(newPosition, enemy.transform.position, 40f * Time.deltaTime);
            }
        }

        // Apply the new position
        transform.position = newPosition;
    }
}
