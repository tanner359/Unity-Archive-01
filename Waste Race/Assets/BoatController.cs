using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    private Rigidbody rb;
    public float turnForce = 1f;
    public float speed = 1f;
    public Transform turnPoint;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        float xValue = Input.GetAxis("Horizontal");
        float zValue = Input.GetAxis("Vertical");

        if (xValue != 0 && zValue > 0)
        {
            rb.AddForceAtPosition(-transform.right *  turnForce * xValue, turnPoint.position, ForceMode.Acceleration);
            rb.AddTorque(transform.right, ForceMode.Acceleration);
        }
        if (zValue > 0)
        {
            rb.AddForceAtPosition(transform.forward * speed * zValue, turnPoint.position, ForceMode.Acceleration);
            rb.AddTorque(transform.forward, ForceMode.Acceleration);
        }
    }





}
