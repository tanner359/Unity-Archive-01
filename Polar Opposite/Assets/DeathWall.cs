using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWall : MonoBehaviour
{
    public float speed;

    private void FixedUpdate()
    {
        if (GameData.GetGameOver().Equals(false))
        {
            transform.position = transform.position + -Vector3.forward * speed * Time.deltaTime;
        }
    }

    private void Update()
    {
        if (gameObject.transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }


}
