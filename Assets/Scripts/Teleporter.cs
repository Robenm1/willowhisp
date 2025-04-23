using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Teleporter : MonoBehaviour
{
    public Color purpleColor = new Color(0.5f, 0f, 1f);
    public Color orangeColor = new Color(1f, 0.5f, 0f);
    public float tolerance = 0.05f;
    public float teleportOffsetY = 1f;
    public float jumpForce = 7f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;

            Collider2D hit = Physics2D.OverlapPoint(mousePos);
            if (hit != null && hit.gameObject == gameObject)
            {
                GameObject player = GameObject.FindWithTag("Player");
                Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
                Light2D playerLight = player.GetComponentInChildren<Light2D>();
                Light2D crosshairLight = GameObject.Find("Crosshair")?.GetComponentInChildren<Light2D>();

                if (player == null || playerLight == null || crosshairLight == null || rb == null) return;

                bool isPlayerPurple = IsColorMatch(playerLight.color, purpleColor);
                bool isCrosshairPurple = IsColorMatch(crosshairLight.color, purpleColor);
                bool isCrosshairOrange = IsColorMatch(crosshairLight.color, orangeColor);

                if (isPlayerPurple && isCrosshairPurple)
                {
                    player.transform.position = transform.position + Vector3.up * teleportOffsetY;
                    Debug.Log("Teleported (Purple + Purple)");
                }
                else if (isPlayerPurple && isCrosshairOrange)
                {
                    player.transform.position = transform.position + Vector3.up * teleportOffsetY;
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                    Debug.Log("Teleported + Jumped (Purple + Orange)");
                }
                else
                {
                    Debug.Log("Invalid color combo for teleporter.");
                }
            }
        }
    }

    bool IsColorMatch(Color a, Color b)
    {
        return Mathf.Abs(a.r - b.r) < tolerance &&
               Mathf.Abs(a.g - b.g) < tolerance &&
               Mathf.Abs(a.b - b.b) < tolerance;
    }
}
