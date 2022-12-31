using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_Script : MonoBehaviour
{
    public bool looked_At;
    public Color tile_Color;
    Color base_Color;

    // Start is called before the first frame update
    void Start()
    {
        base_Color = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void Looked_At()
    {
        GetComponent<SpriteRenderer>().color = tile_Color;
        looked_At = true;
    }

    // This method will be called off of a broadcast in the player Movement script
    void Stepped_On()
    {
        //Do some shit
    }

    void Turn_Reset()
    {
        gameObject.GetComponent<SpriteRenderer>().color = base_Color;
        looked_At = false;
    }

    void Take_Turn()
    {
        // Check if they're being looked at and change looked_At to false if not
        // Do other stuff
    }
}
