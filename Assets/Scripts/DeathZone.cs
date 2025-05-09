using UnityEngine;

public class DeathZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 respawnPos = CheckpointManager.Instance.GetCheckpoint();
            other.transform.position = respawnPos;
            Debug.Log("Player died → Respawning at checkpoint");
        }
    }
}
