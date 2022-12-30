using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class ItemSpawnManager : MonoBehaviour
{
    public List<GameObject> PrefabsList = new List<GameObject>();

    [SerializeField] public int shardSpawnLimit = 6;
    [SerializeField] public int woodSpawnLimit = 200;


    [SerializeField] public int numShardsSpawned;
    [SerializeField] public int numwoodSpawned;

    public int shardTracker = 0;

    // This should only run on the Host's side and will update to the clients


    private void Start()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            enabled = false;
        }
        shardSpawnLimit = shardSpawnLimit * PhotonNetwork.PlayerList.Length;
    }
    void Update()
    {
        
        if (shardTracker == 2)
        {
            Vector2 spawnPos = new Vector2(Random.Range(-165, 175), Random.Range(-101, 132));
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "shard"), spawnPos, Quaternion.identity);
            shardTracker = 0;
        }
        if (numShardsSpawned < shardSpawnLimit)
        {
            Vector2 spawnPos = new Vector2(Random.Range(-165, 175), Random.Range(-101, 132));
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "shard"), spawnPos, Quaternion.identity);
            numShardsSpawned++;
        }
        if (numwoodSpawned < woodSpawnLimit)
        {
            Vector2 spawnPos = new Vector2(Random.Range(-165, 175), Random.Range(-101, 132));
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "log"), spawnPos, Quaternion.identity);
            numwoodSpawned++;
        }
    }

    
}
