using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyMovement : MonoBehaviour
{
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x - GameObject.FindGameObjectWithTag("Player").transform.position.x < 10f)
        {
            transform.position = Vector2.Lerp(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position, moveSpeed);
        }
    }
}
