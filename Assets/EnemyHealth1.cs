using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth1 : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    // Call this to apply damage
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log(gameObject.name + " took " + amount + " damage. Current HP: " + currentHealth);

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        Debug.Log(gameObject.name + " died!");
        // Optional: play death animation, drop loot, disable AI, etc.
        Destroy(gameObject);
        SceneManager.LoadScene("WinScene");

    }
}
