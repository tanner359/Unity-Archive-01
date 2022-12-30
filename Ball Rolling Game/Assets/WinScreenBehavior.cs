using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreenBehavior : MonoBehaviour
{
    
    public Text splashText;
    public GameObject coin24, coin25, coin26, coin27, coin28, coin29, coin30, coin31, coin32;
    public GameObject player;
    public void Start()
    {
        splashText.gameObject.SetActive(false);     
    }

    // Update is called once per frame
    void Update()
    {
        
        if ((!coin24 && !coin25 && !coin26 && !coin27 & !coin28 && !coin29 && !coin30 && !coin31 && !coin32))
        {           
            Debug.Log("coins picked up");
            if(player.GetComponent<item_pickup>().score < 21)
            {
                splashText.gameObject.SetActive(true);
                splashText.text = "F GRADE \n FAILURE!!!";
            }
            if (player.GetComponent<item_pickup>().score > 21 && player.GetComponent<item_pickup>().score <= 25)
            {
                splashText.gameObject.SetActive(true);
                splashText.text = "D GRADE \n I'VE SEEN WORSE";
            }
            if (player.GetComponent<item_pickup>().score > 25 && player.GetComponent<item_pickup>().score <= 28)
            {
                splashText.gameObject.SetActive(true);
                splashText.text = "C GRADE \n OK!";
            }
            if (player.GetComponent<item_pickup>().score > 28 && player.GetComponent<item_pickup>().score <= 31)
            {
                splashText.gameObject.SetActive(true);
                splashText.text = "B GRADE \n GREAT!";
            }
            if (player.GetComponent<item_pickup>().score > 31 && player.GetComponent<item_pickup>().score < 33)
            {
                splashText.gameObject.SetActive(true);
                splashText.text = "A GRADE \n FANTASTIC!";
            }
            if (player.GetComponent<item_pickup>().score == 33)
            {
                splashText.gameObject.SetActive(true);
                splashText.text = "S RANK \n GODLIKE!!!!";
            }
        }       
    }
}
