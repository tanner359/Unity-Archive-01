using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkLoader : MonoBehaviour
{
    public int maxChunks = 10;

    private void OnTriggerExit(Collider other)
    {
        MapGeneration.DestroyChunk(gameObject);
        if (MapGeneration.GetChunksLoaded() < 10)
        {
            MapGeneration.GenerateChunk(transform, transform.localScale.z * MapGeneration.GetChunksLoaded() + transform.localScale.z);
        }
    }

    private void Start()
    {
        if(MapGeneration.GetChunksLoaded() < 10)
        {
            MapGeneration.GenerateChunk(transform, transform.localScale.z);
        }
    }
}
