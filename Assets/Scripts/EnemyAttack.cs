using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.Timeline;

public class EnemyAttack : MonoBehaviour
{
    public int collisionCount = 10; // Number of collisions required to destroy the GameObject
    private int currentCollisions = 0; // Counter for the collisions
    public GameObject destroyVfx;
    public GameObject collideVfx;
    public Material temporaryMaterial; // Material to apply temporarily on collision
    public GameObject objectWithOriginalMaterial; // GameObject containing the original material
    private Material originalMaterial; // Original material of the GameObject

    void Start()
    {
        // Get the original material from the specified GameObject
        if (objectWithOriginalMaterial != null)
        {
            originalMaterial = objectWithOriginalMaterial.GetComponent<Renderer>().material;
        }
        else
        {
            Debug.LogError("Object with original material not assigned.");
        }
    }

    IEnumerator ChangeMaterialTemporarily(Material newMaterial)
    {
        // Apply the new material
        GetComponent<Renderer>().material = newMaterial;

        // Wait for 1 second
        yield return new WaitForSeconds(1f);

        // Revert back to the original material
        GetComponent<Renderer>().material = originalMaterial;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyAttack") || other.gameObject.CompareTag("Enemy"))
        {
            currentCollisions++;
            Debug.Log("Collision with Enemy. " + (collisionCount - currentCollisions) + " more collision(s) required.");
            Instantiate(collideVfx, transform.position, Quaternion.identity);
            // Start the coroutine to change material temporarily
            StartCoroutine(ChangeMaterialTemporarily(temporaryMaterial));

            if (currentCollisions >= collisionCount)
            {
                Instantiate(destroyVfx, transform.position, Quaternion.identity);
                Destroy(gameObject);
                Debug.Log("Destroyed itself");
            }
            else
            {
                
            }
        }
    }
}
