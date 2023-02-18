using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private float jumpForce = 8f;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.relativeVelocity.y <= 0f)
        {
            Rigidbody2D rb = col.collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 velocity = rb.velocity;
                velocity.y = jumpForce;
                rb.velocity = velocity;
            }
        }
    }
}
