using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasDisplay : MonoBehaviour
{
    public static CanvasDisplay instance;

    public GameObject GameOverMenu;
    public TMP_Text score;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        //DontDestroyOnLoad(gameObject);
        instance = this;
    }


    public void GameOverScreen(bool state)
    {
        GameOverMenu.SetActive(state);
        score.text = PlayerData.GetScore().ToString();
    }


    public void OpenMenu(GameObject menu)
    {
        menu.SetActive(true);
    }
    public void CloseMenu(GameObject menu)
    {
        menu.SetActive(false);
    }
}
