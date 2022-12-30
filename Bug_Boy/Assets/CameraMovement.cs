using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float camera_Speed;
    public float zOffset;
    public float yOffset;
    public float xOffset;
    public GameObject player;


    private void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position + new Vector3(xOffset, yOffset, zOffset), camera_Speed);
        if (player.GetComponent<Rigidbody>().velocity.y <= -3.5f)
        {
            if (camera_Speed < 1f)
            {
                camera_Speed += 0.4f * Time.deltaTime;
            }
        }
        else
        {
            camera_Speed = 0.08f;
        }
        if (player.GetComponent<Controller>().flying == false)
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position + new Vector3(xOffset, yOffset + 3, zOffset), camera_Speed - 0.02f);
        }        
    }
   
}
