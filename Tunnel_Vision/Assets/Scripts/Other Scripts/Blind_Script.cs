using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blind_Script : MonoBehaviour
{
    public GameObject player;
    GameObject curr_Tile;
    public GameObject level_Encap;
    public Text blind_Left_UI;

    public int max_Moves;
    public int moves_Left;
    public bool is_Blind;
    public bool can_Blind;

    public KeyCode blind_key;

    private void Start()
    {
        moves_Left = max_Moves;
        can_Blind = true;
    }

    // Update is called once per frame
    void Update()
    {
        blind_Left_UI.text = moves_Left.ToString();

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity))
        {
            curr_Tile = hit.collider.gameObject;
        }

        if (Input.GetKeyDown(blind_key) && moves_Left > 0 && can_Blind)
        {
            is_Blind = !is_Blind;

            if (!is_Blind)
                level_Encap.BroadcastMessage("Pad_Released", SendMessageOptions.DontRequireReceiver);
        }

        if (is_Blind && moves_Left <= 0)
        {
            is_Blind = false;
            level_Encap.BroadcastMessage("Pad_Released", SendMessageOptions.DontRequireReceiver);
        }

        if (is_Blind)
        {
            Become_Blind();
        }
        else
        {
            Renderer[] level_Renderers = level_Encap.GetComponentsInChildren<Renderer>();

            foreach (Renderer renderer in level_Renderers)
            {
                renderer.enabled = true;
            }
        }
    }

    public void Toggle_Blind_Ability()
    {
        can_Blind = !can_Blind;
    }

    void Become_Blind()
    {
        Renderer[] level_Renderers = level_Encap.GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in level_Renderers)
        {
            if (renderer.gameObject != curr_Tile)
                renderer.enabled = false;

            if (renderer.gameObject == curr_Tile)
                renderer.enabled = true;
        }
    }
}
