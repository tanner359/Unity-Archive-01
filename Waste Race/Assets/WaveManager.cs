using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;

    public float amplitude = 1f;
    public float length = 2f;
    public float speed = 1f;
    public float offset = 0f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Destroy Wave manager");
            Destroy(this);
        }
    }

    private void Update()
    {
        offset += Time.time * speed;
    }




    public float GetWaveHeight(float x, float z)
    {
        return ((amplitude * Mathf.Sin((x / length) + (Time.time * speed))) + (amplitude * Mathf.Sin((z / length) + (Time.time * speed))));
    }


    


}
