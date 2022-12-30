using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public GameObject bloodSplatter;
    public List<GameObject> bloodParticleList = new List<GameObject>();
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (bloodParticleList.Count > 0)
        {
            if (timer <= 0)
            {
                Destroy(bloodParticleList[0]);
                bloodParticleList.Remove(bloodParticleList[0]);
                timer = 3f;
            }

        }
        if(bloodParticleList.Count > 15)
        {
            Destroy(bloodParticleList[0]);
            bloodParticleList.Remove(bloodParticleList[0]);
            Destroy(bloodParticleList[1]);
            bloodParticleList.Remove(bloodParticleList[1]);
            Destroy(bloodParticleList[2]);
            bloodParticleList.Remove(bloodParticleList[2]);
            Destroy(bloodParticleList[3]);
            bloodParticleList.Remove(bloodParticleList[3]);
            Destroy(bloodParticleList[4]);
            bloodParticleList.Remove(bloodParticleList[4]);
        }
    }
    public void BloodSplatter(GameObject enemyObject)
    {
        GameObject blood = Instantiate(bloodSplatter, enemyObject.transform.position, Quaternion.identity);
        bloodParticleList.Add(blood);
    }
}
