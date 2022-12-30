using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using System.IO;
using Photon.Pun;
using Photon.Realtime;

public class SpawnManager : MonoBehaviour
{
    GameManager GM;
    Hashtable playerProperties;
    public Campfire fire;
    GameObject campfire;

    [Header("SpookEyes_Sprites")]

    [SerializeField] public List<Sprite> SpookEyesList = new List<Sprite>();

    public float spawnIntervalMIN = 10;
    public float spawnIntervalMAX = 15;

    public int numAnimalsSpawned;
    public int animalSpawnLimit = 75;

    // This should only run on the Host's side and will update to the clients


    // Start is called before the first frame update
    void Start()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            enabled = false;
        }
        GM = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        playerProperties = PhotonNetwork.LocalPlayer.CustomProperties;
        //worldProperties = GM.worldProperties;
        campfire = fire.gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (DayNightCycle.GetStatus() == "Night")
        {
            SpawnSpook();
        }
        else if (DayNightCycle.GetStatus() == "Day")
        {
            if (numAnimalsSpawned < animalSpawnLimit)
            {
                Vector2 spawnPos = new Vector2(Random.Range(-165, 175), Random.Range(-101, 132));
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "angy_bunny"), spawnPos, Quaternion.identity);
                numAnimalsSpawned++;
            }
        }
    }

    float elapsed = 0;

    public void SpawnSpook()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= Random.Range(spawnIntervalMIN, spawnIntervalMAX))
        {           
            Vector2 spawnPos =  new Vector2(campfire.transform.position.x, campfire.transform.position.y) + new Vector2(Random.Range(-20, 20), Random.Range(-20, 20));
            string path = Path.Combine("PhotonPrefabs", "Spook_Eyes");
            PhotonNetwork.Instantiate(path, spawnPos, Quaternion.identity).GetComponent<SpriteRenderer>();
            elapsed = 0;
        }       
    }

    public Sprite GenerateRandomSpookSprite()
    {

        return SpookEyesList[Random.Range(0, SpookEyesList.Count-1)];
    }

    
}
