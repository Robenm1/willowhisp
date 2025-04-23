using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class BluePlatform : MonoBehaviour
{
    public float activeDuration = 3f;
    public Color blueColor = new Color(0f, 0.5f, 1f);
    public float tolerance = 0.05f;

    private SpriteRenderer sprite;
    private Collider2D col;

    private bool isActive = false;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        // Leave collider ON so it can be clicked, but platform invisible and pass-through
        sprite.enabled = false;
        col.isTrigger = true;
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

            if (col.OverlapPoint(mousePos))
            {
                StartCoroutine(ActivateTemporarily());
            }
        }
    }

    IEnumerator ActivateTemporarily()
    {
        SetPlatformState(true);
        yield return new WaitForSeconds(activeDuration);
        SetPlatformState(false);
    }

    void SetPlatformState(bool state)
    {
        isActive = state;
        sprite.enabled = state;
        col.isTrigger = !state; // When active = solid. When inactive = ghost

        Debug.Log("Platform is now " + (state ? "ACTIVE" : "INACTIVE"));
    }

    bool IsColorMatch(Color a, Color b)
    {
        return Mathf.Abs(a.r - b.r) < tolerance &&
               Mathf.Abs(a.g - b.g) < tolerance &&
               Mathf.Abs(a.b - b.b) < tolerance;
    }
}
