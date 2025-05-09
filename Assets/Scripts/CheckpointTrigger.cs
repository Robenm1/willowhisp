using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CheckpointTrigger : MonoBehaviour
{
    public GameObject redLightObject;
    public GameObject greenLightObject;
    private bool activated = false;

    void Start()
    {
        // Make sure lights are in correct starting state
        if (redLightObject) redLightObject.SetActive(true);
        if (greenLightObject) greenLightObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (activated) return;

        if (other.CompareTag("Player"))
        {
            ActivateCheckpoint();
        }
    }

    void ActivateCheckpoint()
    {
        activated = true;

        if (redLightObject) redLightObject.SetActive(false);
        if (greenLightObject) greenLightObject.SetActive(true);

        CheckpointManager.Instance.SetCheckpoint(transform.position);
        Debug.Log("Checkpoint Activated!");
    }
}
