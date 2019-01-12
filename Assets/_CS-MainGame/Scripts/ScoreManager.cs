using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int score = 0;
    public Text scoreText;
    public const string highScore = "HighScore";
    // Use this for initialization
    private void Awake()
    {
        if (instance == null) instance = this;
        _setupScore();
    }
    public void scoreDisplay()
    {
        scoreText.text = getScore().ToString();
    }
    public void scoreDisplay(Text _scoreText)
    {
        _scoreText.text = getScore().ToString();
    }
    public void highScoreDisPlay(Text highText)
    {
        highText.text = PlayerPrefs.GetInt(highScore).ToString();
    }
    public void setScore(int _score)
    {
        score = _score;
    }
    public int getScore()
    {
        return score;
    }
    public void setHighScore(int _score)
    {
        if (_score > PlayerPrefs.GetInt(highScore))
        {
            PlayerPrefs.SetInt(highScore, _score);
        }
    }
    public int getHighScore()
    {
        return PlayerPrefs.GetInt(highScore);
    }
    //public void _setupScore()
    //{
    //    if (PlayerPrefs.GetInt("checkWatchVideo") == 1)
    //    {
    //        PlayerPrefs.SetInt("checkWatchVideo", 0);
    //        setScore(PlayerPrefs.GetInt("currentScore"));
    //        scoreDisplay();
    //    }
    //    else PlayerPrefs.SetInt("currentScore", 0);
    //}
    public void _setupScore()
    {
        setScore(PlayerPrefs.GetInt("currentScore"));
        scoreDisplay();
    }
}
