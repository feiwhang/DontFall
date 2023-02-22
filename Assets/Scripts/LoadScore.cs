using TMPro;
using UnityEngine;

public class LoadScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    
    void Start()
    {
        SetScoreText();
    }

    private void SetScoreText()
    {
        var finalScore = PlayerPrefs.GetInt("FinalScore", 0);
        var highScore = PlayerPrefs.GetInt("HighScore", 0);

        finalScoreText.text = finalScore.ToString();
        highScoreText.text = finalScore > highScore 
            ? "NEW HIGHT SCORE!" 
            : $"HIGH SCORE: {highScore}";
    }
}
