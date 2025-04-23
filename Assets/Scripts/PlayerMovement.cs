using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float move = Input.GetAxisRaw("Horizontal");

        // Maintain your existing velocity logic
        rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocityY);

        // Play animations
        if (Mathf.Abs(move) > 0.01f)
        {
            animator.Play("WilloRun");

            // Flip sprite based on direction
            sprite.flipX = move < 0;
        }
        else
        {
            animator.Play("WilloIdle");
        }
    }
}
