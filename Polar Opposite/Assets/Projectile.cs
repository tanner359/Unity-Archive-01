using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetType().Equals(typeof(BoxCollider)) && other.gameObject.layer == 8)
        {
            other.GetComponent<PolarityBehaviour>().SetPolarity(gameObject.tag);
            Destroy(gameObject);
        }
        if (other.GetType().Equals(typeof(BoxCollider)) && other.gameObject.layer == 13)
        {
            other.GetComponent<PolarityBehaviour>().SetPolarity(gameObject.tag);
            Destroy(gameObject);
        }
        if(other.gameObject.layer == 12)
        {
            Destroy(gameObject);
        }
    }
    
}
