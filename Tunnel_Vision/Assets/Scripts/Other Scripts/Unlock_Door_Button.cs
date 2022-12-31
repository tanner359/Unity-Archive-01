using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlock_Door_Button : MonoBehaviour
{
    GameObject level_Encap;

    private void Start()
    {
        level_Encap = transform.root.gameObject;
    }

    void Button_Pressed()
    {
        Destroy(gameObject);
    }
}
