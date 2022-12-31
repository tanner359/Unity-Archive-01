using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] public static int default_health;
    [SerializeField] public static int default_score;
    [SerializeField] public static int default_age;
    [SerializeField] public static float default_trash;

    [SerializeField] public static int health;
    [SerializeField] public static int score;
    [SerializeField] public static int age;
    [SerializeField] public static float trash;

    public static void SetupPlayer(int _health, int _score, int _age, float _trash)
    {
        health = _health;
        default_health = health;
        score = _score;
        default_score = score;
        age = _age;
        default_age = age;
        trash = _trash;
        default_trash = trash;
    }
    
    public static void ResetData()
    {
        health = default_health;
        score = default_score;
        age = default_age;
        trash = default_trash;
    }
}
