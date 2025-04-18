using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float health = 100f;
    public float speed = 3.5f;
    public int attackDamage = 10;
    public float attackDelay = 1.5f;
    public float detectionRange = 10f;
    public float cooldown = 15f;
    public int fearPointReward = 1;

    [Header("References")]
    public Transform Pine;
    public Transform Racc;

    private NavMeshAgent agent;
    private bool canAttack = true;

    void Start()
    {
        Pine = FindAnyObjectByType<AgentPineControls>().transform;
        Racc = FindAnyObjectByType<AgentRaccControls>().transform;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }

    void Update()
    {
        if (Pine == null || Racc == null)
        {
            return;
        }

        float distanceToPlayer1 = Vector3.Distance(transform.position, Pine.position);
        float distanceToPlayer2 = Vector3.Distance(transform.position, Racc.position);

        Transform closestPlayer = distanceToPlayer1 < distanceToPlayer2 ? Pine : Racc;

        if (Mathf.Min(distanceToPlayer1, distanceToPlayer2) <= detectionRange)
        {
            agent.SetDestination(closestPlayer.position);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if ((other.CompareTag("Pine") || other.CompareTag("Racc")) && canAttack)
        {
            StartCoroutine(AttackPlayer(other.gameObject));
        }
    }

    private IEnumerator AttackPlayer(GameObject playerObject)
    {
        BasePlayer basePlayer = playerObject.GetComponent<BasePlayer>();
        if (basePlayer != null)
        {
            basePlayer.Hurt(attackDamage);
        }
        canAttack = false;
        yield return new WaitForSeconds(attackDelay);
        canAttack = true;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
