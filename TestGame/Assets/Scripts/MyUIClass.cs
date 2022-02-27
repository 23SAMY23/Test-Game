using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MyUIClass : MonoBehaviour
{
    [SerializeField] private GameLogic _myGameLogic;

    [Header("Arrow")]
    public static GameObject arrow_LeftButton;
    public static GameObject arrow_RightButton;

    [Header("Shapes")]
    public static Transform shapesGameobject;
    public List<GameObject> shapechildren = new List<GameObject>();
    public static  int shapeno;

    [Header("Loading")]
    public static Text shapeText;
    public static Animation shapeloadanime;

    [Header("Time")]
    public static float ShapeTime;
    public static bool GameRunning;

    [Header("Scores")]
    public static int score;
    public static Text currentscore;
    public static int highScore;

    [Header("ShapeTag")]
    public static string currenttag;

    [Header("FullGameTime")]
    public static Slider totalLoad;
    public static Text totaltext;
    public static float totaltime;
    public static Image slidercolorchange;


    private void Start()
    {
        _myGameLogic = this.GetComponent<GameLogic>();
        FindAllChildren(shapesGameobject);
        _myGameLogic.onGameStarted += SetGameStartedUI;
        GameLogic.rightClick.AddListener(ArrowRightSelect);
        GameLogic.leftClick.AddListener(ArrowLeftSelect);
    }

    private void SetGameStartedUI()
    {

        MyUIClass.GameRunning = true;
        totaltime = 59;
        ShapeTime = 4;
        totaltext = GameObject.FindGameObjectWithTag("GameTime").GetComponent<Text>();
        totalLoad = GameObject.FindGameObjectWithTag("GameSlider").GetComponent<Slider>();

        currentscore = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
       
        shapeText = GameObject.FindGameObjectWithTag("ShapeTag").GetComponent<Text>();
        shapeloadanime = GameObject.FindGameObjectWithTag("LoadAnimation").GetComponent<Animation>();
        MyUIClass.highScore = PlayerPrefs.GetInt("HighScore");
        ShapeChange();
        Debug.Log(shapechildren.Count);
    }
    public void FindAllChildren(Transform shapesGameobject)
    {
        shapesGameobject = GameObject.FindGameObjectWithTag("Shapes").transform;
        int len = shapesGameobject.childCount;

        for (int i = 0; i < len; i++)
        {
           shapechildren.Add(shapesGameobject.GetChild(i).gameObject);

            if (shapesGameobject.GetChild(i).childCount > 0)
                FindAllChildren(shapesGameobject.GetChild(i).transform);
            
        }
    }
    /*Shape change funtion*/
    public void ShapeChange()
    {
        shapeno = Random.Range(0, 4);
        for (int i = 0; i < shapechildren.Count; i++)
        {
            if (i != shapeno)
            {
            
            shapechildren[i].SetActive(false);
            }
            else 
            {
               shapechildren[i].SetActive(true);
               currenttag = shapechildren[shapeno].tag;
            }
           
        }
    }
    public void ArrowLeftSelect(GameLogic left)
    {
        if (MyUIClass.currenttag == "Triangle" || MyUIClass.currenttag == "Pentagon")
        {
            MyUIClass.score += 100;
        }
        else
        {
            MyUIClass.score -= 200;
        }
        MyUIClass.ShapeTime = 4;
        ShapeChange();
        MyUIClass.shapeloadanime.Stop();
        MyUIClass.shapeloadanime.Play("4sclock");
    }
    /*Display right arrow funtion*/
    public void ArrowRightSelect(GameLogic right)
    {
        if (MyUIClass.currenttag == "Square" || MyUIClass.currenttag == "Circle")
        {
            MyUIClass.score += 100;
        }
        else
        {
            MyUIClass.score -= 200;
        }
        MyUIClass.ShapeTime = 4;
        ShapeChange();
        MyUIClass.shapeloadanime.Stop();
        MyUIClass.shapeloadanime.Play("4sclock");
    }
    public void GameWatch()
    {
        MyUIClass.currentscore.text = "Score:" + MyUIClass.score;

        /*4s clock running function*/
        if (MyUIClass.GameRunning)
        {
            if (MyUIClass.ShapeTime > 0)
            {
                MyUIClass.ShapeTime -= Time.deltaTime;
                DisplayTime(MyUIClass.ShapeTime);
            }
            else
            {
                MyUIClass.score -= 200;
                MyUIClass.ShapeTime = 4;

                ShapeChange();
            }
        }
        /*score limited to 0*/
        if (MyUIClass.score < 0)
        {
            MyUIClass.score = 0;
        }
        /*Score save if higher then highscore*/
        if (MyUIClass.score > MyUIClass.highScore)
        {
            PlayerPrefs.SetInt("HighScore", MyUIClass.score);
            PlayerPrefs.Save();
        }

    }
    /*Display 4 secound clock in UI*/
   public static void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        MyUIClass.shapeText.text = string.Format(seconds + "s");
    }
    public static void FullGametime()
    {
        /*60 sec clock function*/
        if (GameRunning)
        {
            if (totaltime > 0)
            {
                totaltime -= Time.deltaTime;
                GameDisplayTime(totaltime);
                totalLoad.value = totaltime;

            }
            else
            {
                totaltime = 0;
                GameRunning = false;
                GameOver();
            }


        }
        //  score = this.gameObject.GetComponent<GameLogic>().score;
        PlayerPrefs.SetInt("currentscore", score);
        PlayerPrefs.Save();
    }
    /*60 sec Game clock UI*/
    public static void GameDisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        totaltext.text = string.Format(seconds + "s");
    }
    /*GameOver function*/
    public static void GameOver()
    {
        SceneManager.LoadSceneAsync("GameOver");
    }

}



