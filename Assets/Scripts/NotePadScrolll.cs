using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotePadScrolll : MonoBehaviour
{
    public Scrollbar Scrollbar;
    public Vector3 StartPos;
    public Vector3 EndPos;

    // Update is called once per frame
    void Update()
    {
        scroll(); // Call scroll every frame to keep position in sync with the scrollbar
    }

    public void scroll()
    {
        float t = Scrollbar.value; // Value is between 0 and 1
        transform.localPosition = Vector3.Lerp(StartPos, EndPos, t);
    }
}