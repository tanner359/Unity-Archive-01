using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class item_pickup : MonoBehaviour
{
    public int score;
    public Text scoreText;

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score " + score;
    }
}
