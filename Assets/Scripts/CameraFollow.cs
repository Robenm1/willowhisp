using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;           // Assign player in Inspector
    public Vector2 margin = new Vector2(2f, 1.5f); // How far player can move before camera follows
    public float smoothTime = 0.2f;

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (player == null) return;

        Vector3 targetPos = transform.position;

        // Check horizontal margin
        if (Mathf.Abs(transform.position.x - player.position.x) > margin.x)
        {
            targetPos.x = Mathf.Lerp(transform.position.x, player.position.x, smoothTime);
        }

        // Check vertical margin
        if (Mathf.Abs(transform.position.y - player.position.y) > margin.y)
        {
            targetPos.y = Mathf.Lerp(transform.position.y, player.position.y, smoothTime);
        }

        // Keep Z unchanged (important!)
        targetPos.z = transform.position.z;

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }
}
