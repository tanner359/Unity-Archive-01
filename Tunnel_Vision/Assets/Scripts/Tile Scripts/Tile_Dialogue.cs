using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_Dialogue : MonoBehaviour
{
    public GameObject dialogue_Master;
    Dialogue_Script scr_Dialogue;
    public string[] dialogue;

    private void Start()
    {
        scr_Dialogue = dialogue_Master.GetComponent<Dialogue_Script>();
    }

    void Stepped_On()
    {
        scr_Dialogue.Activate(dialogue);
        Destroy(this);
    }
}
