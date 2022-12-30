using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody rb;
    public bool UIElement = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        if (!UIElement)
        {
            rb.AddTorque(new Vector3(Random.Range(0, 5), Random.Range(0, 5), Random.Range(0, 5)));
        }
        else if(UIElement)
        {
            rb.AddTorque(0, 2, 0);
        }
    }   
}
