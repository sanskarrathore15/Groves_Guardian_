using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttack : MonoBehaviour
{
    public GameObject firePoint;
    public List<GameObject> vfx = new List<GameObject>();
    private GameObject effectToSpawn;
    private bool canAttack = true;

    void Start()
    {
        // Check if the vfx list has any elements
        if (vfx.Count > 0)
        {
            effectToSpawn = vfx[0];
        }
        else
        {
            Debug.LogError("No VFX assigned.");
        }
    }

    void Update()
    {
        // Remove the semicolon after the if condition
        if (canAttack && Input.GetKeyDown(KeyCode.Q))
        {
            // Remove the Invoke calls and directly call SpawnVFX
            SpawnVFX();
            canAttack = false;
            // Use StartCoroutine for ResetAttack coroutine
            StartCoroutine(ResetAttack());
        }
    }

    void SpawnVFX()
    {
        if (firePoint != null && effectToSpawn != null)
        {
            // Check the direction the player is facing
            Vector3 spawnDirection = transform.right; // Assuming the player's forward direction is along the X-axis

            // If the player is facing left, flip the spawn direction
            if (transform.localScale.x <= 0)
            {
                spawnDirection *= -1;
            }

            // Start the coroutine to spawn VFX
            StartCoroutine(JumpAttackVfxCoroutine(spawnDirection));
        }
        else
        {
            Debug.LogError("Fire point or VFX not assigned.");
        }
    }

    IEnumerator JumpAttackVfxCoroutine(Vector3 direction)
    {
        // Delay the attack by 0.5 seconds
        yield return new WaitForSeconds(1.5f);
        // Instantiate the VFX object with adjusted position and direction
        GameObject vfxObject = Instantiate(effectToSpawn, firePoint.transform.position, Quaternion.identity);
        // Set the VFX object's direction
        vfxObject.transform.right = direction;
        // Destroy the VFX object after 7 seconds
        Destroy(vfxObject, 1f);
    }

    IEnumerator ResetAttack()
    {
        // Wait for 1 second
        yield return new WaitForSeconds(1f);
        // Reset canAttack flag
        canAttack = true;
    }
}
