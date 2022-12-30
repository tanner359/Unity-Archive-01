using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class DestroySelf : MonoBehaviour
{

    PhotonView PV;
    ItemSpawnManager ItemSpawnManager;
    SpawnManager CreatureSpawnManager;


    private void Awake()
    {
       
        PV = GetComponent<PhotonView>();
        ItemSpawnManager = GameObject.Find("Spawn_Manager").GetComponent<ItemSpawnManager>();
        CreatureSpawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

    }
    private void OnTriggerEnter2D(Collider2D other)
    {                
        if (other.CompareTag("Player"))
        {
            if (gameObject.tag == "animal")
            {
                CreatureSpawnManager.numAnimalsSpawned--;
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "food"), (Vector2)transform.position + new Vector2(2, 2), Quaternion.identity);
            }
            if (gameObject.tag == "Wood")
            {
                ItemSpawnManager.numwoodSpawned--;
            }
            if (gameObject.tag == "Shard")
            {
                ItemSpawnManager.shardTracker++;
            }
            PV.RPC("DestroyItem", RpcTarget.All);

                  
        }
    }
    [PunRPC]
    public void DestroyItem()
    {
        PhotonNetwork.Destroy(gameObject);
    }
}
