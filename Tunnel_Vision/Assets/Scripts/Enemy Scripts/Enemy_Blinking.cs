using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Blinking : MonoBehaviour
{
    Enemy_Base base_scr;

    private void Start()
    {
        base_scr = GetComponent<Enemy_Base>();
    }

    void Take_Turn()
    {
        base_scr.blinked = !base_scr.blinked;
    }
}
