using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;

    private Vector2 _moveInput;
    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
    }

    void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }

    void Move()
    {
        Vector2 playerVelocity = new Vector2(_moveInput.x * moveSpeed, _rb.velocity.y);

        _rb.velocity = playerVelocity;
    }

    private void OnBecameInvisible()
    {
        if (_rb.velocity.y < 0 && _rb.position.y < 80)
        {
            SceneManager.LoadScene("DeadScene");
        }
    }
}
