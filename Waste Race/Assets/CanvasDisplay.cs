using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasDisplay : MonoBehaviour
{
    public static CanvasDisplay instance;

    [SerializeField] Animator InfoBoardAnimator;
    [SerializeField] TMP_Text ScreenOverlay;
    [SerializeField] TMP_Text Health;
    [SerializeField] TMP_Text Score;
    [SerializeField] TMP_Text FinalScore;
    [SerializeField] TMP_Text Age;
    [SerializeField] TMP_Text Trash;
    [SerializeField] GameObject GameEndPanel;
    

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
    
    public void DisplayInfoBoard(bool active)
    {
        InfoBoardAnimator.SetBool("Slide", active);
    }

    public void DisplayEndGameText(string message, float t)
    {
        ScreenOverlay.text = message;
        StartCoroutine(WaitForSeconds(t));
    }
    public void DisplayEndGameText(string message, float t, Color color)
    {
        ScreenOverlay.color = color;
        ScreenOverlay.text = message;
        StartCoroutine(WaitForSeconds(t));
    }
    public void ToggleGameEndPanel(bool state)
    {
        GameEndPanel.SetActive(state);
        Score.text = PlayerData.score.ToString();
        Age.text = PlayerData.age.ToString() + "yrs";
        Trash.text = PlayerData.trash.ToString("0.00") + "lbs";
    }
    public void DisplayHealthText(string health, Color colorFlash)
    {
        Health.text = health;
        StartCoroutine(ColorFlash(Health.color, Health, colorFlash));
    }
    public void DisplayScore(string score, Color colorFlash)
    {
        FinalScore.text = score;
        Score.text = score;
        StartCoroutine(ColorFlash(Score.color, Score, colorFlash));
    }
    


    public IEnumerator ColorFlash(Color origColor, TMP_Text textComp, Color newColor)
    {
        textComp.color = newColor;
        yield return new WaitForSeconds(0.2f);
        textComp.color = origColor;
    }
    public IEnumerator WaitForSeconds(float t)
    {       
        yield return new WaitForSeconds(t);
        ScreenOverlay.text = "";
        ScreenOverlay.color = Color.white;
        yield return new WaitForSeconds(0.15f);
        ToggleGameEndPanel(true);
    }
}
