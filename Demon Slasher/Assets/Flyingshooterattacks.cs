using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyingshooterattacks : MonoBehaviour
{
    public GameObject projectile;
    public float timer;
    public float speed;
    GameObject projectileClone;
    List<GameObject> projectiles = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x - GameObject.Find("Player").transform.position.x < 10f )
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                projectileClone = Instantiate(projectile, transform.position, Quaternion.identity);
                projectiles.Add(projectileClone);
                timer = Random.Range(4, 8);
            }
            
        }
    }
}
