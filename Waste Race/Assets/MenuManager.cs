using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadLevel(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }
    public void OpenMenu(GameObject Menu)
    {
        Menu.SetActive(true);
    }
    public void CloseMenu(GameObject Menu)
    {
        Menu.SetActive(false);
    }
}
