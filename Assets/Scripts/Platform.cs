using System.Collections;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private const float DefaultJumpForce = 8f;
    private const float AccessoryJumpForce = 12f;
    private const float AccessoryEffectDuration = 5f;
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
    
    public void OnTouchingAccessory()
    {
        jumpForce = AccessoryJumpForce;
        StartCoroutine(ResetJumpForce());
    }
    
    IEnumerator ResetJumpForce()
    {
        yield return new WaitForSeconds(AccessoryEffectDuration);
        jumpForce = DefaultJumpForce;
    }
}
