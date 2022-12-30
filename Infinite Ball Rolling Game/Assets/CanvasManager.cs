using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{

    public GameObject winScreen;
    bool resumed = false;   
      
    public void Restart()
    {       
        SceneManager.LoadScene("INFINITE");
        Time.timeScale = 1f;
    }

    public void continueRun()
    {
        Time.timeScale = 1f;
        winScreen.SetActive(false);
        resumed = true;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene("INFINITE");
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
        if(GameObject.Find("player").GetComponent<item_pickup>().score == 100 && resumed == false)
        {
            Time.timeScale = 0f;
            winScreen.SetActive(true);
        }        
    }
}
