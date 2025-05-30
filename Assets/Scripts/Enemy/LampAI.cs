using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class LampAI : MonoBehaviour
{
    public Animator anim;
    public float detectionRange = 10f;
    public float sneakRange = 25f;
    public float verticalJumpForce = 15f;
    public float lungeForce = 25f;
    public float explosionRadius = 5f;
    public int explosionDamage = 50;
    public float explosionDelay = 0.2f;
    public float missedRecoveryDelay = 2f;
    public LayerMask damageableLayers;
    public float sneakSpeed = 2f;
    public float fleeSpeed = 6f;

    private Transform Pine;
    private Transform Racc;
    private Camera pineCam;
    private Camera raccCam;
    private NavMeshAgent agent;
    private Rigidbody rb;

    private bool hasJumped = false;
    private bool hasLunged = false;
    private bool isExploding = false;
    private bool playerInRange = false;
    private bool missed = false;
    private bool isSeen = false;
    private bool isFleeing = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        var pineObj = FindAnyObjectByType<AgentPineControls>();
        if (pineObj != null)
        {
            Pine = pineObj.transform;
            pineCam = Pine.GetComponentInChildren<Camera>();
        }

        var raccObj = FindAnyObjectByType<AgentRaccControls>();
        if (raccObj != null)
        {
            Racc = raccObj.transform;
            raccCam = Racc.GetComponentInChildren<Camera>();
        }

        agent.updateRotation = false;
        agent.isStopped = true;
        rb.isKinematic = true;
    }

    void Update()
    {
        if (isExploding) return;

        ReacquirePlayers();
        Transform target = GetClosestPlayer();
        if (target == null) return;

        float distance = Vector3.Distance(transform.position, target.position);
        isSeen = IsSeenByCamera();

        if (!hasJumped && distance <= detectionRange)
        {
            StartCoroutine(PerformJumpSequence(target));
        }
        else if (!hasJumped && distance <= sneakRange)
        {
            FaceTarget(target);

            if (isSeen)
            {
                // Dodge to the left or right (randomly)
                isFleeing = true;
                agent.isStopped = false;
                agent.speed = fleeSpeed;

                Vector3 toTarget = (target.position - transform.position).normalized;
                Vector3 dodgeDir = Vector3.Cross(toTarget, Vector3.up).normalized;

                // Randomly choose left or right
                if (Random.value > 0.5f)
                    dodgeDir *= -1;

                Vector3 dodgeDestination = transform.position + dodgeDir * 5f;
                agent.SetDestination(dodgeDestination);
            }
            else
            {

                // Sneak toward the target
                isFleeing = false;
                agent.isStopped = false;
                agent.speed = sneakSpeed;
                agent.SetDestination(target.position);
            }
        }
    }

    private void FaceTarget(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        direction.y = 0; // keep it flat
        if (direction.sqrMagnitude > 0.01f)
        {
            // Rotate to face the player
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            // Apply a +90 degree offset if model's forward is +X
            lookRotation *= Quaternion.Euler(0, 270f, 0);

            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    private void ReacquirePlayers()
    {
        if (Pine == null)
        {
            var pineObj = FindAnyObjectByType<AgentPineControls>();
            if (pineObj != null)
            {
                Pine = pineObj.transform;
                pineCam = Pine.GetComponentInChildren<Camera>();
            }
        }

        if (Racc == null)
        {
            var raccObj = FindAnyObjectByType<AgentRaccControls>();
            if (raccObj != null)
            {
                Racc = raccObj.transform;
                raccCam = Racc.GetComponentInChildren<Camera>();
            }
        }
    }

    private bool IsSeenByCamera()
    {
        return (pineCam != null && CanSeeLamp(pineCam)) || (raccCam != null && CanSeeLamp(raccCam));
    }

    private bool CanSeeLamp(Camera cam)
    {
        Vector3 viewportPoint = cam.WorldToViewportPoint(transform.position);
        if (viewportPoint.z > 0 && viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1)
        {
            Ray ray = new Ray(cam.transform.position, transform.position - cam.transform.position);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == transform)
                    return true;
            }
        }
        return false;
    }

    private Transform GetClosestPlayer()
    {
        float distPine = Pine != null ? Vector3.Distance(transform.position, Pine.position) : float.MaxValue;
        float distRacc = Racc != null ? Vector3.Distance(transform.position, Racc.position) : float.MaxValue;

        if (distPine == float.MaxValue && distRacc == float.MaxValue)
            return null;

        return distPine < distRacc ? Pine : Racc;
    }

    private IEnumerator PerformJumpSequence(Transform target)
    {
        hasJumped = true;
        hasLunged = false;
        missed = false;
        playerInRange = false;

        agent.enabled = false;
        rb.isKinematic = false;

        rb.velocity = new Vector3(0, verticalJumpForce, 0);
        anim.SetBool("IsAttacking", true);

        yield return new WaitForSeconds(1f);

        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            rb.velocity = direction * lungeForce;
            hasLunged = true;
        }

        yield return new WaitForSeconds(missedRecoveryDelay);

        if (!isExploding)
        {
            missed = true;
            anim.SetBool("IsAttacking", false);
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
            agent.enabled = true;

            hasJumped = false;
            hasLunged = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasLunged && !isExploding && (collision.gameObject.CompareTag("Pine") || collision.gameObject.CompareTag("Racc")))
        {
            StartCoroutine(Explode());
        }
    }

    private IEnumerator Explode()
    {
        isExploding = true;
        anim.SetBool("IsAttacking", false);
        anim.SetBool("isMissed", false);

        yield return new WaitForSeconds(explosionDelay);

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius, damageableLayers);
        foreach (Collider hit in hitColliders)
        {
            if (hit.CompareTag("Pine") || hit.CompareTag("Racc"))
            {
                BasePlayer basePlayer = hit.GetComponent<BasePlayer>();
                if (basePlayer != null)
                {
                    basePlayer.Hurt(explosionDamage);
                }
            }
        }

        Destroy(gameObject);
    }
}