using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackvfx : MonoBehaviour
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
        if (canAttack && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.S)))
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
            StartCoroutine(JumpAttackVfxCoroutine());
        }
        else
        {
            Debug.LogError("Fire point or VFX not assigned.");
        }
    }


    IEnumerator JumpAttackVfxCoroutine()
    {
        // Delay the attack by 0.5 seconds
        yield return new WaitForSeconds(0.5f);
        // Instantiate the VFX object at firePoint position
        GameObject vfxObject = Instantiate(effectToSpawn, firePoint.transform.position, Quaternion.identity);

        // Get the direction the player is facing
        Vector3 direction = transform.forward;
        // If the player is facing left, flip the direction
        if (transform.localScale.x < 0)
        {
            direction *= -1f;
        }
        // Rotate the VFX object to face the direction the player is facing
        vfxObject.transform.forward = direction;


        // Rotate the VFX object by 90 degrees in X and Y axes
        vfxObject.transform.Rotate(90, 90, 0);

        // Destroy the VFX object after 7 seconds
        Destroy(vfxObject, 5f);
    }

    IEnumerator ResetAttack()
    {
        // Wait for 1 second
        yield return new WaitForSeconds(1f);
        // Reset canAttack flag
        canAttack = true;
    }
}
