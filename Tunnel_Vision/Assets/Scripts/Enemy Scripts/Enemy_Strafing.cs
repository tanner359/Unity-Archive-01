using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Strafing : MonoBehaviour
{
    Enemy_Base base_scr;

    public GameObject player;
    GameObject standing_Tile;
    int move_Time;
    Vector3 target_pos;
    public GameObject sprite;
    Vector3 velocity;

    int move_spaces;
    public int max_Moves_One_Dir;
    int num_Moved;
    public bool flip_Direction;

    private void Start()
    {
        base_scr = GetComponent<Enemy_Base>();
        move_spaces = 1;
        num_Moved = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        velocity = Vector3.zero;
    }

    void Take_Turn()
    {
        RaycastHit hit;

        if (flip_Direction)
            target_pos = new Vector3(transform.position.x, transform.position.y, transform.position.z - move_spaces);
        else
            target_pos = new Vector3(transform.position.x - move_spaces, transform.position.y, transform.position.z);

        if (Physics.Raycast(target_pos, Vector3.down, out hit, Mathf.Infinity) && num_Moved < max_Moves_One_Dir)
        {
            if (hit.collider.gameObject.tag == "Tile")
            {
                if (player.GetComponent<Move_Script>().standingTile == hit.collider.gameObject)
                    player.GetComponent<Move_Script>().Die();

                move_Time = 10;
                standing_Tile = hit.collider.gameObject;
                Tile_Script scr = standing_Tile.GetComponent<Tile_Script>();
                transform.position = new Vector3(standing_Tile.transform.position.x, transform.position.y, standing_Tile.transform.position.z);
                num_Moved++;
            }
        }
        else
        {
            move_spaces = -move_spaces;
            num_Moved = 0;
        }
    }

    private void Update()
    {
        sprite.transform.position = Vector3.SmoothDamp(sprite.transform.position, transform.position, ref velocity, 0.2f);
    }
}
