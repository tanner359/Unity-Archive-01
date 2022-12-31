using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_Button : MonoBehaviour
{
    public GameObject button_Wall;
    public GameObject level_Encap;

    private void Start()
    {
        level_Encap = transform.root.gameObject;
    }

    void Stepped_On()
    {
        level_Encap.BroadcastMessage("Button_Pressed", SendMessageOptions.DontRequireReceiver);
    }
}
