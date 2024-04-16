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
            transform.position += transform.forward * (speed * Time.deltaTime);
            MoveTowardsEnemies();
        }
        else
        {
            Debug.Log("No Speed");
        }
    }

    void MoveTowardsEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) < 3)
                transform.position = Vector3.MoveTowards(transform.position, enemy.transform.position, 40f * Time.deltaTime);
        }
    }
}
