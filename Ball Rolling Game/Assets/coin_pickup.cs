using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class coin_pickup : MonoBehaviour
{
    public AudioClip coinSound;
    public Animation scoreAnimation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == ("Player"))
        {
            other.GetComponent<item_pickup>().score++;
            scoreAnimation.Play("scoreRumble");
            AudioSource.PlayClipAtPoint(coinSound, other.transform.position);
            Destroy(gameObject);          
        }
    }
}
