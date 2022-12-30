using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapGenerator : MonoBehaviour
{
    public GameObject[] mapPrefabs;
    public float spawnZ = 0;
    public float tileLength = 4.0f;
    public float amountOfTiles = 10f;
    public float waitTime = 15f;
    GameObject tunnel;
    GameObject between;   
    private List<GameObject> activeTiles;


    // Start is called before the first frame update
    void Start()
    {
        activeTiles = new List<GameObject>();
    }
    // Update is called once per frame
    void Update()
    {
        
        if (GameObject.FindGameObjectWithTag("Player").transform.position.z > (spawnZ - amountOfTiles * tileLength))
        {
            int i;
            i = Random.Range(0, 17);              
            
            tunnel = Instantiate(mapPrefabs[i]);          
            activeTiles.Add(tunnel);            
            tunnel.transform.SetParent(transform);
            tunnel.transform.position = Vector3.forward * spawnZ;
            spawnZ += tileLength;

            between = Instantiate(mapPrefabs[17]);
            activeTiles.Add(between);
            between.transform.SetParent(transform);
            between.transform.position = Vector3.forward * spawnZ;
            spawnZ += tileLength;
        }
        if(GameObject.FindGameObjectWithTag("Player").transform.position.z > activeTiles[0].transform.position.z + waitTime)
        {
            
            Destroy(activeTiles[0]);
            activeTiles.RemoveAt(0);
        }
    }
}
