using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coroutine_Manager : MonoBehaviour
{
    public Image select_Border;
    public bool damp_Active = false;

    public Vector2 new_Pos;
    Vector2 velocity = Vector2.zero;
    public float time = 0.15f;

    IEnumerator Move()
    {
        Vector2 start_Pos = select_Border.transform.localPosition;

        while (Vector2.Distance(select_Border.transform.localPosition, new_Pos) > 1f)
        {
            select_Border.transform.localPosition = Vector2.SmoothDamp(select_Border.transform.localPosition, new_Pos, ref velocity, time);
            yield return new WaitForEndOfFrame();
        }

        Debug.Log("Damp Completed");
    }
}
