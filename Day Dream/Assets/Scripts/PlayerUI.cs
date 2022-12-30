using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;


public class PlayerUI : MonoBehaviour
{
    //Collectable UI References
    [SerializeField] TMP_Text woodCountText;
    [SerializeField] TMP_Text shardCountText;
    [SerializeField] TMP_Text foodCountText;

    //DN Cycle UI References
    [SerializeField] TMP_Text dayCycleTimeText;
    [SerializeField] Image dayCycleImage;
    [SerializeField] TMP_Text dayCycleStatusText;

    //PreGame UI References
    [SerializeField] TMP_Text ScreenTextOverlay;

    //General UI References
    [SerializeField] TMP_Text fireLuminosityText;
    [SerializeField] Image fireLuminosityBar;

    [SerializeField] TMP_Text foodBarText;
    [SerializeField] Image foodBar;

    [SerializeField] Sprite daySprite;
    [SerializeField] Sprite nightSprite;

    GameManager GM;

    Hashtable playerProperties;





    static float maxFireBar;
    static float maxFoodBar;

    private void Awake()
    {
        GM = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        playerProperties = PhotonNetwork.LocalPlayer.CustomProperties;
    }
    private void Start()
    {
        maxFoodBar = foodBar.rectTransform.sizeDelta.y;
        foodBar.rectTransform.sizeDelta = new Vector2(foodBar.rectTransform.sizeDelta.x, 0);
        maxFireBar = fireLuminosityBar.rectTransform.sizeDelta.x;
    }




    public void FireStatusUpdate(string _fireLuminosityText, float barSize)
    {
        fireLuminosityText.text = "Fire Strength :" + _fireLuminosityText + "%";

        if (barSize > 0 && barSize < maxFireBar)
        {
            fireLuminosityBar.rectTransform.sizeDelta = new Vector2(Mathf.Round(barSize * maxFireBar), fireLuminosityBar.rectTransform.sizeDelta.y);
        }
        
    }

    public void UpdateFoodUI(string _foodBarText, float barSize)
    {
        foodBarText.text = _foodBarText + "%";

        if (barSize > 0 && barSize < maxFoodBar)
        {
            foodBar.rectTransform.sizeDelta = new Vector2(foodBar.rectTransform.sizeDelta.x, Mathf.Round(barSize * maxFoodBar));
        }
        
    }


    public void UIUpdate(int _woodCountText, int _shardCountText, int _foodCountText)
    {
        woodCountText.text = _woodCountText.ToString();
        shardCountText.text = _shardCountText.ToString() + "/" + 6;
        foodCountText.text = _foodCountText.ToString();
    }
    
    public void DNCycleUpdate(string _dayCycleTimeText, string _dayCycleStatusText)
    {
        dayCycleTimeText.text = _dayCycleTimeText;
        dayCycleStatusText.text = _dayCycleStatusText;

        if(_dayCycleStatusText == "Day"){
            dayCycleImage.sprite = daySprite;
        }
        else if(_dayCycleStatusText == "Night"){
            dayCycleImage.sprite = nightSprite;
        }
    }
    public void ScreenOverlayText(string overlayText, Color textColor)
    {
        ScreenTextOverlay.text = overlayText;
        ScreenTextOverlay.color = textColor;
    }
  
}
