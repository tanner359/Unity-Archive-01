using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMovement : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.Rotate(0, 0, 1);
    }
}
