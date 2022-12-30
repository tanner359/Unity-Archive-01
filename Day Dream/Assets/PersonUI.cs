using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PersonUI : MonoBehaviour
{


    [SerializeField] public TMP_Text playerName;

    Hashtable playerProperties;
    GameManager GM;


    public void SetName(string name)
    {
        playerName.text = name;

        if (GetComponentInParent<PhotonView>().IsMine)
        {
            gameObject.SetActive(false);
        }
    }


    private void Awake()
    {
        GM = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        //playerProperties = PhotonNetwork.LocalPlayer.CustomProperties;
        //playerProperties = new Hashtable { { "Name", "Survivor_" + PhotonNetwork.LocalPlayer.ActorNumber.ToString() } };
        
    }

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponentInParent<PhotonView>().IsMine)
        {
            playerProperties = new Hashtable { { "Name", GetComponentInParent<PhotonView>().Owner.NickName } };
            PhotonNetwork.LocalPlayer.SetCustomProperties(playerProperties);
        }             
    }
}
