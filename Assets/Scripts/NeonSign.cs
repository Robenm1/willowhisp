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
    private bool wispsFadedIn = false;

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
        // Initial delay (light stays on at first)
        yield return new WaitForSeconds(steadyStartDelay);

        while (true)
        {
            // 💥 Broken Flicker (light toggles on/off)
            float flickerTime = 0f;
            while (flickerTime < brokenFlickerDuration)
            {
                flickerLight.enabled = !flickerLight.enabled;
                float wait = Random.Range(minBrokenInterval, maxBrokenInterval);
                yield return new WaitForSeconds(wait);
                flickerTime += wait;
            }

            // 🕯️ Light turns off
            flickerLight.enabled = false;

            // 🟣 Fade in wisps only once
            if (!wispsFadedIn)
            {
                foreach (var wisp in FindObjectsOfType<WispFadeSimple>())
                {
                    wisp.FadeIn();
                }
                wispsFadedIn = true;
            }

            yield return new WaitForSeconds(offDuration);

            // 💡 Light turns back on
            flickerLight.enabled = true;

            // 🌟 Flicker intensity randomly (but stay on)
            float stableTime = 0f;
            while (stableTime < stableFlickerDuration)
            {
                flickerLight.intensity = Random.Range(minIntensity, maxIntensity);
                yield return new WaitForSeconds(0.05f);
                stableTime += 0.05f;
            }

            // 🔁 Reset to steady brightness and delay before looping
            flickerLight.intensity = baseIntensity;
            yield return new WaitForSeconds(steadyStartDelay);
        }
    }
}
