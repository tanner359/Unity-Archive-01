using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int maxHealth = 5;
    public float scoreMultiplier = 1;
    bool birthday = true;
    bool gameover = false;

    void Start()
    {
        PlayerData.SetupPlayer(maxHealth, 0, 0, 0);
    } 

    private void Update()
    {
        Debug.Log(PlayerData.health);
        if(PlayerData.health == 0)
        {
            gameover = true;
            CanvasDisplay.instance.DisplayEndGameText("You Died from eating too much plastic!!", 5f, Color.red);
            PlayerData.health = -1;
            GetComponent<PlayerController>().speed = 0;            
        }

        if(PlayerData.age < 100 && birthday && !gameover)
        {
            StartCoroutine(AgeTimer(15f));
            birthday = false;
        }

        if(PlayerData.age == 100 && !gameover)
        {
            gameover = true;
            GetComponent<PlayerController>().speed = 0;
            CanvasDisplay.instance.DisplayEndGameText("Your Sea Turtle died a happy life at 100 years old!", 4f, Color.yellow);
        }
    }

    IEnumerator AgeTimer(float timeBeforeAge)
    {
        yield return new WaitForSeconds(timeBeforeAge);
        PlayerData.age += 1;
        birthday = true;
        GetComponent<PlayerController>().speed += 0.1f;

        if(PlayerData.age % 5 == 0)
        {
            StartCoroutine(ShowInfoBoard());
        }
    }
    IEnumerator ShowInfoBoard()
    {
        float speed = GetComponent<PlayerController>().speed;
        GetComponent<PlayerController>().speed = 0.1f;
        CanvasDisplay.instance.DisplayInfoBoard(true);
        yield return new WaitForSeconds(10f);
        GetComponent<PlayerController>().speed += speed;
        CanvasDisplay.instance.DisplayInfoBoard(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trash"))
        {
            if(PlayerData.score > 0)
            {
                PlayerData.score -= 100;
                CanvasDisplay.instance.DisplayScore(PlayerData.score.ToString(), Color.red);
            }
            if (scoreMultiplier <= 1.1f)
            {
                scoreMultiplier = 1f;
            }
            PlayerData.health--;
            PlayerData.trash += (Random.Range(3, 8) / 16f);
            CanvasDisplay.instance.DisplayHealthText(((PlayerData.health / (float)maxHealth) * 100).ToString() + "%", Color.red);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Food"))
        {
            if (scoreMultiplier < 3f)
            {
                scoreMultiplier += 0.1f;
            }
            PlayerData.score += (int)(100 * scoreMultiplier);
            CanvasDisplay.instance.DisplayScore(PlayerData.score.ToString(), Color.green);
            Destroy(other.gameObject);
        }
    }  
}
