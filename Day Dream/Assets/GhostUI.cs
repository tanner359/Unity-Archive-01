using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GhostUI : MonoBehaviour
{

    [SerializeField] public TMP_Text overlayText;
    


    public void OverlayText(string text, Color color)
    {
        overlayText.text = text;
        overlayText.color = color;
    }
}
