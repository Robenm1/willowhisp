using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class BluePlatform : MonoBehaviour
{
    public float activeDuration = 3f;
    public Color blueColor = new Color(0f, 0.5f, 1f); // The blue wisp color
    public float tolerance = 0.05f;

    private SpriteRenderer sprite;
    private Collider2D col;
    private Animator animator;

    private bool isActive = false;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();

        sprite.enabled = true;          // Always visible
        col.isTrigger = true;           // Not solid by default

        if (animator != null)
        {
            animator.Play("BluePlatformAir"); // Start with air animation
        }
    }

    void Update()
    {
        Light2D crosshairLight = GameObject.Find("Crosshair")?.GetComponentInChildren<Light2D>();
        if (crosshairLight == null) return;

        bool crosshairIsBlue = IsColorMatch(crosshairLight.color, blueColor);

        if (Input.GetMouseButtonDown(0) && crosshairIsBlue)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;

            if (col.OverlapPoint(mousePos) && !isActive)
            {
                StartCoroutine(ActivateTemporarily());
            }
        }
    }

    IEnumerator ActivateTemporarily()
    {
        isActive = true;

        if (animator != null)
        {
            animator.Play("BluePlatformFreeze"); // Play freeze animation once
        }

        yield return new WaitForSeconds(0.2f); // Wait for animation to begin
        col.isTrigger = false; // Now platform is solid

        yield return new WaitForSeconds(activeDuration);

        col.isTrigger = true; // Back to ghost mode
        isActive = false;

        if (animator != null)
        {
            animator.Play("BluePlatformAir"); // Return to air animation
        }
    }

    bool IsColorMatch(Color a, Color b)
    {
        return Mathf.Abs(a.r - b.r) < tolerance &&
               Mathf.Abs(a.g - b.g) < tolerance &&
               Mathf.Abs(a.b - b.b) < tolerance;
    }
}
