using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject firePoint;
    public List<GameObject> vfx = new List<GameObject>();
    private GameObject effectToSpawn;
    private bool canAttack = true;
    public RotateToMouse rotateToMouse;

    void Start()
    {
        effectToSpawn = vfx[0];
    }

    void Update()
    {
        if (canAttack && Input.GetMouseButton(0))
        {
            Invoke("SpawnVFX", 0.5f); // Delay the attack by 0.5 seconds
            canAttack = false;
            Invoke("ResetAttack", 1f); // Start a timer to reset attack after 1 second
        }
    }

    void SpawnVFX()
    {
        GameObject vfxObject;
        if (firePoint != null)
        {
            vfxObject = Instantiate(effectToSpawn, firePoint.transform.position, Quaternion.identity);
            if (rotateToMouse != null)
            {
                vfxObject.transform.localRotation = rotateToMouse.GetRotation();
            }
        }
        else
        {
            Debug.Log("No Fire Point");
        }
    }

    void ResetAttack()
    {
        canAttack = true;
    }
}
