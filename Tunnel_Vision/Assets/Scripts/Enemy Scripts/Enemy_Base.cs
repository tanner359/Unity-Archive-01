using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Base : MonoBehaviour
{
    public int tiles_Looked_At;
    public Color tile_Change;
    Vector3 target_pos;

    // Blinking Enemy
    public bool blinked;

    // Rotating Enemy
    public int dir;

    void Double_Check()
    {
        int layer_Mask = ~(1 << 8);

        if (!blinked)
        {
            for (int i = 1; i <= tiles_Looked_At; i++)
            {
                if (dir == 0)
                    target_pos = new Vector3(transform.position.x, 100, transform.position.z + i);
                else if (dir == 1)
                    target_pos = new Vector3(transform.position.x + i, 100, transform.position.z);
                else if (dir == 2)
                    target_pos = new Vector3(transform.position.x, 100, transform.position.z - i);
                else if (dir == 3)
                    target_pos = new Vector3(transform.position.x - i, 100, transform.position.z);

                RaycastHit hit;

                if (Physics.Raycast(target_pos, Vector3.down, out hit, Mathf.Infinity, layer_Mask))
                {
                    if (hit.collider.gameObject.tag == "Tile")
                    {
                        hit.collider.gameObject.GetComponent<Tile_Script>().tile_Color = tile_Change;
                        hit.collider.gameObject.SendMessage("Looked_At", SendMessageOptions.DontRequireReceiver);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }

    void Take_Turn()
    {
        int layer_Mask = ~(1 << 8);

        if (!blinked)
        {
            for (int i = 1; i <= tiles_Looked_At; i++)
            {
                if (dir == 0)
                    target_pos = new Vector3(transform.position.x, 100, transform.position.z + i);
                else if (dir == 1)
                    target_pos = new Vector3(transform.position.x + i, 100, transform.position.z);
                else if (dir == 2)
                    target_pos = new Vector3(transform.position.x, 100, transform.position.z - i);
                else if (dir == 3)
                    target_pos = new Vector3(transform.position.x - i, 100, transform.position.z);

                RaycastHit hit;

                if (Physics.Raycast(target_pos, Vector3.down, out hit, Mathf.Infinity, layer_Mask))
                {
                    if (hit.collider.gameObject.tag == "Tile")
                    {
                        hit.collider.gameObject.GetComponent<Tile_Script>().tile_Color = tile_Change;
                        hit.collider.gameObject.SendMessage("Looked_At", SendMessageOptions.DontRequireReceiver);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}
