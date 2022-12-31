using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fix_Error : MonoBehaviour
{
    void Start()
    {
        GetComponent<Activate_Dialogue>().enabled = false;
    }

    void Stepped_On()
    {
        GetComponent<Activate_Dialogue>().enabled = true;
    }
}
