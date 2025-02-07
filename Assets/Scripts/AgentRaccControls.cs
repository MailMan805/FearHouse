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
    void Shoot(float angleOffset, float speed)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Rigidbody rigidBody = bullet.GetComponent<Rigidbody>();
        if (rigidBody != null)
        {
            //This causes the bullets to be shot at a bizarre angle when not moving, so the quaternion logic should probably be changed
            Vector3 shootDirection = Quaternion.AngleAxis(angleOffset, Vector3.up) * transform.forward;
            rigidBody.velocity = shootDirection * speed;
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot(0,12);
            Shoot(15,8);
            Shoot(-15,8);
            GetComponent<AudioSource>().Play();
        }
    }
}
