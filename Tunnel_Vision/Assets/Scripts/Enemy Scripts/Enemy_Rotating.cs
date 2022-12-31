using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Rotating : MonoBehaviour
{
    Enemy_Base base_scr;

    private void Start()
    {
        base_scr = GetComponent<Enemy_Base>();
    }

    void Take_Turn()
    {
        if (base_scr.dir == 3)
            base_scr.dir = 0;
        else
            base_scr.dir++;
    }
}
