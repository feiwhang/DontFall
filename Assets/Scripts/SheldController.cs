using UnityEngine;

public class SheldController : MonoBehaviour
{
    private Player _player;
    
    void Start()
    {
        _player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _player.OnTouchingShield();
        }
    }
}
