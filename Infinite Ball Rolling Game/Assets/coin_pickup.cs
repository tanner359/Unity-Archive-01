using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class coin_pickup : MonoBehaviour
{
    //  public AudioClip coinSound;
    //public Animation scoreAnimation;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Player") && GameObject.Find("YOU_DIED") == false)
        {
            other.GetComponent<item_pickup>().score++;
            Destroy(gameObject);          
        }
    }    
}
