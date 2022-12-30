using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float speed;
    public Rigidbody rb;
    public GameObject endScreen;
    private static int highScore;

    // Update is called once per frame   
    void Update()
    {
        speed = 1 + 0.05f * Time.timeSinceLevelLoad;
        rb.velocity = new Vector3(0,0,5 + speed);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("obsticle") == true && GameObject.Find("WIN_SCREEN") == false)
        {
            endScreen.SetActive(true);           
        }

    }
}
