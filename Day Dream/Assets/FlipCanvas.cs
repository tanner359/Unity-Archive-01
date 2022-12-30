using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipCanvas : MonoBehaviour
{

    [SerializeField] Transform parentTarget;


    // Update is called once per frame
    void Update()
    {
        if(parentTarget.localScale.x < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if(parentTarget.localScale.x > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}
