using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine_Box : MonoBehaviour
{
    public GameObject machine;

    public void OnTriggerExit(Collider other)
    {
        GetComponent<Collider>().enabled = true;
    }
}
