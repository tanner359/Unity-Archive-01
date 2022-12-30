using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Pun;


public class Light_Control : MonoBehaviour
{

    public UnityEngine.Experimental.Rendering.Universal.Light2D[] lights;
    public UnityEngine.Experimental.Rendering.Universal.Light2D fireLight;
    DayNightCycle DNCycle;

    GameManager GM;
    Hashtable playerProperties;
    PhotonView PV;

    public float[] maxIntesity;
    static float fireMaxIntensity;

    public int lightSwitchSmoothing = 10;


    // This should only run on the Host's side and will update to the clients


    private void Awake()
    {
        GM = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        PV = GetComponent<PhotonView>();
        playerProperties = PhotonNetwork.LocalPlayer.CustomProperties;
        //worldProperties = GM.worldProperties;
        DNCycle = GM.GetComponentInChildren<DayNightCycle>();
    }

    private void Start()
    {
        fireMaxIntensity = fireLight.intensity;
        fireLight.intensity = 0;


        for (int i = 0; i < lights.Length; i++)
        {
            maxIntesity[i] = lights[i].intensity;
        }
    }

    public void FixedUpdate()
    {
        if (GM.gameStart == true && PhotonNetwork.IsMasterClient)
        {
            if (DNCycle.GetTimeSeconds() <= lightSwitchSmoothing + 1 && DNCycle.GetTimeSeconds() != 0)
            {
                Debug.Log("LightSwitch");
                PV.RPC("SwitchWorldLight", RpcTarget.AllBuffered, DayNightCycle.isDay);
                PV.RPC("SwitchFireLight", RpcTarget.AllBuffered, DayNightCycle.isDay);
            }
        }     
    }

    [PunRPC]
    public void SwitchWorldLight(bool isDay)
    {
        if (!isDay)
        {
            for (int i = 0; i < lights.Length; i++)
            {
                if (lights[i].intensity < maxIntesity[i])
                {
                    lights[i].intensity += maxIntesity[i] * (lightSwitchSmoothing * 0.01f) * Time.deltaTime;
                }
            }
        }
        else if (isDay)
        {
            for (int i = 0; i < lights.Length; i++)
            {
                if (lights[i].intensity > 0)
                {
                    lights[i].intensity -= maxIntesity[i] * (lightSwitchSmoothing * 0.01f) * Time.deltaTime;
                }
                else
                {
                    lights[i].intensity = 0;
                }
            }
        }
    }
    [PunRPC]
    public void SwitchFireLight(bool isDay)
    {
        if (isDay)
        {
            if (fireLight.intensity < fireMaxIntensity)
            {
                fireLight.intensity += fireMaxIntensity * (lightSwitchSmoothing * 0.01f) * Time.deltaTime;
            }
        }
        else if (!isDay)
        {
            if (fireLight.intensity > 0)
            {
                fireLight.intensity -= fireMaxIntensity * (lightSwitchSmoothing * 0.01f) * Time.deltaTime;
            }
            else
            {
                fireLight.intensity = 0;
            }
        }
    }
}
