using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CrosshairFollow : MonoBehaviour
{
    public Light2D crosshairLight;
    private Color storedColor = Color.white;
    private bool colorTransferred = false;

    void Update()
    {
        FollowMouse();

        if (Input.GetMouseButtonDown(0))
            TryAbsorbWisp();

        if (Input.GetMouseButtonDown(1))
            HandleRightClick();

        if (Input.GetKeyDown(KeyCode.E))
            ResetAll();
    }

    void FollowMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        transform.position = mousePos;
    }

    void TryAbsorbWisp()
    {
        Vector3 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hit = Physics2D.OverlapPoint(clickPos);

        if (hit != null && hit.CompareTag("Wisp"))
        {
            Light2D wispLight = hit.GetComponentInChildren<Light2D>();
            if (wispLight != null)
            {
                storedColor = wispLight.color;
                crosshairLight.color = storedColor;
                colorTransferred = false;
            }
        }
    }

    void HandleRightClick()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null) return;

        Light2D playerLight = player.GetComponentInChildren<Light2D>();
        if (playerLight == null) return;

        if (!colorTransferred)
        {
            playerLight.color = storedColor;
            colorTransferred = true;
        }
        else
        {
            storedColor = Color.white;
            crosshairLight.color = Color.white;
            colorTransferred = false;
        }
    }

    void ResetAll()
    {
        storedColor = Color.white;
        crosshairLight.color = Color.white;

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Light2D playerLight = player.GetComponentInChildren<Light2D>();
            if (playerLight != null)
                playerLight.color = Color.white;
        }

        colorTransferred = false;
    }
}
