using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CrosshairFollow : MonoBehaviour
{
    public Light2D crosshairLight; // assign in Inspector
    private Color storedColor = Color.white;
    private bool colorTransferred = false;

    void Update()
    {
        // Follow the mouse
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        transform.position = mousePos;

        // Absorb wisp color (Left Click)
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D hit = Physics2D.OverlapPoint(mousePos);
            if (hit && hit.CompareTag("Wisp"))
            {
                SpriteRenderer wispSprite = hit.GetComponent<SpriteRenderer>();
                if (wispSprite != null)
                {
                    storedColor = wispSprite.color;
                    crosshairLight.color = storedColor;
                    colorTransferred = false; // reset transfer status
                }
            }
        }

        // Transfer to player (1st Right Click), then reset crosshair (2nd Right Click)
        if (Input.GetMouseButtonDown(1))
        {
            if (!colorTransferred)
            {
                GameObject player = GameObject.FindWithTag("Player");
                if (player != null)
                {
                    Light2D playerLight = player.GetComponentInChildren<Light2D>();
                    if (playerLight != null)
                    {
                        playerLight.color = storedColor;
                        colorTransferred = true; // mark as transferred
                    }
                }
            }
            else
            {
                crosshairLight.color = Color.white;
                storedColor = Color.white;
                colorTransferred = false; // reset
            }
        }

        // Reset both (E key)
        if (Input.GetKeyDown(KeyCode.E))
        {
            storedColor = Color.white;
            crosshairLight.color = Color.white;

            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                Light2D playerLight = player.GetComponentInChildren<Light2D>();
                if (playerLight != null)
                {
                    playerLight.color = Color.white;
                }
            }

            colorTransferred = false;
        }
    }
}
