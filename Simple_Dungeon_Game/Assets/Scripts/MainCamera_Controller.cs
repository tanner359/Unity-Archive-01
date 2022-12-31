using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera_Controller : MonoBehaviour
{
    public float camera_Speed;
    public float zOffset;
    public float yOffset;
    public float xOffset;
    Player_Controller player;
    Vector3 player_Position;

    float panDelay = 1f;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player_Controller>();
    }


    // Update is called once per frame
    void Update()
    {
        player_Position = GameObject.Find("Player").GetComponent<Transform>().position;
        transform.position = Vector3.Lerp(transform.position, player_Position + new Vector3(xOffset, yOffset, zOffset), camera_Speed);
        if (GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity.y <= -3.5f)
        {
            if(camera_Speed < 1f)
            {
                camera_Speed += 0.4f * Time.deltaTime;             
            }
            
        }       
        else
        {
            camera_Speed = 0.05f;
        }
        if (Input.GetAxisRaw("Vertical") < 0.2 && Input.GetAxisRaw("Vertical") > -0.2)
        {
            panDelay = 1f;
        }
        
        if (Input.GetAxisRaw("Vertical") > 0.5f && player.getGrounded())
        {           
            if(panDelay <= 0)
            {
                transform.position = Vector3.Lerp(transform.position, player_Position + new Vector3(xOffset, yOffset + 6, zOffset), camera_Speed - 0.04f);
                player.animator.SetBool("LookUp", true);
            }
            else
            {
                panDelay -= Time.deltaTime;
            }
        }
        else
        {          
            player.animator.SetBool("LookUp", false);
        }
        if (Input.GetAxisRaw("Vertical") < -0.5f && player.getGrounded())
        {            
            if (panDelay <= 0)
            {
                transform.position = Vector3.Lerp(transform.position, player_Position + new Vector3(xOffset, yOffset - 6, zOffset), camera_Speed - 0.04f);
                player.animator.SetBool("LookDown", true);
            }
            else
            {
                panDelay -= Time.deltaTime;
            }
        }
        else
        {
            player.animator.SetBool("LookDown", false);
        }
    }

    public float Timer(float time)
    {
        if(time > 0)
        {
            time -= Time.deltaTime;
        }
        return time;
    }
}
