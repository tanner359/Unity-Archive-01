using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activate_Dialogue : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Tile_Dialogue>().enabled = false;
    }

    void Pad_Pressed()
    {
        GetComponent<Tile_Dialogue>().enabled = true;
    }
}
