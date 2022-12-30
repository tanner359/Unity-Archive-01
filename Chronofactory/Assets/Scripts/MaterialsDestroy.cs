using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialsDestroy : MonoBehaviour
{
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject, 0.1f);
        }
    }
}
