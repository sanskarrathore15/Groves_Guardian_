using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile_attack : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float fountainSpeed = 50f; // Speed of the fountain bullets
    public float fountainInterval = 0.5f; // Time interval between fountain shots
    public float range = 10f; // Range within which the enemy starts shooting
    public Transform projectile_pos;
    private float fountainTimer;
    public float up_force = 20f;
    public float spawnHeight = 1.0f; // Height offset from the enemy's position to spawn the bullet

    void Update()
    {
        float distance = Vector3.Distance(transform.position, projectile_pos.position);

        if (distance < range)
        {
            fountainTimer += Time.deltaTime;
            if (fountainTimer > fountainInterval)
            {
                fountainTimer = 0;
                ShootFountain();
            }
        }
    }

    void ShootFountain()
    {
        // Calculate direction towards the projectile_pos
        Vector3 directionToProjectilePos = (projectile_pos.position - transform.position).normalized;

        // Instantiate bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);

        // Move the bullet towards the spawn height
        StartCoroutine(MoveToSpawnHeight(bullet));

        // Calculate force direction (upward)
        Vector3 upwardForce = Vector3.up * fountainSpeed * up_force;

        // Apply initial upward force to move the bullet to the desired height
        bullet.GetComponent<Rigidbody>().AddForce(upwardForce, ForceMode.Impulse);

        // Calculate the direction towards the projectile_pos
        Vector3 directionToAttack = (projectile_pos.position - bullet.transform.position).normalized;

        // Apply additional force to the bullet towards the projectile_pos
        bullet.GetComponent<Rigidbody>().AddForce(directionToAttack * fountainSpeed, ForceMode.Impulse);
    }

    IEnumerator MoveToSpawnHeight(GameObject bullet)
    {
        // Calculate the target position with the spawn height
        Vector3 targetPosition = bullet.transform.position + Vector3.up * spawnHeight;

        // Move the bullet towards the spawn height gradually
        while (bullet.transform.position.y < targetPosition.y)
        {
            bullet.transform.position += Vector3.up * fountainSpeed * Time.deltaTime;
            yield return null;
        }
    }
}
