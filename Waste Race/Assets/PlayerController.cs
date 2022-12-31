using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float turnForce = 1f;
    public float speed = 1f;
    public Transform turnPoint;
    public float rotationSpeed = 0.1f;
    public float MinX;
    public float MinY;
    public float MaxX;
    public float MaxY;
    Vector2 targetPosition;

    private void Start()
    {
        targetPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if ((Vector2)transform.position != targetPosition)
        {
            Debug.Log(targetPosition);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPosition.x, targetPosition.y, transform.position.z), 0.1f);
        }
    }

    private void Update()
    {
        

        float xValue = Input.GetAxis("Horizontal");
        float yValue = Input.GetAxis("Vertical");

        rb.velocity = transform.forward * speed;

        if (Input.GetKeyDown(KeyCode.A))
        {
            if(targetPosition.x != MinX)
            {
                targetPosition = targetPosition + new Vector2(-1.5f, 0);
            }         
            //transform.rotation = Quaternion.LerpUnclamped(transform.rotation, Quaternion.Euler(0, 0, -15f), rotationSpeed);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (targetPosition.y != MinY)
            {
                targetPosition = targetPosition + new Vector2(0, -1.5f);
            }
            //transform.rotation = Quaternion.LerpUnclamped(transform.rotation, Quaternion.Euler(0, 0, -15f), rotationSpeed);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (targetPosition.x != MaxX)
            {
                targetPosition = targetPosition + new Vector2(1.5f, 0);
            }
            //transform.rotation = Quaternion.LerpUnclamped(transform.rotation, Quaternion.Euler(0, 0, 15f), rotationSpeed);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (targetPosition.y != MaxY)
            {
                targetPosition = targetPosition + new Vector2(0, 1.5f);
            }
            //transform.rotation = Quaternion.LerpUnclamped(transform.rotation, Quaternion.Euler(0, 15, 0), rotationSpeed);
        }
        //else
        //{
        //    transform.rotation = Quaternion.LerpUnclamped(transform.rotation, Quaternion.Euler(0, 0, 0), rotationSpeed);
        //}
        //if (yValue != 0)
        //{
        //    rb.AddForceAtPosition(transform.up * turnForce * yValue, turnPoint.position, ForceMode.Acceleration);
        //}
    }

    
}
