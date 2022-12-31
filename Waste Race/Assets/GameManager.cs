using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static void RestartLevel()
    {
        SceneManager.LoadScene(1);
        MapGeneration.chunksLoaded = 1;
        PlayerData.ResetData();
        CanvasDisplay.instance.DisplayScore(PlayerData.score.ToString(), Color.white);
        CanvasDisplay.instance.DisplayHealthText(((PlayerData.health / (float)PlayerData.default_health) * 100).ToString() + "%", Color.white);
    }

    public static void ExitApplication()
    {
        Application.Quit();
    }

    public static void CloseWindow(GameObject window)
    {
        window.SetActive(false);
    }
    public static void OpenWindow(GameObject window)
    {
        window.SetActive(true);
    }
    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }
}
