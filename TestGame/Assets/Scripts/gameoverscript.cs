using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameoverscript : MonoBehaviour
{
    public int highscore;
    public Text HighScoretext;
    public int currentscore;
    public Text CurrentScoretext;
    // Start is called before the first frame update
    void Start()
    {
        /*Display current and high score*/
        currentscore = PlayerPrefs.GetInt("currentscore");
        CurrentScoretext.text = "CurrentScore:" + currentscore;

        highscore = PlayerPrefs.GetInt("HighScore");
        HighScoretext.text = "HighScore:" + highscore;
    }
    /*Play again function*/
    public void playagain()
    {
        SceneManager.LoadSceneAsync("Start");
    }
}
