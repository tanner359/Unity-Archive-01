using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue_Script : MonoBehaviour
{
    public GameObject player;
    public GameObject txt_Box;
    public Text txt;
    string[] dialogue;
    int text_Index;
    float start_Timer;
    float start_Time;
    bool can_Progress;

    bool dialogue_Visible;

    private void Start()
    {
        text_Index = 0;
        txt_Box.SetActive(false);
        txt.fontSize = 50;
        start_Time = 1.0f;
    }

    public void Activate(string[] dialogue)
    {
        txt_Box.SetActive(true);
        this.dialogue = dialogue;
        txt.text = dialogue[text_Index];
        dialogue_Visible = true;
        player.GetComponent<Move_Script>().Toggle_Move();
        player.GetComponent<Blind_Script>().Toggle_Blind_Ability();
    }

    void Progress_Dialogue()
    {
        if (!can_Progress && start_Timer < start_Time)
        {
            start_Timer += Time.deltaTime;
        }
        else
        {
            can_Progress = true;
        }

        if (Input.anyKeyDown && can_Progress)
        {
            if (text_Index < dialogue.Length - 1)
            {
                text_Index++;
                txt.text = dialogue[text_Index];
            }
            else
            {
                txt_Box.SetActive(false);
                text_Index = 0;
                dialogue_Visible = false;
                txt.text = "";
                player.GetComponent<Move_Script>().Toggle_Move();
                player.GetComponent<Blind_Script>().Toggle_Blind_Ability();
                can_Progress = false;
                start_Timer = 0f;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogue_Visible)
        {
            Progress_Dialogue();
        }
    }
}
