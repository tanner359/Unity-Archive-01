using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killScript : MonoBehaviour
{
    public ContactFilter2D hitboxFilter;
    public List<Collider2D> enemyHitbox = new List<Collider2D>();
    public float slashSpeed;
    float timer;
    public float slashLifeSpan;
    StatsandCombat playerScript;
    public Vector2 mousePos;
    

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<StatsandCombat>();
        timer = slashLifeSpan;
        transform.rotation = GameObject.Find("Player").transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {  
        Physics2D.OverlapCollider(gameObject.GetComponent<Collider2D>(), hitboxFilter, enemyHitbox);
        if (enemyHitbox.Count > 0)
        {
            for (int i = 0; i < enemyHitbox.Count; i++)
            {
                GameObject.Find("Player").GetComponent<StatsandCombat>().enemyHitbox.Add(enemyHitbox[i]);
            }
        }       
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Destroy(gameObject);
        }  
        transform.position += transform.right * Time.deltaTime * slashSpeed;
    }     
}
