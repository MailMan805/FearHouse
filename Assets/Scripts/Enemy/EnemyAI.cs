using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Animator anim;
    [Header("Enemy Stats")]
    public string name = "";
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
        var pineObj = FindAnyObjectByType<AgentPineControls>();
        if (pineObj != null) Pine = pineObj.transform;

        var raccObj = FindAnyObjectByType<AgentRaccControls>();
        if (raccObj != null) Racc = raccObj.transform;

        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }

    void Update()
    {
        // Recheck players in case they were not found earlier or were destroyed and respawned
        if (Pine == null)
        {
            var pineObj = FindAnyObjectByType<AgentPineControls>();
            if (pineObj != null) Pine = pineObj.transform;
        }
        if (Racc == null)
        {
            var raccObj = FindAnyObjectByType<AgentRaccControls>();
            if (raccObj != null) Racc = raccObj.transform;
        }

        Transform target = null;
        float closestDistance = Mathf.Infinity;

        if (Pine != null)
        {
            float dist = Vector3.Distance(transform.position, Pine.position);
            if (dist <= detectionRange && dist < closestDistance)
            {
                target = Pine;
                closestDistance = dist;
            }
        }

        if (Racc != null)
        {
            float dist = Vector3.Distance(transform.position, Racc.position);
            if (dist <= detectionRange && dist < closestDistance)
            {
                target = Racc;
                closestDistance = dist;
            }
        }

        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if ((other.CompareTag("Pine") || other.CompareTag("Racc")) && canAttack)
        {
            anim.SetBool("isAttacking", true);
            StartCoroutine(AttackPlayer(other.gameObject));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.CompareTag("Pine") || other.CompareTag("Racc")))
        {
            anim.SetBool("isAttacking", false);
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