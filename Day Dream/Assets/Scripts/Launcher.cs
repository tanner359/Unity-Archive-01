using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using UnityEngine.UI;
using System.IO;
using ExitGames.Client.Photon;
using Hashtable = ExitGames.Client.Photon.Hashtable;


public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher Instance;
    [SerializeField] TMP_InputField roomNameInputField;
    [SerializeField] TMP_InputField playerNameInputField;
    [SerializeField] TMP_Text errorText;
    [SerializeField] TMP_Text roomNameText;
    [SerializeField] Transform roomListContent;
    [SerializeField] public Transform playerListContent;
    [SerializeField] GameObject roomListItemPrefab;
    [SerializeField] GameObject playerListItemPrefab;
    [SerializeField] GameObject startGameButton;
    [SerializeField] public Sprite selected_Character;

    bool connectionEstablished;

    PhotonView PV;


    void Awake()
    {       
        Instance = this;
        
    }
    private void Start()
    {
        PV = GetComponent<PhotonView>();
        
    }

    public void StartConnection()
    {
        if (!connectionEstablished)
        {
            Debug.Log("Connecting");
            PhotonNetwork.ConnectUsingSettings();
            MenuManager.Instance.OpenMenu("connecting");
            connectionEstablished = true;
        }
        else
        {
            MenuManager.Instance.OpenMenu("menu");
        }
        
    }
    
    void Update()
    {
        PhotonNetwork.NickName = playerNameInputField.text;
        
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        Debug.Log("Target: " + targetPlayer.NickName + "Changed prop: " + changedProps);
        object playerIcon;

        // check if ready was the property changed
        if (changedProps.TryGetValue("playerIcon", out playerIcon))
        {
            Debug.Log(playerIcon.ToString());
            Debug.Log(targetPlayer);

            GameObject playerlistitem = null;
            // gets the game playerlistobject of the targetPlayer
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                if (playerListContent.GetChild(i).GetComponent<PlayerListItem>().player.NickName == targetPlayer.NickName)
                {                   
                    playerlistitem = playerListContent.GetChild(i).gameObject;
                    Debug.Log("playerOBJ: " + playerlistitem+ "_" + playerListContent.GetChild(i).GetComponent<PlayerListItem>().player.NickName);
                }
            }
            playerlistitem.GetComponent<PlayerListItem>().SetIcon(playerIcon.ToString());   // if changed is true set player setReady to bool property value
        }

        object property;

        // check if ready was the property changed
        if (changedProps.TryGetValue("ready", out property)) 
        {
            GameObject playerlistitem = null;
            // gets the game playerlistobject of the targetPlayer
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                if (playerListContent.GetChild(i).GetComponent<PlayerListItem>().player == targetPlayer)
                {
                    playerlistitem = playerListContent.GetChild(i).gameObject;
                }
            }
            playerlistitem.GetComponent<PlayerListItem>().SetReady((bool)property);   // if changed is true set player setReady to bool property value
        }
        
        // goes through player list to see if all players are ready
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            if (playerListContent.GetChild(i).GetComponent<PlayerListItem>().getReadyStatus() == false)
            {
                startGameButton.GetComponent<Button>().interactable = false;
                Debug.Log("start button false");
                return;
            }
            else
            {
                Debug.Log("start button true");
                startGameButton.GetComponent<Button>().interactable = true;
            }
        }
    }

    public void StartGame()
    {       
        PhotonNetwork.LoadLevel(1);
        PhotonNetwork.CurrentRoom.IsOpen = false;
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    public override void OnJoinedLobby()
    {
        MenuManager.Instance.OpenMenu("menu");
        Debug.Log("Joined Lobby");
        playerNameInputField.text = Name_Generator.Generate_Name();

    }
    public void GenerateName()
    {
        playerNameInputField.text = Name_Generator.Generate_Name();
        PhotonNetwork.NickName = playerNameInputField.text;
    }
    
    public void CreateRoom()
    {      
        if (string.IsNullOrEmpty(roomNameInputField.text))
        {
            return;
        }
        PhotonNetwork.CreateRoom(roomNameInputField.text);
        MenuManager.Instance.OpenMenu("loading");
    }

    public override void OnJoinedRoom()
    {
        MenuManager.Instance.OpenMenu("room");
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;
        PhotonNetwork.CurrentRoom.MaxPlayers = 4;

        Player[] players = PhotonNetwork.PlayerList;




        foreach (Transform child in playerListContent)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < players.Length; i++)
        {                             
            
            Debug.Log("Joined Room");
            Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
        startGameButton.GetComponent<Button>().interactable = false;

        Hashtable playerProperties = new Hashtable { { "playerIcon", selected_Character.name } };
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerProperties);
    }   
    
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorText.text = "Room Creation Failed: " + message;
        MenuManager.Instance.OpenMenu("error");
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenu("loading");
    }
    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.Instance.OpenMenu("connecting");
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        errorText.text = message;
        MenuManager.Instance.OpenMenu("error");
    }
    public override void OnLeftRoom()
    {
        MenuManager.Instance.OpenMenu("title");
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }
        for(int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
                continue;
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Player Entered");
        Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);

        Hashtable playerProperties = new Hashtable { { "playerIcon", selected_Character.name } };
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerProperties);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

}
