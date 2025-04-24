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

    public int Health = 20;
    public int DespawnTime = 3;

    bool checkingDamage = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) //For testing purposes
        {
            Health -= 10;
        }
        if (pine == null)
        {
            pine = GameObject.FindGameObjectWithTag("Pine");
        }
        if (racc == null)
        {
            racc = GameObject.FindGameObjectWithTag("Racc");
        }
        if (Health <= 0)
        {
            isPineEnsnared = false;
            isRaccEnsnared= false;
            DamageActivation = false;
            anim.SetBool("IsDying", true);
            StartCoroutine(Death());
        }

        if (!checkingDamage)
        {
            StartCoroutine(DamageCheck());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pine"))
        {
            isPineEnsnared = true;
            DamageActivation = true;
            anim.SetBool("IsEnsnare", true);
        }
        if(other.gameObject.CompareTag("Racc"))
        {
            isRaccEnsnared = true;
            DamageActivation = true;
            anim.SetBool("IsEnsnare", true);
        }
    }

    public void DealDamageToPlayer()
    {
        if (isPineEnsnared && DamageActivation)
        {
            pine.GetComponent<PlayerHealth>().TakeDamage(damagePerTic);
            Debug.Log("Pine takes " + damagePerTic + " snare damage.");
        }
        if (isRaccEnsnared && DamageActivation)
        {
            racc.GetComponent<PlayerHealth>().TakeDamage(damagePerTic);
            Debug.Log("Racc takes " + damagePerTic + " snare damage.");
        }
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
        anim.SetBool("IsDying", true);
        yield return new WaitForSeconds(DespawnTime);
        Destroy(gameObject);
    }
}
