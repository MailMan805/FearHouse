using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rug : MonoBehaviour
{
    public Animator anim;
    bool DamageActivation;
    public int damagePerTic = 1;
    public int tickSpeed = 1;

    bool isPineEnsnared = false;
    bool isRaccEnsnared = false;
    private GameObject pine;
    private GameObject racc;
    public GameObject location;

    public int Health = 20;
    public int DespawnTime = 3;

    bool checkingDamage = false;
    bool checkingDeath = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) //For testing purposes
        {
            DamageRug(10);
        }
        if (pine == null)
        {
            pine = GameObject.FindGameObjectWithTag("Pine");
        }
        if (racc == null)
        {
            racc = GameObject.FindGameObjectWithTag("Racc");
        }
        if (Health <= 0 && !checkingDeath)
        {      
            checkingDeath = true;
            StartCoroutine(Death());
        }

        if (!checkingDamage)
        {
            StartCoroutine(DamageCheck());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pine") && !DamageActivation)
        {
            isPineEnsnared = true;
            DamageActivation = true;
            anim.SetBool("IsEnsnare", true);
            other.GetComponent<PlayerController>().ensnared = true;
            other.gameObject.transform.position = location.transform.position;
        }
        if(other.gameObject.CompareTag("Racc") && !DamageActivation)
        {
            isRaccEnsnared = true;
            DamageActivation = true;
            anim.SetBool("IsEnsnare", true);
            other.GetComponent<PlayerController>().ensnared = true;
            other.gameObject.transform.position = location.transform.position;
        }
    }

    public void DealDamageToPlayer()
    {
        if (isPineEnsnared && DamageActivation)
        {
            pine.gameObject.transform.position = location.transform.position;
            pine.GetComponent<PlayerHealth>().TakeDamage(damagePerTic);
            Debug.Log("Pine takes " + damagePerTic + " snare damage.");
        }
        if (isRaccEnsnared && DamageActivation)
        {
            racc.gameObject.transform.position = location.transform.position;
            racc.GetComponent<PlayerHealth>().TakeDamage(damagePerTic);
            Debug.Log("Racc takes " + damagePerTic + " snare damage.");
        }
    }

    public void DamageRug(int damage)
    {
        Health -= damage;
    }
    IEnumerator DamageCheck()
    {
        checkingDamage = true;
        DealDamageToPlayer();
        yield return new WaitForSeconds(tickSpeed);
        checkingDamage = false;
    }

    IEnumerator Death()
    {
        isPineEnsnared = false;
        isRaccEnsnared = false;
        DamageActivation = false;
        anim.SetBool("IsDying", true);
        if (pine == null)
        {
            pine = GameObject.FindGameObjectWithTag("Pine");
        }
        else
        {
            pine.GetComponent<PlayerController>().ensnared = false;
        }
        if (racc == null)
        {
            racc = GameObject.FindGameObjectWithTag("Racc");
        }
        else
        {
            racc.GetComponent<PlayerController>().ensnared = false;
        }
        anim.SetBool("IsDying", true);
        yield return new WaitForSeconds(DespawnTime);
        Destroy(gameObject);
    }
}
