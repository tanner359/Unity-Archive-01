using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed;
    public GameObject[] players;   
    Vector3 focusPoint;
    Vector3 player1;
    Vector3 player2;
    Vector3 player3;
    Vector3 player4;
    
    public Vector3 offset;

    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if(players.Length == 1)
        {
            player1 = players[0].gameObject.transform.GetChild(2).transform.position;
            focusPoint = (player1);
        }
        if (players.Length == 2)
        {
            player1 = players[0].gameObject.transform.GetChild(2).transform.position;
            player2 = players[1].gameObject.transform.GetChild(2).transform.position;
            focusPoint = (player1 + player2) / 2;
        }
        if (players.Length == 3)
        {
            player1 = players[0].gameObject.transform.GetChild(2).transform.position;
            player2 = players[1].gameObject.transform.GetChild(2).transform.position;
            player3 = players[2].gameObject.transform.GetChild(2).transform.position;
            focusPoint = (player1 + player2 + player3) / 3;
        }
        if (players.Length == 4)
        {
            player1 = players[0].gameObject.transform.GetChild(2).transform.position;
            player2 = players[1].gameObject.transform.GetChild(2).transform.position;
            player3 = players[2].gameObject.transform.GetChild(2).transform.position;
            player4 = players[3].gameObject.transform.GetChild(2).transform.position;
            focusPoint = (player1 + player2 + player3 + player4)/4;
        }
     


        transform.position = Vector3.Lerp(transform.position, new Vector3(focusPoint.x, transform.position.y, focusPoint.z) + offset, moveSpeed);
    }
}
