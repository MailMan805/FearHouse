using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentPineControls : MonoBehaviour
{
    //This handles pines animations and attack/block
    public Animator pineIdol;
    public Animator pineBlock;
    public Animator pineAttack;
    public Animator pineDowned;
    public Animator pineRevived;
    public Animator pineArms;
    public Animator pineRunning;

    public GameObject Idol;
    public GameObject IdolBody; //Just the torso for running
    public GameObject RunningLegs;
    public GameObject AttackingBody;
    public GameObject BlockingBody;
    public GameObject Downed;
    public GameObject Revived;
    public GameObject ArmsNormal;
    public GameObject ArmsAttack;
    public GameObject ArmsBlock;
    public GameObject ArmsHoldBlock;

    public PlayerController Controller;
    public BasePlayer player;

    private bool wasDowned = false;
    private bool isReviving = false;
    


    private void Start()
    {
        ResetAllStates();
        Idol.SetActive(true);
        ArmsNormal.SetActive(true);
    }

    private void Update()
    {
        // Handle movement animations
        if(!wasDowned)
        {
            if ((Controller.moveInput.x != 0 || Controller.moveInput.z != 0))
            {
                Idol.SetActive(false);
                RunningLegs.SetActive(true);
                IdolBody.SetActive(true);
            }
            else
            {
                Idol.SetActive(true);
                RunningLegs.SetActive(false);
                IdolBody.SetActive(false);
            }
        }

        // Handle downed state
        if (player.currentHealth <= 0)
        {
            if (!wasDowned)
            {
                // Only reset and play when first entering downed state
                ResetAllStates();
                Controller.ensnared = true;
                Controller.OnDowned();
                Downed.SetActive(true);
                pineDowned.Play("Downed", -1, 0f); // Restart the animation from beginning
                wasDowned = true;
            }
        }
        else if (wasDowned && player.currentHealth > 0 && !isReviving) 
        {
            isReviving = true;
            ResetAllStates();
            StartCoroutine(Revive());
        }

        if(Controller.isBlocking)
        {

        }

        if(Controller.isAttacking)
        {

        }
    }

    IEnumerator Revive()
    {
        Revived.SetActive(true);
        pineRevived.Play("Revived", -1, 0f); // Restart the animation from beginning
        yield return new WaitForSeconds(1.1f);
        Revived.SetActive(false);
        ArmsNormal.SetActive(true);
        Controller.ensnared = false;
        wasDowned = false;
        isReviving = false;
        Controller.OnRevived();
    }

    private void ResetAllStates()
    {
        Idol.SetActive(false);
        IdolBody.SetActive(false);
        RunningLegs.SetActive(false);
        AttackingBody.SetActive(false);
        BlockingBody.SetActive(false);
        Downed.SetActive(false);
        Revived.SetActive(false);
        ArmsNormal.SetActive(false);
        ArmsAttack.SetActive(false);
        ArmsBlock.SetActive(false);
        ArmsHoldBlock.SetActive(false);
    }
}