using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/** 
 * Simple Unity script that simulates a ranged shotgun attack with spread. It calls the shoot method 
 * 3 times per shot input, where the latter 2 bullets are offset by 15 degrees from the first with a slower
 * speed.
 * 
 * 
 */
public class AgentRaccControls : MonoBehaviour
{
    public GameObject bulletPrefab;

    //This handles pines animations and attack/block
    public Animator raccIdol;
    public Animator raccAttack;
    public Animator raccDowned;
    public Animator raccRevived;
    public Animator raccRunning;

    public GameObject Idol;
    public GameObject IdolBody; //Just the torso for running
    public GameObject RunningLegs;
    public GameObject AttackingBody;
    public GameObject Downed;
    public GameObject Revived;
    public GameObject raccArms;

    public PlayerController Controller;
    public BasePlayer player;

    private bool wasDowned = false;
    private bool isReviving = false;


    public void Shoot(float angleOffset, float speed)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Rigidbody rigidBody = bullet.GetComponent<Rigidbody>();
        if (rigidBody != null)
        {
            //This causes the bullets to be shot at a bizarre angle when not moving, so the quaternion logic should probably be changed
            Vector3 shootDirection = Quaternion.Euler(0, angleOffset, 0) * transform.forward;
            rigidBody.velocity = shootDirection * speed;
        }

        Destroy(bullet, 5);
    }
    public void TripleShot()
    {
        Shoot(0, 12);
        Shoot(5, 8);
        Shoot(-5, 8);
        GetComponent<AudioSource>().Play();
    }

    private void Start()
    {
        ResetAllStates();
        Idol.SetActive(true);
    }

    private void Update()
    {
        // Handle movement animations
        if (!wasDowned)
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
                raccDowned.Play("Downed", -1, 0f); // Restart the animation from beginning
                wasDowned = true;
            }
        }
        else if (wasDowned && player.currentHealth > 0 && !isReviving)
        {
            isReviving = true;
            ResetAllStates();
            StartCoroutine(Revive());
        }

        if (Controller.isBlocking)
        {

        }

        if (Controller.isAttacking)
        {

        }
    }

    IEnumerator Revive()
    {
        Revived.SetActive(true);
        raccRevived.Play("Revived", -1, 0f); // Restart the animation from beginning
        yield return new WaitForSeconds(1.1f);
        Revived.SetActive(false);
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
        Downed.SetActive(false);
        Revived.SetActive(false);
    }
}
