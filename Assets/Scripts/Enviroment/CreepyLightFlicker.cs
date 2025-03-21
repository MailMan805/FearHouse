using UnityEngine;
using System.Collections;

public class CreepyLightFlicker : MonoBehaviour
{
    public Light flickeringLight;
    public float minIntensity = 0.5f;
    public float maxIntensity = 2f;
    public float flickerSpeed = 0.1f;
    public bool enableLightFlashes = true;
    public float flashChance = 0.1f;
    public float flashDuration = 0.05f;

    private void Start()
    {
        if (flickeringLight == null)
            flickeringLight = GetComponent<Light>();

        StartCoroutine(FlickerLight());
    }

    private IEnumerator FlickerLight()
    {
        while (true)
        {
            // Randomize intensity
            flickeringLight.intensity = Random.Range(minIntensity, maxIntensity);

            // Chance to briefly turn off the light (like a horror effect)
            if (enableLightFlashes && Random.value < flashChance)
            {
                flickeringLight.enabled = false;
                yield return new WaitForSeconds(flashDuration);
                flickeringLight.enabled = true;
            }

            yield return new WaitForSeconds(Random.Range(flickerSpeed * 0.5f, flickerSpeed * 1.5f));
        }
    }
}
