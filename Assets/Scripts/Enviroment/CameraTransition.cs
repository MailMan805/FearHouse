using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class CameraTransition : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 2f;
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration = 1f;
    public string nextSceneName;

    public GameObject MainMenu;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    public void StartTransition()
    {
        StartCoroutine(TransitionSequence());
    }

    private IEnumerator TransitionSequence()
    {
        MainMenu.SetActive(false);
        // Move the camera to point B
        float elapsedTime = 0f;
        Vector3 startPos = pointA.position;
        Vector3 endPos = pointB.position;

        StartCoroutine(FadeToBlack());

        while (elapsedTime < 1f)
        {
            mainCamera.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime);
            elapsedTime += Time.deltaTime * moveSpeed;
            yield return null;
        }

        mainCamera.transform.position = endPos;


        // Load new scene
        SceneManager.LoadScene(nextSceneName);
    }

    private IEnumerator FadeToBlack()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            fadeCanvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        fadeCanvasGroup.alpha = 1f;
    }
}
