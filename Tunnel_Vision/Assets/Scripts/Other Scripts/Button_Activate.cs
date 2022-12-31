using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Activate : MonoBehaviour
{
    private void Update()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0f);
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    void Button_Pressed()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 255f);
        gameObject.GetComponent<BoxCollider>().enabled = true;
        Destroy(this);
    }
}
