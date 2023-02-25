using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;

    private Vector2 _moveInput;
    private Rigidbody2D _rb;
    private CapsuleCollider2D _capsuleCollider;
    private CircleCollider2D _circleCollider;
    private Animator _animator;
    
    private bool _isShielded;
    private const float ShieldEffectDuration = 6.9f;

    void Start()
    {
        
        _rb = GetComponent<Rigidbody2D>();
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
        _circleCollider = GetComponent<CircleCollider2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        FlipSprite();
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
    
    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(_rb.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(_rb.velocity.x) * 1.25f, 1.25f);
        }
    }

    void OnTouchingHazard()
    {
        if (!_isShielded && _capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Hazard")))
        {
            _animator.SetTrigger("Dying");
            _capsuleCollider.isTrigger = true;
            _circleCollider.isTrigger = true;
            _rb.velocity = new Vector2(_rb.velocity.x, -3f);
            // this will automatically trigger the OnBecameInvisible() method
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    void UpdateScore()
    {
        if (_rb.velocity.y > 0)
        {
            FindObjectOfType<GameSession>().ProcessScoreCount(_rb.position.y);
        }
    }

    private void OnBecameInvisible()
    {
        if (_rb.velocity.y < 0 && _rb.position.y < 80)
        {
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

    public void OnTouchingShield()
    {
        _isShielded = true;
        _animator.SetBool("isShielding", true);
        
        StartCoroutine(RemoveShield());
    }
    
    IEnumerator RemoveShield()
    {
        yield return new WaitForSeconds(ShieldEffectDuration);
        
        _isShielded = false;
        _animator.SetBool("isShielding", false);  
    }
}
