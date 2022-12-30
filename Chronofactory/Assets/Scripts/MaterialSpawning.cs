using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSpawning : MonoBehaviour
{
    //FMOD Event Manager
    [FMODUnity.EventRef] //Event Caller
    public string soundInput;
    FMOD.Studio.EventInstance Spawner_SFX;


    public GameObject spawnTube;
    public List<GameObject> materials = new List<GameObject>();
    public float spawnSpeed;
    float spawntimer;
    // Start is called before the first frame update
    void Start()
    {
        spawntimer = 5f;

        //FMOD Starter
        Spawner_SFX = FMODUnity.RuntimeManager.CreateInstance("event:/Machine_Spawner_events/Machine_Spawner_noise");
    }

    // Update is called once per frame
    void Update()
    {
        SpawnMaterial();
    }
    public void SpawnMaterial()
    {
        spawntimer -= Time.deltaTime;
        if(spawntimer < 0)
        {
            Instantiate(materials[Random.Range(0, materials.Count)], spawnTube.transform.position, Quaternion.identity);
            spawntimer = spawnSpeed;
            Spawn_Sound(); // Calls Event (Hand Sanitizer SFX is only a placeholder)
        }
    }

    //FMOD Sound Method

    void Spawn_Sound()
    {
        Debug.Log("Audio.Log: Material Spawned");
        Spawner_SFX.start(); //plays sound
    }

}
