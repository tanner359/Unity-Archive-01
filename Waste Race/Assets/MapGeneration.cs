using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class MapGeneration : MonoBehaviour
{
    [SerializeField] public static int chunksLoaded = 1;
    [SerializeField] private static readonly GameObject[] chunkPrefabs = Resources.LoadAll<GameObject>("ChunkPrefabs");


    public static int GetChunksLoaded()
    {
        Debug.Log("ChunksLoaded: " + chunksLoaded);
        return chunksLoaded;        
    }

    public static void GenerateChunk(Transform pos, float size)
    {
        Instantiate(chunkPrefabs[Random.Range(0, chunkPrefabs.Length)], pos.transform.position + new Vector3(0,0,size), pos.rotation);
        chunksLoaded++;
    }
    public static void DestroyChunk(GameObject chunk)
    {
        Destroy(chunk);
        chunksLoaded--;
    }
}
