using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float health;
    public float damage;
    public float knockbackStrength;
    

    public void Start()
    {
        
    }
    public void FixedUpdate()
    {
        if(health <= 0)
        {
            Destroy(gameObject);                   
        }       
    }
    
}
