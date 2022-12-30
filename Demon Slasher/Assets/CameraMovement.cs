using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Vector3 offset;
    public Vector3 offset2;
    public Vector3 offset3;
    public float camSpeed;
    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().flipX == false)
        {
            transform.position = Vector3.Lerp(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position + offset, camSpeed);
        }
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().flipX == true)
        {
            transform.position = Vector3.Lerp(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position + offset2, camSpeed);
        }
        if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            transform.position = Vector3.Lerp(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position + offset3, camSpeed);

        }

    }
}
