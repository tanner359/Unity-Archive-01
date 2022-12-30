using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TIMER_SCRIPT : MonoBehaviour
{
    public TextMeshProUGUI timeDisplay;
    public GameObject scoreScreen;
    public float timeLimit;
    public float seconds;
    public float minutes;
    public float hours;
    public float t;

    private void Start()
    {
        timeLimit = timeLimit * 60;
    }



    void Update()
    {
        TimerCount();
    }
    void TimerCount()
    {
        t = timeLimit - Time.timeSinceLevelLoad;
        //hours = ((int)(t / 60) / 60);
        if(t > 0)
        {
            minutes = ((int)(t / 60 % 60));
            seconds = t % 60;
            timeDisplay.text = "Time Left     " /*+ hours.ToString() + ':'*/ + minutes.ToString() + ':' + seconds.ToString("00");
        }  
        if(t <= 0)
        {
            t = 0;
            Time.timeScale = 0;
            scoreScreen.SetActive(true);
        }
    }
}
