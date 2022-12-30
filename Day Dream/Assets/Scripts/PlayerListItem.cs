using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using System.IO;

public class PlayerListItem : MonoBehaviourPunCallbacks
{

    [SerializeField] TMP_Text text;
    [SerializeField] TMP_Text readyStatus;   
    [SerializeField] GameObject readyButton;
    public Image profile_Pic;
    public bool playerReady;

    public Player player;
    PhotonView PV;


    private void Awake()
    {       
        PV = GetComponent<PhotonView>();
    }
    private void Start()
    {        
        

        if (PhotonNetwork.LocalPlayer.ActorNumber != player.ActorNumber)
        {
            readyButton.gameObject.SetActive(false);
        }       
        readyButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            Debug.Log("Button Clicked");
            playerReady = !playerReady;
            SetReady(playerReady);
            Hashtable playerListProperties = new Hashtable() { { "ready", playerReady } };
            PhotonNetwork.LocalPlayer.SetCustomProperties(playerListProperties);
        });
        
    }
    public bool getReadyStatus()
    {
        return playerReady;
    }
    public void SetUp(Player _player)
    {
        player = _player;
        text.text = _player.NickName;
        readyStatus.text = "Not Ready";
        readyStatus.color = Color.red;
        SetIcon(Launcher.Instance.selected_Character.name);
        //playerListProperties["playerIcon"] = profile_Pic.sprite.ToString();
        //PhotonNetwork.LocalPlayer.SetCustomProperties(playerListProperties);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Hashtable playerListProperties = new Hashtable() { { "ready", playerReady } };
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerListProperties);          
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (player == otherPlayer)
        {
            Destroy(gameObject);
        }
    }   

    public override void OnLeftRoom()
    {
        Destroy(gameObject);
    }

    public void SetReady(bool isReady)
    {
        if (isReady)
        {
            playerReady = isReady;
            readyStatus.text = "Ready";
            readyStatus.color = Color.green;
            readyButton.GetComponent<Button>().interactable = false;
        }
    }

    public void SetIcon(string imageName)
    {      
        profile_Pic.sprite =  Resources.Load<Sprite>(imageName);
        PlayerPrefs.SetString(player.NickName, imageName);
    }
    
}
