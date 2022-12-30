using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class DayNightCycle : MonoBehaviour
{

    public string GetTime()
    {
        return string.Format("{0}:{1}", ((int)(t / 60)).ToString(), ((int)(t % 60)).ToString("00"));        
    }   
    public float GetTimeSeconds()
    {
        return t;
    }   

    [Header("Day and Night Durations")]
    [SerializeField] int dayDurationMin;
    [SerializeField] int dayDurationSec;
    [SerializeField] int nightDurationMin;
    [SerializeField] int nightDurationSec;
    public AudioSource day_Source;
    public AudioSource night_Source;
    public float transition_Duration;

    Hashtable playerProperties;
    GameManager GM;
    PhotonView PV;

    public float preGameStartDuration = 3f;
    private float pregameTime;
    public bool DN_CycleStart;
    public static bool isDay = true;

    public bool cycle = false;
    float t = 0;
    float elapsed = 0f;

    // This should only run on the Host's side and will update to the clients

    private void Awake()
    {
        GM = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        PV = GetComponent<PhotonView>();

        

    }

    private void Start()
    {
       
        pregameTime = preGameStartDuration;
    }

    private void Update()
    {        
        if (GM.gameStart && DN_CycleStart == false)
        {
            PreGameStart();
        }

        if (DN_CycleStart && PhotonNetwork.IsMasterClient)
        {           
            DNCycle();
        }
    }

    public void PreGameStart()
    {
        Debug.Log("Pregame Start");
        if (pregameTime > 0)
        {
            pregameTime -= Time.deltaTime;
        }

        if(pregameTime/preGameStartDuration > 0.75f)
        {
            GM.playerUI.ScreenOverlayText("Ready", Color.white);
        }
        else if(pregameTime / preGameStartDuration > 0.50f)
        {
            GM.playerUI.ScreenOverlayText("Set", Color.white);
        }
        else if(pregameTime / preGameStartDuration > 0.25f)
        {
            GM.playerUI.ScreenOverlayText("Go!", Color.white);
        }
        else
        {
            DN_CycleStart = true;
            GM.playerUI.ScreenOverlayText("", Color.white);
        }
    }

    [PunRPC]
    public void UpdateTimeUI(string time, string status)
    {
        GM.playerUI.DNCycleUpdate(time, status);       
    }

    [PunRPC]
    public void SetStatus(bool _isDay)
    {
        isDay = _isDay;
    }

    public static string GetStatus()
    {
        if (isDay == true)
        {
            return "Day";
        }
        else
        {
            return "Night";
        }
        
    }


    
    public void DNCycle()
    {        
        if (isDay)
        {
            if(t <= 0 && !cycle)
            {
                t = (dayDurationMin * 60) + dayDurationSec;
                cycle = !cycle;
                string time = string.Format("{0}:{1}", ((int)(t / 60)).ToString(), ((int)(t % 60)).ToString("00"));
                string status = "Day";
                PV.RPC("UpdateTimeUI", RpcTarget.AllBuffered, time, status);             
            }
            else if(t > 0)
            {              
                elapsed += Time.deltaTime;
                t -= Time.deltaTime;
                if (elapsed >= 1f)
                {
                    elapsed = elapsed % 1f;
                    string time = string.Format("{0}:{1}", ((int)(t / 60)).ToString(), ((int)(t % 60)).ToString("00"));
                    string status = "Day";
                    PV.RPC("UpdateTimeUI", RpcTarget.AllBuffered, time, status);
                }
            }
            else
            {
                PV.RPC("SetStatus", RpcTarget.AllBuffered, !isDay);              
                cycle = !cycle;
                StartCoroutine("Crossfade");
            }
        }
        else if (!isDay)
        {
            if (t <= 0 && !cycle)
            {
                t = (nightDurationMin * 60) + nightDurationSec;
                cycle = !cycle;
                string time = string.Format("{0}:{1}", ((int)(t / 60)).ToString(), ((int)(t % 60)).ToString("00"));
                string status = "Night";
                PV.RPC("UpdateTimeUI", RpcTarget.AllBuffered, time, status);
            }
            else if(t > 0)
            {
                elapsed += Time.deltaTime;
                t -= Time.deltaTime;                         
                if (elapsed >= 1f)
                {
                    elapsed = elapsed % 1f;
                    string time = string.Format("{0}:{1}", ((int)(t / 60)).ToString(), ((int)(t % 60)).ToString("00"));
                    string status = "Night";
                    PV.RPC("UpdateTimeUI", RpcTarget.AllBuffered, time, status);
                }
            }
            else
            {
                PV.RPC("SetStatus", RpcTarget.AllBuffered, !isDay);
                cycle = !cycle;
                StartCoroutine("Crossfade");
            }
        }
    }

    IEnumerator Crossfade()
    {
        for (int i = 0; i < transition_Duration; i++)
        {
            if (isDay && night_Source.volume > 0f && day_Source.volume < 0.1f)
            {
                day_Source.volume += 1 / transition_Duration;
                night_Source.volume -= 1 / transition_Duration;
            }
            else if (!isDay && day_Source.volume > 0f && night_Source.volume < 0.1f)
            {
                day_Source.volume -= 1 / transition_Duration;
                night_Source.volume += 1 / transition_Duration;
            }

            yield return new WaitForSecondsRealtime(0.1f);
        }
    }
}
