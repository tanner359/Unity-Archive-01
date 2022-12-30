using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] static int score = 0;  

    public static void GivePlayerPoints(int amount)
    {
        score = score + amount;
    }
    public static int GetScore()
    {
        return score;
    }
}
