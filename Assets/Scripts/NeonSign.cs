using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class NeonFlickerCycle : MonoBehaviour
{
    public Light2D flickerLight;

    [Header("Broken Flicker Settings")]
    public float brokenFlickerDuration = 2f;
    public float minBrokenInterval = 0.05f;
    public float maxBrokenInterval = 0.2f;

    [Header("Stable Flicker Settings (intensity flicker)")]
    public float stableFlickerDuration = 1.5f;
    public float minIntensity = 0.5f;
    public float maxIntensity = 1.5f;

    [Header("Cycle Timing")]
    public float offDuration = 2f;
    public float steadyStartDelay = 4f;

    private float baseIntensity;

    void Start()
    {
        if (flickerLight != null)
        {
            baseIntensity = flickerLight.intensity;
            flickerLight.intensity = baseIntensity;
            flickerLight.enabled = true;
            StartCoroutine(FlickerCycle());
        }
    }

    IEnumerator FlickerCycle()
    {
        yield return new WaitForSeconds(steadyStartDelay);

        while (true)
        {
            // 💥 Broken Flicker: off/on flashes
            float brokenTime = 0f;
            while (brokenTime < brokenFlickerDuration)
            {
                flickerLight.enabled = !flickerLight.enabled;
                float wait = Random.Range(minBrokenInterval, maxBrokenInterval);
                yield return new WaitForSeconds(wait);
                brokenTime += wait;
            }

            // 🕯️ Turn off for a while
            flickerLight.enabled = false;
            yield return new WaitForSeconds(offDuration);

            // 💡 Turn on and stay lit
            flickerLight.enabled = true;

            // 🌟 Intensity flicker: light stays ON but pulses
            float stableTime = 0f;
            while (stableTime < stableFlickerDuration)
            {
                flickerLight.intensity = Random.Range(minIntensity, maxIntensity);
                yield return new WaitForSeconds(0.05f);
                stableTime += 0.05f;
            }

            // 🔁 Reset intensity to default
            flickerLight.intensity = baseIntensity;
            yield return new WaitForSeconds(steadyStartDelay);
        }
    }
}
