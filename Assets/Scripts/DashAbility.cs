using System.Collections;
using UnityEngine;

public class DashAbility : MonoBehaviour
{
    public float dashDistance = 5f; // Distance to dash
    public float dashDuration = 0.2f; // Duration of the dash
    public GameObject dashEffectPrefab; // Prefab for dash visual effect
    private bool isDashing = false; // Flag to check if currently dashing
    private Vector3 dashDirection; // Direction of the dash

    private void Start()
    {
        dashEffectPrefab.SetActive(false);
    }
    void Update()
    {
        // Check if A or D key and Left Shift key are pressed together
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && Input.GetKeyDown(KeyCode.LeftShift) && !isDashing)
        {
            // Set the dash direction based on the pressed key
            dashDirection = transform.forward;


            // Start the dash
            StartDash();
        }
    }

    void StartDash()
    {
        // Start dashing
        isDashing = true;

        dashEffectPrefab.SetActive(true);

        // Move the player quickly in the dash direction
        StartCoroutine(PerformDash());
    }

    IEnumerator PerformDash()
    {
        // Show dash visual effect
        GameObject dashEffect = Instantiate(dashEffectPrefab, transform.position, Quaternion.identity);

        // Move the player for the duration of the dash
        float startTime = Time.time;
        while (Time.time < startTime + dashDuration)
        {
            transform.position += dashDirection * (dashDistance / dashDuration) * Time.deltaTime;
            yield return null;
        }

        // End the dash
        EndDash();

        // Cooldown before the next dash
        yield return new WaitForSeconds(1f);
        isDashing = false;
    }

    IEnumerator ShowDashEffect()
    {
        // Instantiate dash visual effect
        GameObject dashEffect = Instantiate(dashEffectPrefab, transform.position, Quaternion.identity);


        // Wait for the duration of the dash
        yield return new WaitForSeconds(dashDuration);
    }

    IEnumerator EndDashCoroutine()
    {
        // Wait for 1 second before deactivating the dash visual effect
        yield return new WaitForSeconds(0.8f);
        dashEffectPrefab.SetActive(false);
    }

    void EndDash()
    {
        // Perform any actions needed at the end of the dash
        StartCoroutine(EndDashCoroutine());
    }
}
