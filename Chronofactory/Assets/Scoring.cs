using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoring : MonoBehaviour
{
    public int carsMade;
    public int timeBonus;  
    public float timer;
    float localTimer;
    public TextMeshProUGUI carsScoreText;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI timeBonusText;
    public GameObject[] stars;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        carsScoreText.text = carsMade + " Cars " + " x " + " 15 ";
        timeBonusText.text = "Time Bonus " + " +" + timeBonus;
        finalScoreText.text = "Final Score: " + timeBonus + (carsMade * 15);

    }
}
