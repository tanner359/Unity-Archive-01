using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorter : MonoBehaviour
{
    private Machine_Base m_Base;

    private void Start()
    {
        m_Base = GetComponent<Machine_Base>();
    }

    private void Update()
    {
        if (m_Base.material_Queue.Count > 0)
            Sort();
    }

    private void OnTriggerEnter(Collider other)
    {
        m_Base.OTE(other, 0.2f);
    }

    void Sort()
    {
        for(int k = 0; k < m_Base.output_Paths.Length; k++)
        {
            for (int i = 0; i < m_Base.material_Queue.Count; i++)
            {
                if (m_Base.material_Queue[i].tag == m_Base.output_Paths[k].tag)
                {
                    Instantiate(m_Base.material_Queue[i], m_Base.output_Paths[k].transform.position, Quaternion.identity);                    
                    m_Base.material_Queue.Remove(m_Base.material_Queue[i]);
                }
            }
        }
    }
}
