using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using UnityEditor;
using UnityEngine.Experimental.Rendering.LWRP;
using System.IO;
using UnityEngine.Animations;

public class Player_Core : MonoBehaviour
{
    //I only touched the Move method on line 101. Dan: 09:35PM. 10/27/2020.

    // Core References
    Rigidbody2D rb;
    Player_Input input_Actions;

    //Animation References
    public Animator anim;

    // Movement
    [Header("Movement Attributes")]
    public float move_Speed;
    public float ghost_Speed;
    private Vector2 move_Input;
    private bool moving;
    private bool facingRight = true;
    private bool lastDirection = false;

    // Collectible Info
    [Header("Collectible References")]
    public string shard_Tag;
    public string wood_Tag;
    public string food_Tag;
    public string fire_Tag;
    [Space(10)]
    public int shards_To_Win;

    [SerializeField] private int shards_Collected;
    [SerializeField] private int wood_Collected;
    [SerializeField] private int food_Collected;

    //Photon Networking
    public PhotonView PV;
    PhotonView ItemPV;

    //Player UI
    public PlayerUI playerUI;
    public GhostUI ghostUI;
    public PersonUI GlobalPersonUI;
    public GameObject woodInteractItem;
    public GameObject foodInteractItem;

    //player Death
    public bool safe = true;
    bool dead = false;

    public float deathTime = 3f;
    Transform ghostTeam;
    public UnityEngine.Experimental.Rendering.Universal.Light2D ghostEmission;

    //GameManager
    GameManager GM;
    Hashtable playerProperties;
    GameObject playerList;

    //campfire
    Campfire Campfire;





    private void Awake()
    {

        ghostTeam = GameObject.Find("Ghost_List").transform;
        transform.parent = GameObject.Find("Player_List").transform;
        rb = GetComponent<Rigidbody2D>();
        PV = GetComponent<PhotonView>();
        gameObject.name = PV.Owner.NickName;
        GM = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        //playerProperties = PhotonNetwork.LocalPlayer.CustomProperties;
        playerProperties = PhotonNetwork.LocalPlayer.CustomProperties;
        //worldProperties = GM.worldProperties;

        Campfire = GameObject.Find("Campfire").GetComponent<Campfire>();
        playerList = GM.gameObject.transform.Find("Player_List").gameObject;

    }

    void Start()
    {

        anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(PlayerPrefs.GetString(PV.Owner.NickName));

        if (!PV.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(rb);
            playerUI.gameObject.SetActive(false);
        }

        playerUI.UIUpdate(wood_Collected, shards_Collected, food_Collected);
    }

    private void FixedUpdate()
    {
        if (!PV.IsMine)
        {
            return;
        }
        Move();
    }
    void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }

        if (!safe && !dead && DayNightCycle.GetStatus() == "Night")
        {
            DeathTimer(deathTime);
        }
        else if (safe && timeElapsed != 0)
        {
            timeElapsed = 0;
        }


        if (shards_Collected >= shards_To_Win)
        {
            PV.RPC("Win", RpcTarget.AllBuffered, PhotonNetwork.NickName);
        }

        if (playerList.transform.childCount == 0)
        {
            PV.RPC("Lose", RpcTarget.AllBuffered, "Everyone was Killed");           
        }

    }

    float timeElapsed = 0;
    public void DeathTimer(float deathTime)
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= deathTime)
        {
            playerProperties = new Hashtable { { "isDead", true } };
            PhotonNetwork.LocalPlayer.SetCustomProperties(playerProperties);
            
            move_Speed = ghost_Speed;
            
            if(playerList.transform.childCount != 0)
            {
                StartCoroutine("DeathDialog");
                playerUI.gameObject.SetActive(false);
                ghostUI.gameObject.SetActive(true);
                dead = true;
            }
        }
    }
    IEnumerator DeathDialog()
    {
        if(playerList.transform.childCount == 0)
        {
            StopCoroutine("DeathDialog");
        }
        ghostUI.OverlayText("You Died", Color.red);
        yield return new WaitForSecondsRealtime(2f);
        ghostUI.OverlayText("Welcome to the Ghost Team", Color.cyan);
        yield return new WaitForSecondsRealtime(2f);
        ghostUI.OverlayText("Scare the campfire to try and kill the remaining survivors", Color.cyan);
        yield return new WaitForSecondsRealtime(3f);
        ghostUI.OverlayText("", Color.cyan);
    }




    #region Movement
    public void Move()
    {
        //Had to set another line in here so I added brackets to the if-else statement.
        if (move_Input != Vector2.zero)
        {

            transform.position = Vector2.MoveTowards(transform.position, transform.position + new Vector3(move_Input.x, move_Input.y), move_Speed * Time.deltaTime);
            anim.SetBool("IsMoving", true);
        }
        else
        {
            rb.velocity = Vector2.zero;
            anim.SetBool("IsMoving", false);
        }     
    }

    public void OnMovement(InputValue value)
    {

        if (!PV.IsMine)
        {
            return;
        }
        else
        {
            move_Input = value.Get<Vector2>();

            if (move_Input.x < 0 && facingRight)
            {
                Flip(facingRight);
                facingRight = false;
            }
            else if (move_Input.x > 0 && !facingRight)
            {
                Flip(facingRight);
                facingRight = true;
            }
        }
        
    }
    public void OnAddFood(InputValue value)
    {
        if (!PV.IsMine)
        {
            return;
        }
        if (foodInteractItem.activeSelf.Equals(true))
        {
            if (!Campfire.IsFoodMax() && (Campfire.maxFood - Campfire.getFoodCount()) < food_Collected)
            {
                PV.RPC("AddFood", RpcTarget.AllBuffered, Campfire.getFoodCount() + (Campfire.maxFood - Campfire.getFoodCount())); //send out rpc to all clients to update campfire wood to my given parameter
                food_Collected -= (Campfire.maxFood - Campfire.getFoodCount());
            } 
            else if((Campfire.maxFood - Campfire.getFoodCount()) >= food_Collected)
            {
                PV.RPC("AddFood", RpcTarget.AllBuffered, Campfire.getFoodCount() + food_Collected); //send out rpc to all clients to update campfire wood to my given parameter
                food_Collected -= food_Collected;
            }       
            
            playerUI.UIUpdate(wood_Collected, shards_Collected, food_Collected);       //updates the Local players UI with updated collectables count    
        }
    }

    [PunRPC]
    public void AddFood(int food)
    {
        Campfire.UpdateFood(food);
    }

    public void OnAddWood(InputValue value)
    {
        if (!PV.IsMine)
        {
            return;
        }
        if (woodInteractItem.activeSelf.Equals(true))
        {          
            if (!Campfire.IsWoodMax() && (Campfire.maxWood - Campfire.getWoodCount()) < wood_Collected)
            {
                PV.RPC("AddWood", RpcTarget.AllBuffered, Campfire.getWoodCount() + (Campfire.maxWood - Campfire.getWoodCount())); //send out rpc to all clients to update campfire wood to my given parameter
                wood_Collected -= (Campfire.maxWood - Campfire.getWoodCount());
            }
            else if((Campfire.maxWood - Campfire.getWoodCount()) >= wood_Collected)
            {
                PV.RPC("AddWood", RpcTarget.AllBuffered, Campfire.getWoodCount() + wood_Collected); //send out rpc to all clients to update campfire wood to my given parameter
                wood_Collected -= wood_Collected;
            }
            playerUI.UIUpdate(wood_Collected, shards_Collected, food_Collected); //updates the Local players UI with updated collectables count
        }
    }

    [PunRPC]
    public void AddWood(int wood)
    {
        Campfire.UpdateWood(wood);
    }

    

    

    

    public void Flip(bool isFlipped)
    {
        if (isFlipped)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        if (!isFlipped)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
    #endregion

    #region Pickups
    private void OnTriggerStay2D(Collider2D collision)
    {       
        if (collision.CompareTag("Interactable"))
        {
            if(wood_Collected > 0)
            {
                woodInteractItem.SetActive(true);               
            }
            else
            {
                woodInteractItem.SetActive(false);
            }

            if (food_Collected > 0)
            {
                foodInteractItem.SetActive(true);
                
            }
            else
            {
                foodInteractItem.SetActive(false);
            }
        }

        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Interactable"))
        {
            foodInteractItem.SetActive(false);
            woodInteractItem.SetActive(false);
        }
        if (collision.CompareTag(fire_Tag))
        {
            safe = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(fire_Tag))
        {
            safe = true;
        }
        if (other.CompareTag(shard_Tag) && !dead)
        {           
            shards_Collected++;
            playerUI.UIUpdate(wood_Collected, shards_Collected, food_Collected);
        }
        if (other.CompareTag(wood_Tag) && !dead)
        {           
            wood_Collected++;
            playerUI.UIUpdate(wood_Collected, shards_Collected, food_Collected);
        }
        if (other.CompareTag(food_Tag) && !dead)
        {           
            food_Collected++;
            playerUI.UIUpdate(wood_Collected, shards_Collected, food_Collected);
        }
    }

    [PunRPC]
    public void Win(string WinnerName)
    {
        GM.playerUI.ScreenOverlayText(WinnerName + " Wins", Color.green);
        ghostUI.OverlayText(WinnerName + " Wins", Color.green);
    }


    [PunRPC]
    public void Lose(string LoseText)
    {
        GM.playerUI.ScreenOverlayText(LoseText, Color.red);
        ghostUI.OverlayText(LoseText, Color.red);
    }
    #endregion

    public void OnEnable()
    {
        if (input_Actions == null)
            input_Actions = new Player_Input();

        input_Actions.Player.Enable();
    }

    public void OnDisable()
    {
        input_Actions.Player.Disable();
    }
}
