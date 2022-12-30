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

    public static void GenerateChunk(Transform transform, float size)
    {
        Instantiate(chunkPrefabs[Random.Range(0, chunkPrefabs.Length)], transform.position + new Vector3(0,0,size), transform.rotation);
        chunksLoaded++;
    }
    public static void GenerateChunk(Transform transform, Transform container)
    {
        GameObject chunk = chunkPrefabs[Random.Range(0, chunkPrefabs.Length)];
        Instantiate(chunk, transform.position, chunk.transform.rotation, container);
        chunksLoaded++;
    }
    public static void DestroyChunk(GameObject chunk)
    {
        Destroy(chunk);
        chunksLoaded--;
    }
}
