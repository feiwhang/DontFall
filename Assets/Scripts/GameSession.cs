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
        if (newYPosition > _highestHeight)
        {
            var sceneIndex = SceneManager.GetActiveScene().buildIndex;
            _score += (int)(newYPosition * 0.25 * sceneIndex);
            scoreText.text = _score.ToString();
            UpdateHighestHeight(newYPosition);
        }
    }
    
    public void UpdateHighestHeight(float newHeight)
    {
        _highestHeight = newHeight;
    }
    
    public void ProcessPlayerDeath()
    {
        var currentHighScore = PlayerPrefs.GetInt("HighScore", 0);
        if (_score > currentHighScore)
        {
            PlayerPrefs.SetInt("HighScore", _score);
        }
        PlayerPrefs.SetInt("FinalScore", _score);
        Destroy(gameObject);
        SceneManager.LoadScene("DeadScene");
    }
}
