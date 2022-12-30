using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    public float beltSpeed;
    public GameObject movePoint;
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == 8)
        {
            Vector3 Pos = other.transform.position;
            other.transform.position = Vector3.MoveTowards(Pos, movePoint.transform.position, beltSpeed * Time.deltaTime);
        }
        
    }     
}
