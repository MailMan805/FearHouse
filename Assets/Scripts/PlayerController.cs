using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal" + gameObject.name); // e.g. "HorizontalP1", "HorizontalP2"
        float moveZ = Input.GetAxis("Vertical" + gameObject.name);   // e.g. "VerticalP1", "VerticalP2"

        Vector3 move = new Vector3(moveX, 0, moveZ) * moveSpeed * Time.deltaTime;
        transform.Translate(move);
    }
}
