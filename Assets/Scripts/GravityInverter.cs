using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GravityInverter : MonoBehaviour
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

                bool isPlayerOrange = IsColorMatch(playerLight.color, orangeColor);
                bool isCrosshairOrange = IsColorMatch(crosshairLight.color, orangeColor);
                bool isCrosshairPurple = IsColorMatch(crosshairLight.color, purpleColor);

                if (isPlayerOrange && isCrosshairOrange)
                {
                    InvertGravity(rb, player);
                    Debug.Log("Gravity Inverted (Orange + Orange)");
                }
                else if (isPlayerOrange && isCrosshairPurple)
                {
                    player.transform.position = transform.position + Vector3.down * teleportOffsetY;
                    InvertGravity(rb, player);

                    float gravityDir = Mathf.Sign(rb.gravityScale);
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce * gravityDir);

                    Debug.Log("Teleport + Invert Gravity + Jump (Orange + Purple)");
                }
                else
                {
                    Debug.Log("Invalid color combo for gravity orb.");
                }
            }
        }
    }

    void InvertGravity(Rigidbody2D rb, GameObject player)
    {
        rb.gravityScale *= -1;

        Vector3 scale = player.transform.localScale;
        scale.y *= -1;
        player.transform.localScale = scale;
    }

    bool IsColorMatch(Color a, Color b)
    {
        return Mathf.Abs(a.r - b.r) < tolerance &&
               Mathf.Abs(a.g - b.g) < tolerance &&
               Mathf.Abs(a.b - b.b) < tolerance;
    }
}
