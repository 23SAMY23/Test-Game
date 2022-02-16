using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    [Header("Loading")]
    public float loadtime = 3;
    public Text loadtext;

    // Update is called once per frame
    void Update()
    {
        /*3 sec start scene function*/
        if (loadtime > 0)
        {
            loadtime -= Time.deltaTime;
            DisplayTime(loadtime);


        }
        else
        {
            SceneManager.LoadSceneAsync("MainGame");
        }
    }
    /*3 sec start scene UI*/
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        loadtext.text = string.Format(seconds + "");
    }
}
