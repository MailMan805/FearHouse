using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public GameObject camera;
    public GameObject PointA;
    public GameObject PointB;

    public float t = 0;
    public bool timeStart = false;
    public void GoToScene(int x)
    {
        StartCoroutine(CameraMove(x));
    }

    void Update()
    {
        if (timeStart)
        {
            t += Time.deltaTime;
        }
        if (t > 4f)
        {
            t = 0;
        }
    }

    IEnumerator CameraMove(int x)
    {
        timeStart = true;
        camera.transform.position = new Vector3(Mathf.Lerp(PointA.transform.position.x, PointB.transform.position.x, t), Mathf.Lerp(PointA.transform.position.y, PointB.transform.position.y, t), Mathf.Lerp(PointA.transform.position.z, PointB.transform.position.z, t));
        yield return new WaitForSeconds(t);
        SceneManager.LoadScene(x);
    }
}
