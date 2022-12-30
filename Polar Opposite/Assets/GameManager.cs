using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    void Update()
    {
        if (GameData.GetGameOver().Equals(true))
        {
            CanvasDisplay.instance.GameOverScreen(true);
            Debug.Log("The Game is Over");
        }
    }
}
