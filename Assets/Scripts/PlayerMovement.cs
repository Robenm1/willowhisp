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

        // Movement logic (keep your own custom Y handling)
        rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocityY);

        // Set speed parameter for animation blend
        animator.SetFloat("Speed", Mathf.Abs(move));

        // Flip sprite
        if (move != 0)
        {
            sprite.flipX = move < 0;
        }
    }
}
