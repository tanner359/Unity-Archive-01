using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkSpawner : MonoBehaviour
{
    bool spawnChunk = true;
    [Header("Spawn Interval in Seconds")]
    public float spawnInterval = 3f;
    public Transform DeathWallContainer;

    private void Update()
    {
        if(spawnChunk == true && !GameData.GetGameOver())
        {
            StartCoroutine(SpawnChunkAfterSeconds(spawnInterval));
            spawnChunk = false;
        }
    }

    IEnumerator SpawnChunkAfterSeconds(float time)
    {
        yield return new WaitForSeconds(time);
        MapGeneration.GenerateChunk(transform, DeathWallContainer);
        spawnChunk = true;
    }
}
