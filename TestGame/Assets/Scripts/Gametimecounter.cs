using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gametimecounter : MonoBehaviour
{
    public int score;
    public Slider totalLoad;
    public Text totaltext;
    public float totaltime = 59;
    public bool GameStart = false;
    public Image slidercolorchange;
    // Start is called before the first frame update
    void Start()
    {
        GameStart = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        /*60 sec clock function*/
        if (GameStart)
        {
            if (totaltime > 0)
            {
                totaltime -= Time.deltaTime;
                DisplayTime(totaltime);
                totalLoad.value = totaltime;
                
            }
            else
            {
                totaltime = 0;
                GameStart = false;
                GameOver();
            }


        }
        score = this.gameObject.GetComponent<GameLogic>().score;
        PlayerPrefs.SetInt("currentscore", score);
        PlayerPrefs.Save();
    }
    /*60 sec Game clock UI*/
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        totaltext.text = string.Format(seconds + "s");
    }
    /*GameOver function*/
    public void GameOver()
    {
        SceneManager.LoadSceneAsync("GameOver");
    }
}
