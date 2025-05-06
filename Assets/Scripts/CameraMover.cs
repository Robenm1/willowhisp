using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public Transform mainMenuTarget;
    public Transform optionsTarget;
    public float moveSpeed = 5f;

    private Transform targetPosition;
    private bool isMoving = false;

    void Update()
    {
        if (isMoving && targetPosition != null)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition.position, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition.position) < 0.01f)
            {
                transform.position = targetPosition.position;
                isMoving = false;
            }
        }
    }

    public void MoveToMainMenu()
    {
        targetPosition = mainMenuTarget;
        isMoving = true;
    }

    public void MoveToOptions()
    {
        targetPosition = optionsTarget;
        isMoving = true;
    }
}
