using UnityEngine;

public class Wisp : MonoBehaviour
{
    public float revealDistance = 2f;
    private SpriteRenderer spriteRenderer;
    private Transform crosshair;
    private float fadeSpeed = 3f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        crosshair = GameObject.Find("Crosshair").transform;

        // Start hidden
        Color startColor = spriteRenderer.color;
        spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, 0f);
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, crosshair.position);
        float targetAlpha = (distance <= revealDistance) ? 1f : 0f;

        Color currentColor = spriteRenderer.color;
        float newAlpha = Mathf.MoveTowards(currentColor.a, targetAlpha, fadeSpeed * Time.deltaTime);
        spriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
    }
}
