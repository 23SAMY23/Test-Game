using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    [Header("Arrow")]
    public GameObject arrow_LeftButton;
    public GameObject arrow_RightButton;

    [Header("Shapes")]
    public GameObject[] shapes;
    public int shapeno;

    [Header("Loading")]
    public Image shapeLoad;
    public Text shapeText;
    public Animation shapeloadanime;

    [Header("Time")]
    public float ShapeTime = 4;
    public bool GameRunning = false;

    [Header("Scores")]
    public int score;
    public Text currentscore;
    public int highScore;

    [Header("ShapeTag")]
    public string currenttag;

    // Start is called before the first frame update
    void Start()
    {
        shapeno = Random.Range(0, 4);
        shapes[shapeno].gameObject.SetActive(true);
        currenttag = shapes[shapeno].tag;
        GameRunning = true;
        highScore = PlayerPrefs.GetInt("HighScore");
    }

    // Update is called once per frame
    void Update()
    {
        currentscore.text = "Score:" + score;

        /*4s clock running function*/
        if (GameRunning)
        {
            if (ShapeTime > 0)
            {
                ShapeTime -= Time.deltaTime;
                DisplayTime(ShapeTime);
            }
            else
            {
                score -= 200;
               ShapeTime = 4;
             
               Shapeselection();
               
            }
        }
        /*score limited to 0*/
        if (score < 0)
        {
            score = 0;
        }
        /*Score save if higher then highscore*/
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }
    }
    /*Display 4 secound clock in UI*/
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        shapeText.text = string.Format(seconds + "s");
    }
    /*Display Left arrow funtion*/
    public void ArrowLeftSelect()
    {
       if(currenttag == "Triangle" || currenttag == "Pentagon")
        {
            score += 100;
        }
        else
        {
            score -= 200;
        }
        ShapeTime = 4;
       
        Shapeselection();
        shapeloadanime.Stop();
        shapeloadanime.Play("4sclock");
    }
    /*Display right arrow funtion*/
    public void ArrowRightSelect()
    {
        if (currenttag == "Square" || currenttag == "Circle")
        {
            score += 100;
        }
        else
        {
            score -= 200;
        }
        ShapeTime = 4;

        Shapeselection();
        shapeloadanime.Stop();
        shapeloadanime.Play("4sclock");
    }
    /*Shape change funtion*/
    public void Shapeselection()
    {
        shapeno = Random.Range(0, 4);
        if (shapeno == 0)
        {
            shapes[0].gameObject.SetActive(true);
            shapes[1].gameObject.SetActive(false);
            shapes[2].gameObject.SetActive(false);
            shapes[3].gameObject.SetActive(false);
            currenttag = shapes[0].tag;

        }
        else if (shapeno == 1)
        {
            shapes[1].gameObject.SetActive(true);
            shapes[0].gameObject.SetActive(false);
            shapes[2].gameObject.SetActive(false);
            shapes[3].gameObject.SetActive(false);
            currenttag = shapes[1].tag;
        }
        else if (shapeno == 2)
        {
            shapes[2].gameObject.SetActive(true);
            shapes[0].gameObject.SetActive(false);
            shapes[1].gameObject.SetActive(false);
            shapes[3].gameObject.SetActive(false);
            currenttag = shapes[2].tag;
        }
        else if (shapeno == 3)
        {
            shapes[3].gameObject.SetActive(true);
            shapes[0].gameObject.SetActive(false);
            shapes[1].gameObject.SetActive(false);
            shapes[2].gameObject.SetActive(false);
            currenttag = shapes[3].tag;
        }

    }
}
