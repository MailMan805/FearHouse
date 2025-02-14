using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCube : MonoBehaviour
{
    //Move Cube from Point A to Point B, Constant
    public GameObject PointA;
    public GameObject PointB;
    public float speed = 2;

    private bool flipflop = true;


    // Update is called once per frame
    void Update()
    {
        if(flipflop && Input.GetKeyDown(KeyCode.Z))
        {
            
            transform.position = PointA.transform.position;
            flipflop = false;
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            transform.position = PointB.transform.position;
            flipflop = true;
        }

    }


}
