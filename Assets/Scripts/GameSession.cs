using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    private int _score;
    private float _highestHeight;
    
    [SerializeField] private TextMeshProUGUI scoreText;
    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;

        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ProcessScoreCount(float newYPosition)
    {
        _score += (int)(newYPosition * 0.5);
        scoreText.text = _score.ToString();
    }
    
    public void UpdateHighestHeight(float newHeight)
    {
        _highestHeight = newHeight;
    }
    
    public float GetHighestHeight()
    {
        return _highestHeight;
    }

    public void ProcessPlayerDeath()
    {
        SceneManager.LoadScene("DeadScene");
        Destroy(gameObject);
    }
}
