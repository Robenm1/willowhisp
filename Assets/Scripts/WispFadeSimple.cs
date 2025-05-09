using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;
using UnityEngine.SceneManagement;

public class WispFadeSimple : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Light2D light2D;
    public float fadeDuration = 1.5f;

    void Awake()
    {
        // Only allow in "MainMenu" scene
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            this.enabled = false;
            return;
        }

        sprite = GetComponent<SpriteRenderer>();
        light2D = GetComponentInChildren<Light2D>();

        SetAlpha(0f);
        if (light2D != null) light2D.intensity = 0f;
    }

    public void FadeIn()
    {
        if (!enabled) return;
        StopAllCoroutines();
        StartCoroutine(FadeRoutine(0f, 1f));
    }

    public void FadeOut()
    {
        if (!enabled) return;
        StopAllCoroutines();
        StartCoroutine(FadeRoutine(1f, 0f));
    }

    IEnumerator FadeRoutine(float from, float to)
    {
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float t = time / fadeDuration;
            float value = Mathf.Lerp(from, to, t);

            SetAlpha(value);
            if (light2D != null) light2D.intensity = value;

            yield return null;
        }

        SetAlpha(to);
        if (light2D != null) light2D.intensity = to;
    }

    void SetAlpha(float alpha)
    {
        if (sprite != null)
        {
            Color color = sprite.color;
            color.a = alpha;
            sprite.color = color;
        }
    }
}
