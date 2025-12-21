using UnityEngine;
using UnityEngine.AI;

public class KillerAI1 : MonoBehaviour
{
    [Header("References")]
    public Transform player;          // Assign in Inspector or auto-find by tag
    private NavMeshAgent agent;

    [Header("Combat")]
    public int damage = 20;           // Damage per attack
    public float attackDistance = 2f; // Distance to attack
    public float attackCooldown = 1.5f; 

    private float nextAttackTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        // Move toward the player
        agent.SetDestination(player.position);

        // Check distance for attack
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= attackDistance && Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    void Attack()
    {
        if (player == null) return;

        PlayerHealth1 health = player.GetComponent<PlayerHealth1>();
        if (health != null)
        {
            health.TakeDamage(damage);
            Debug.Log("Player took " + damage + " damage! Current health: " + health.currentHealth);
        }
    }
}
