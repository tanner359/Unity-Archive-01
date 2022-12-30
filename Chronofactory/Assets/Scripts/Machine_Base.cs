using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine_Base : MonoBehaviour
{
    public List<GameObject> material_Queue;
    public GameObject[] output_Paths;

    public void OTE(Collider other, float destroy_Delay)
    {
        if (other.gameObject.layer == 8)
        {
            GameObject material = other.gameObject;
            material_Queue.Add(material);
            Destroy(other.gameObject, destroy_Delay);
        }
    }

    public void Cleanup_Queue()
    {
        for (int j = material_Queue.Count - 1; j < -1; j++)
        {
            if (material_Queue[j] == null)
                material_Queue.RemoveAt(j);
        }
    }
}
