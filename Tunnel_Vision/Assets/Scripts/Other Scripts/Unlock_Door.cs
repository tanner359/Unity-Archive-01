using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlock_Door : MonoBehaviour
{
    public Animator unlock_Anim;
    GameObject level_Encap;

    private void Start()
    {
        level_Encap = transform.root.gameObject;
    }

    void Pad_Pressed()
    {
        SpriteRenderer[] child_Renderers = gameObject.GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer renderer in child_Renderers)
        {
            renderer.enabled = false;
        }
        gameObject.GetComponent<BoxCollider>().enabled = false;
        level_Encap.BroadcastMessage("Double_Check", SendMessageOptions.DontRequireReceiver);
    }

    void Pad_Released()
    {
        SpriteRenderer[] child_Renderers = gameObject.GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer renderer in child_Renderers)
        {
            renderer.enabled = false;
        }

        gameObject.GetComponent<BoxCollider>().enabled = true;
        level_Encap.BroadcastMessage("Double_Check", SendMessageOptions.DontRequireReceiver);
    }
}
