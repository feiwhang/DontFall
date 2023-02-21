using TMPro;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private float highestHeight;
    
    [SerializeField] private TextMeshProUGUI scoreText;
    void Awake()
    {
        score = 0;
        highestHeight = 0f;
        
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
        score += (int)(newYPosition * 0.5);
        scoreText.text = score.ToString();
    }
    
    public void UpdateHighestHeight(float newHeight)
    {
        highestHeight = newHeight;
    }
    
    public float GetHighestHeight()
    {
        return highestHeight;
    }

    // public void ProcessPlayerDeath()
    // {
    //     
    // }
}
