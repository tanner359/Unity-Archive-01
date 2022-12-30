using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Tilemaps;
using UnityEngine.Tilemaps;

[ExecuteInEditMode]
public class Layer_Controller : MonoBehaviour
{
    [Header("2D Sprite Renderer")]
    public SpriteRenderer render;

    [Header("2D Tilemap Renderer")]
    public TilemapRenderer tm_Renderer;
    public Transform player;

    [Header("Settings")]
    public int layerOffset = 0;

    private void Start()
    {
        if (gameObject.isStatic)
        {
            if (render)
            {
                render.sortingOrder = Mathf.RoundToInt(-transform.position.y + layerOffset);
            }
            else if (tm_Renderer)
            {
                tm_Renderer.sortingOrder = Mathf.RoundToInt(player.position.y + layerOffset);
            }
        }              
    }

    private void Update()
    {
        if (!gameObject.isStatic)
        {
            if (render)
            {
                render.sortingOrder = Mathf.RoundToInt(-transform.position.y + layerOffset);
            }
            else if (tm_Renderer)
            {
                tm_Renderer.sortingOrder = Mathf.RoundToInt(player.position.y + layerOffset);
            }
        }
    }

}
