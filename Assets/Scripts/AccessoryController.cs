using UnityEngine;

public class AccessoryController : MonoBehaviour
{
    private Platform _platform;
    
    void Start()
    {
        _platform = FindObjectOfType<Platform>();
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _platform.OnTouchingAccessory(); 
        }
    }
}
