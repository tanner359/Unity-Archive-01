using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControls : MonoBehaviour
{
    public GameObject panel;

    public void PlayGame()
    {
        panel.SetActive(false);

    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
