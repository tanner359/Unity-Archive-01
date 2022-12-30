using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using ExitGames.Client.Photon;
using Photon.Realtime;
using UnityEngine.Experimental.Rendering.LWRP;

public class GameManager : MonoBehaviourPunCallbacks
{


    [SerializeField] public PlayerUI playerUI; // Player's UI is added from Player_Core when they spawn in the game.
    [SerializeField] public Player_Core player_Core;

    public Transform playerListContent;
    public Transform ghostListContent;

    [SerializeField] public GameObject localPlayerObject;
    [SerializeField] public bool localPlayerFound;

    [SerializeField] Campfire Campfire;
    [SerializeField] Light_Control LightControl;

    [SerializeField] public Hashtable playerProperties = new Hashtable();
    [SerializeField] public Hashtable worldProperties;

    [SerializeField] public bool gameStart = false;

    PhotonView PV;

    //Only the host will keep track of gameManagement, this GM will send out info to other client to update there UI's such as time etc.
    

    void Awake()
    {

        PV = gameObject.GetComponent<PhotonView>();
    }

    private void Update()
    {        
        //gets the game object of the local player
        if (localPlayerFound.Equals(false))
        {          
            for (int i = 0; i < playerListContent.childCount; i++)
            {
                if (playerListContent.GetChild(i).GetComponent<PhotonView>().IsMine)
                {
                    localPlayerObject = playerListContent.GetChild(i).gameObject;
                    player_Core = localPlayerObject.GetComponent<Player_Core>();
                    playerUI = localPlayerObject.GetComponentInChildren<PlayerUI>();
                }

                
            }
            localPlayerFound = true;           
        }

        if(gameStart == false)
        {
            if(playerListContent.transform.childCount == PhotonNetwork.PlayerList.Length)
            {
                gameStart = true;
            }
        }
    }




    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        object name;

        for (int i = 0; i < playerListContent.childCount; i++)
        {
            if (playerListContent.GetChild(i).GetComponent<PhotonView>().Owner.NickName == targetPlayer.NickName) // if a player was the target
            {
                GameObject targetPlayerOBJ = playerListContent.GetChild(i).gameObject;
                if (targetPlayerOBJ != null)
                {
                    if (changedProps.TryGetValue("Name", out name))
                    {
                        Debug.Log(targetPlayer.NickName + "updated name to : " + name.ToString());
                        targetPlayerOBJ.GetComponentInChildren<PersonUI>().SetName(name.ToString());                       
                    }

                    if (changedProps.ContainsKey("isDead").Equals(true))
                    {
                        Debug.Log("change to ghost");
                        targetPlayerOBJ.transform.Find("GhostStateEmission").GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().enabled = true;
                        targetPlayerOBJ.transform.SetParent(ghostListContent);
                        targetPlayerOBJ.tag = "Enemy";
                        targetPlayerOBJ.GetComponent<Animator>().SetBool("IsDead", true);
                    }
                }
            }
        }                    
    }


}
