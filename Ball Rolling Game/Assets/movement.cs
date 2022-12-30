using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public Rigidbody rb;   
    public AudioSource audiosource;   
    public bool playSound;
    public GameObject panel;
    // Update is called once per frame
    public void Start()
    {
     
    }
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            panel.SetActive(true);
        }
        if (Input.GetKey(KeyCode.W) && panel.activeSelf == false)
        {            
            rb.AddForce(0, 0, 1000);
            rb.AddTorque(500, 0, 0);
        }
        if (Input.GetKey(KeyCode.A) && panel.activeSelf == false)
        {
            rb.AddForce(-1000, 0, 0);
            rb.AddTorque(0, 0, 500);
        }
        if (Input.GetKey(KeyCode.D) && panel.activeSelf == false)
        {
            rb.AddForce(1000, 0, 0);
            rb.AddTorque(0, 0, -500);
        }
        if (Input.GetKey(KeyCode.S) && panel.activeSelf == false)
        {
            rb.AddForce(0, 0, -1000);
            rb.AddTorque(-500, 0, 0);
        }
        while (Physics.Raycast(transform.position, Vector3.down, 1) == false)
        {
            Debug.Log("not grounded");
            Debug.DrawRay(transform.position, Vector3.down, Color.magenta);
            audiosource.Stop();
            break;
        }
        if(rb.velocity.magnitude > 1 && audiosource.isPlaying == false && Physics.Raycast(transform.position, Vector3.down, 1) == true)
        {
            audiosource.Play();
        }
        



            audiosource.volume = rb.velocity.magnitude /3f;          
    }
    

}
