using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : MonoBehaviour
{
    public int damagePerTick = 5;
    public float tickInterval = 0.5f;
    public ParticleSystem fireBreathParticle;
    public float rotationSpeed = 2f;
    public Animator anim;
    public float detectionRange = 10f;
    public int FearPointsModifier;
    public int Health = 30;
    public int DespawnTime = 3;

    private GameObject pine;
    private GameObject racc;
    private GameObject currentTarget;

    private float timeInRange = 0f;
    private bool isFiring = false;
    private bool isCoolingDown = false;

    private bool pineIsInDangerRange = false;
    private bool raccIsInDangerRange = false;

    void Start()
    {
        pine = GameObject.FindGameObjectWithTag("Pine");
        racc = GameObject.FindGameObjectWithTag("Racc");
    }

    void Update()
    {
        UpdateTarget();
        if(Input.GetKeyDown(KeyCode.H)) //For testing purposes
        {
            StartCoroutine(Damage(10));
        }
        if (currentTarget != null)
        {
            RotateTowards(currentTarget.transform);

            if (!isFiring && !isCoolingDown)
            {
                timeInRange += Time.deltaTime;

                if (timeInRange >= 5f)
                {
                    StartCoroutine(FireAtPlayer(currentTarget));
                }
            }
        }
        if(Health <= 0)
        {
            anim.SetBool("IsDying", true);
            StartCoroutine(Death());
        }
        
    }

    public IEnumerator Damage(int damage)
    {
        anim.SetBool("IsDamaged", true);
        Health -= damage;
        yield return new WaitForSeconds(.5f);
        anim.SetBool("IsDamaged", false);

    }

    void UpdateTarget()
    {
        pine = GameObject.FindGameObjectWithTag("Pine");
        racc = GameObject.FindGameObjectWithTag("Racc");
        float distToPine = pine != null ? Vector3.Distance(transform.position, pine.transform.position) : Mathf.Infinity;
        float distToRacc = racc != null ? Vector3.Distance(transform.position, racc.transform.position) : Mathf.Infinity;

        bool pineInRange = distToPine <= detectionRange;
        bool raccInRange = distToRacc <= detectionRange;

        // If no target and someone enters range
        if (currentTarget == null)
        {
            if (pineInRange) currentTarget = pine;
            else if (raccInRange) currentTarget = racc;
        }
        else
        {
            // If current target leaves range, try to switch
            float distToCurrent = Vector3.Distance(transform.position, currentTarget.transform.position);
            if (distToCurrent > detectionRange)
            {
                if (currentTarget == pine && raccInRange) currentTarget = racc;
                else if (currentTarget == racc && pineInRange) currentTarget = pine;
                else currentTarget = null;
                anim.SetBool("IsTurning", false);

                timeInRange = 0f;
            }
        }
    }

    void RotateTowards(Transform target)
    {
        anim.SetBool("IsTurning", true);
        Vector3 direction = (target.position - transform.position).normalized;
        direction.y = 0f;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }

    IEnumerator FireAtPlayer(GameObject target)
    {
        isFiring = true;
        anim.SetTrigger("OpenMouth");
        anim.SetBool("IsFiring", true);
        yield return new WaitForSeconds(0.5f);

        fireBreathParticle.Play();
        float elapsed = 0f;

        while (elapsed < 3f)
        {
            DealDamageToPlayer(target); // Customize per player damage
            yield return new WaitForSeconds(tickInterval);
            elapsed += tickInterval;
        }
        anim.SetBool("IsFiring", false);
        fireBreathParticle.Stop();
        isFiring = false;
        isCoolingDown = true;
        currentTarget = null;
        timeInRange = 0f;

        yield return new WaitForSeconds(10f);
        isCoolingDown = false;
    }

    IEnumerator Death()
    {
        anim.SetBool("IsDying", true);
        yield return new WaitForSeconds(DespawnTime);
        Destroy(gameObject);
    }

    void DealDamageToPlayer(GameObject player)
    {
        if(player.gameObject.CompareTag("Pine") && pineIsInDangerRange)
        {
            player.GetComponent<PlayerHealth>().TakeDamage(damagePerTick);
        Debug.Log(player.name + " takes " + damagePerTick + " fire damage.");
        }
        if (player.gameObject.CompareTag("Racc") && raccIsInDangerRange)
        {
            player.GetComponent<PlayerHealth>().TakeDamage(damagePerTick);
            Debug.Log(player.name + " takes " + damagePerTick + " fire damage.");
        }
        
        // Hook into health script here if needed
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pine"))
        {
            pineIsInDangerRange = true;
        }
        if (other.CompareTag("Racc"))
        {
            raccIsInDangerRange = true;
        }
    }

    // Use OnTriggerExit for when the player exits the danger range
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pine"))
        {
            pineIsInDangerRange = false;
        }
        if (other.CompareTag("Racc"))
        {
            raccIsInDangerRange = false;
        }
    }
}
