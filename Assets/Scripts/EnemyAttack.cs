using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAttack : MonoBehaviour
{
    public int collisionCount = 10; // Number of collisions required to destroy the GameObject
    private int currentCollisions = 0; // Counter for the collisions
    public GameObject destroyVfx;
    public GameObject collideVfx;
    public int enemy = 0;
    public int hero = 0;

    // Reference to the health bar UI Image component
    public Image healthBar;
    public GameObject bar;

    private void Start()
    {
        // Ensure healthBar is not null
        if (healthBar == null)
        {
            Debug.LogError("Health Bar Image reference is not set!");
        }

       
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyAttack") || other.gameObject.CompareTag("Enemy"))
        {
            currentCollisions++;
            Debug.Log("Collision with Enemy. " + (collisionCount - currentCollisions) + " more collision(s) required.");
            Instantiate(collideVfx, transform.position, Quaternion.identity);

            // Update the health bar fill amount
            UpdateHealthBar();

            bar.SetActive(true);

            StartCoroutine(bgCoroutine());

            if (currentCollisions >= collisionCount)
            {
                if(currentCollisions == 60 && hero == 1)
                {
                    Time.timeScale = 0f;
                    gameOver.GameOver.gameObject.SetActive(true);
                }
                if (currentCollisions == 30 && enemy == 1)
                {
                    Time.timeScale = 0f;
                    gameOver.Game_Complete.gameObject.SetActive(true);
                }

                Instantiate(destroyVfx, transform.position, Quaternion.identity);
                Destroy(gameObject);
                Debug.Log("Destroyed itself");
                Destroy(bar);
            }
        }
    }

    // Update the health bar fill amount based on currentCollisions
    void UpdateHealthBar()
    {
        // Ensure healthBar is not null
        if (healthBar == null)
        {
            Debug.LogError("Health Bar Image reference is not set!");
            return;
        }

        // Calculate the fill amount based on currentCollisions and collisionCount
        float fillAmount = 1f - ((float)currentCollisions / collisionCount);
        healthBar.fillAmount = fillAmount;
    }

    IEnumerator bgCoroutine()
    {
        yield return new WaitForSeconds(15f);

        //bar.SetActive(false );
    }
}
