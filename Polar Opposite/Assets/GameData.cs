using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    [SerializeField] private static bool GameOver = false;
    [SerializeField] private static string level = "Level 1";

    public static bool GetGameOver()
    {
        return GameOver;
    }

    public static void SetGameOver(bool state)
    {
        GameOver = state;
    }
}
