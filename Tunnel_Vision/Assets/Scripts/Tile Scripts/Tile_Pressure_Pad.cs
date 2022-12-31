using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_Pressure_Pad : MonoBehaviour
{
    public GameObject player;
    public GameObject level_Encap;
    Blind_Script scr_Blind;

    private void Start()
    {
        level_Encap = transform.root.gameObject;
        scr_Blind = player.GetComponent<Blind_Script>();
    }

    void Stepped_On()
    {
        level_Encap.BroadcastMessage("Pad_Pressed", SendMessageOptions.DontRequireReceiver);
    }

    void Stepped_Off()
    {
        if (!scr_Blind.is_Blind)
        level_Encap.BroadcastMessage("Pad_Released", SendMessageOptions.DontRequireReceiver);
    }
}
