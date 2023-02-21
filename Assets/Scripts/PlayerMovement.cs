using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;

    private Vector2 _moveInput;
    private Rigidbody2D _rb;
    private CapsuleCollider2D _capsuleCollider;
    private CircleCollider2D _circleCollider;
    private Animator _animator;
    private GameSession _gameSession;

    void Start()
    {
        _gameSession = FindObjectOfType<GameSession>();
        _rb = GetComponent<Rigidbody2D>();
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
        _circleCollider = GetComponent<CircleCollider2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        OnTouchingHazard();
        UpdateScore();
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

    void OnTouchingHazard()
    {
        if (_capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Hazard")))
        {
            _animator.SetTrigger("Dying");
            _capsuleCollider.isTrigger = true;
            _circleCollider.isTrigger = true;
            _rb.velocity = new Vector2(_rb.velocity.x, -5f);
        }
    }

    void UpdateScore()
    {
        var highestHeight = _gameSession.GetHighestHeight();
        if (_rb.position.y > highestHeight && _rb.velocity.y > 0)
        {
            _gameSession.UpdateHighestHeight(_rb.position.y);
            _gameSession.ProcessScoreCount(_rb.position.y);
        }
    }

    private void OnBecameInvisible()
    {
        if (_rb.velocity.y < 0 && _rb.position.y < 80)
        {
            SceneManager.LoadScene("DeadScene");
        }
    }
}
