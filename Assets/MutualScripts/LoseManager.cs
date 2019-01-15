using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoseManager : MonoBehaviour
{
    public ScoreManager scoreManager;
    public Text current;
    public Text high; 
    // Start is called before the first frame update
    public void updateScore()
    {
        scoreManager.scoreDisplay(current);
        scoreManager.highScoreDisPlay(high);
    }
}
