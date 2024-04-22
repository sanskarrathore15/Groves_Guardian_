using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.Timeline;

public class FlashEffect : MonoBehaviour
{
   
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
        yield return new WaitForSeconds(0.1f);

        // Revert back to the original material
        GetComponent<Renderer>().material = originalMaterial;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyAttack") || other.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(ChangeMaterialTemporarily(temporaryMaterial));
        }
    }
}
