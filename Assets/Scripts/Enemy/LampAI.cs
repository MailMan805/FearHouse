using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LampAI : MonoBehaviour
{
    public float detectionRange = 10f;
    public float verticalJumpForce = 15f;
    public float lungeForce = 25f;
    public float explosionRadius = 5f;
    public int explosionDamage = 50;
    public float explosionDelay = 0.2f;
    public LayerMask damageableLayers;

    private Transform Pine;
    private Transform Racc;
    private NavMeshAgent agent;
    private Rigidbody rb;
    private bool hasJumped = false;
    private bool hasLunged = false;
    private bool isExploding = false;
    private bool playerInRange = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        Pine = FindAnyObjectByType<AgentPineControls>()?.transform;
        Racc = FindAnyObjectByType<AgentRaccControls>()?.transform;
        agent.updateRotation = false;
        agent.isStopped = true; // Enemy remains stationary until players are detected
        rb.isKinematic = true;
    }

    void Update()
    {
        if (isExploding || (Pine == null && Racc == null))
            return;

        Transform target = GetClosestPlayer();
        if (target == null)
            return;

        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (!playerInRange && distanceToTarget <= detectionRange)
        {
            playerInRange = true;
            agent.isStopped = true; // Ensure agent doesn't interfere
        }

        if (playerInRange && !hasJumped && distanceToTarget <= detectionRange)
        {
            StartCoroutine(PerformJumpSequence(target));
        }
    }

    Transform GetClosestPlayer()
    {
        float distanceToPine = Pine != null ? Vector3.Distance(transform.position, Pine.position) : float.MaxValue;
        float distanceToRacc = Racc != null ? Vector3.Distance(transform.position, Racc.position) : float.MaxValue;
        return distanceToPine < distanceToRacc ? Pine : Racc;
    }

    IEnumerator PerformJumpSequence(Transform target)
    {
        hasJumped = true;
        agent.enabled = false; // Disable agent to prevent interference
        rb.isKinematic = false;

        // First jump upwards
        rb.velocity = new Vector3(0, verticalJumpForce, 0);
        yield return new WaitForSeconds(1f); // Short delay before lunging

        // Lunge towards the player
        if (!hasLunged)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            rb.velocity = direction * lungeForce;
            hasLunged = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasLunged && !isExploding && (collision.gameObject.CompareTag("Pine") || collision.gameObject.CompareTag("Racc")))
        {
            StartCoroutine(Explode());
        }
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(explosionDamage);
        }
    }

    private System.Collections.IEnumerator Explode()
    {
        isExploding = true;
        yield return new WaitForSeconds(explosionDelay);

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius, damageableLayers);
        foreach (Collider hit in hitColliders)
        {
            if (hit.CompareTag("Pine") || hit.CompareTag("Racc"))
            {
                //fsg
            }
        }

        // Add explosion effects here
        Destroy(gameObject);
    }
}
