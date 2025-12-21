using UnityEngine;
using UnityEngine.AI;

public class KillerAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;

    [Header("Attack Settings")]
    public float attackRange = 1.8f;
    public int damage = 15;
    public float attackCooldown = 1.5f;

    private float nextAttackTime = 0f;

    void Start()
    {
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // Move toward player
        if (distance > attackRange)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
        // Attack player
        else
        {
            agent.isStopped = true;
            AttackPlayer();
        }
    }

    void AttackPlayer()
    {
        if (Time.time < nextAttackTime) return;

        PlayerHealth health = player.GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.TakeDamage(damage);
            nextAttackTime = Time.time + attackCooldown;
        }
    }
}
