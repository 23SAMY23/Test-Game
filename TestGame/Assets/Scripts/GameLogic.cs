using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class buttonevent : UnityEvent<GameLogic> { }
public class GameLogic : MonoBehaviour
{
    public Action onGameStarted;
    [SerializeField] private MyUIClass myUIClass;
    [SerializeField] public static buttonevent rightClick = new buttonevent();
    [SerializeField] public static buttonevent leftClick = new buttonevent();



    // Start is called before the first frame update
    private void Start()
    {
        myUIClass = this.GetComponent<MyUIClass>();
        onGameStarted.Invoke();
    }

    // Update is called once per frame
   void Update()
    {
        myUIClass.GameWatch();
        MyUIClass.FullGametime();
    }
   
    /*Display Left arrow funtion*/
   
    public void RightArrowclicked(GameLogic right)
    {
        GameLogic.rightClick.Invoke(this);
    }
    public void LeftArrowclicked(GameLogic left)
    {
        GameLogic.leftClick.Invoke(this);
    }

}
