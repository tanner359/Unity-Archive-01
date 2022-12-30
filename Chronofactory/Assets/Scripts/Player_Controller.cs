using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class Player_Controller : MonoBehaviour
{
    // UI
    public GameObject pauseMenu;
    //FMOD Event Manager
    [FMODUnity.EventRef] //Event Caller
    public string soundInput;
    FMOD.Studio.EventInstance Grab_Drop_Rotate;

    // General Variables
    private Transform playerPos;
    private Player player_Input;
    public int player_ID;
    private Rigidbody rb;    
    Animator animator;
    // Movement Variables
    public float speed;

    // Placement Marker Variables
    public GameObject blockPlacement;
    public GameObject obstructPlacement;
    public float xPlacementOffset;
    public float zPlacementOffset;
    public int placementRange;
    public float indicatorSpeed;
    private bool obstructed;
    public LayerMask obstructMask;

    // Pick Up/Drop/Throw Variables
    public List<string> pck_Tags = new List<string>();
    public Transform hand_Loc;
    public float pickup_Range;
    public Vector3 pickup_offset;
    public float throw_Force;

    private bool holding;
    private bool ready_To_Throw;

    private GameObject held_Obj;

    // Repair Variables
    public float repair_time;
    float repair_t;

    private bool repairing;

    int layer_Mask = 1 << 12;

    void Start()
    {       
        animator = GetComponent<Animator>();
        playerPos = GameObject.Find("Player").GetComponent<Transform>();
        player_Input = ReInput.players.GetPlayer(player_ID);
        rb = GetComponent<Rigidbody>();

        layer_Mask = ~layer_Mask;

        // FMOD Starter
        Grab_Drop_Rotate = FMODUnity.RuntimeManager.CreateInstance("event:/GDR_Event");
        Grab_Drop_Rotate.start();

        repair_t = repair_time;
    }


    void Update()
    {
        if (held_Obj == null)
        {
            holding = false;
        }

        Animations();
        if (player_Input.GetButtonDown("Pause"))
        {
            if (pauseMenu.activeSelf.Equals(false))
            {
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
            }
        }
        if (player_Input.GetButtonDown("Quit"))
        {
            Application.Quit();
        }
        CheckBlockPlacement();
        if (holding)
        {            
            held_Obj.transform.position = hand_Loc.position;
        }
        if (player_Input.GetButtonDown("Interact") && holding)
        {
            held_Obj.GetComponent<Transform>().Rotate(0, 90, 0);
            Rotate_Sound(); // Calls for Rotate Sound method from FMOD bool section
        }
        if (ready_To_Throw)
        {
            if (player_Input.GetButtonTimedPressUp("Pick Up", 0f, 0.7f))
                Check_Drop();
            else if (player_Input.GetButtonTimedPressDown("Pick Up", 0.7f))
                Check_Throw();
        }
        else
        {
            if (player_Input.GetButtonUp("Pick Up") && holding)
                ready_To_Throw = true;
        }

        if (player_Input.GetButtonDown("Pick Up") && !holding)
        {
            Check_Pickup();
        }
        Debug.DrawRay(transform.position + pickup_offset, transform.forward, Color.blue, pickup_Range);

        if (player_Input.GetButtonDown("Interact") && !holding)
        {
            repairing = true;
        }

        if (repairing)
        {
            Check_Repair();
        }
    }


    private void FixedUpdate()
    {
        Check_Move();
    }

    void Check_Move()
    {
        Vector3 input_Vec = new Vector3(player_Input.GetAxisRaw("Horizontal"), 0f, player_Input.GetAxisRaw("Vertical"));

        rb.velocity = input_Vec * speed;

        Quaternion target_Rot = Quaternion.LookRotation(rb.velocity);

        if (rb.velocity != Vector3.zero)           
            transform.rotation = Quaternion.Slerp(transform.rotation, target_Rot, 15f * Time.deltaTime);
    }

    void Check_Pickup()
    {
        if (!holding)
        {
            RaycastHit hit;            
            if (Physics.Raycast(transform.position + pickup_offset, transform.forward, out hit, pickup_Range, layer_Mask))
            {
                foreach (string tag in pck_Tags)
                {
                    if (hit.transform.gameObject.CompareTag("Box"))
                    {
                        holding = true;
                        held_Obj = Instantiate(hit.transform.gameObject.GetComponent<Machine_Box>().machine);
                        held_Obj.transform.position = hand_Loc.position;
                        held_Obj.layer = 12;
                        held_Obj.GetComponent<Collider>().isTrigger = true;
                        held_Obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                        Grab_Sound(); // Calls for Grab Sound method from FMOD bool section
                        break;
                    }
                    else if (hit.transform.gameObject.CompareTag(tag))
                    {
                        holding = true;
                        hit.transform.position = hand_Loc.position;
                        held_Obj = hit.transform.gameObject;
                        held_Obj.layer = 12;
                        held_Obj.GetComponent<Collider>().isTrigger = true;
                        held_Obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                        Grab_Sound(); // Calls for Grab Sound method from FMOD bool section
                        break;
                    }
                }
            }
        }
    }

    void Check_Drop()
    {
        if (holding && !obstructed)
        {
            holding = false;
            ready_To_Throw = false;
            held_Obj.GetComponent<Collider>().isTrigger = false;
            held_Obj.layer = 11;
            held_Obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            held_Obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
            if (held_Obj.CompareTag("Double"))
                held_Obj.GetComponent<Transform>().position = new Vector3(Mathf.Round(blockPlacement.GetComponent<Transform>().position.x), (held_Obj.transform.lossyScale.y * 1.3f), Mathf.Round(blockPlacement.GetComponent<Transform>().position.z));
            else
                held_Obj.GetComponent<Transform>().position = new Vector3(Mathf.Round(blockPlacement.GetComponent<Transform>().position.x), (held_Obj.transform.lossyScale.y / 2f), Mathf.Round(blockPlacement.GetComponent<Transform>().position.z));

            Throw_Sound(); // Calls for Drop/Throw Sound method from FMOD bool section
        }        
    }
    void Check_Throw()
    {
        ready_To_Throw = false;
        Check_Drop();
        held_Obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        held_Obj.GetComponent<Rigidbody>().AddForce((rb.transform.forward + rb.transform.up) * throw_Force, ForceMode.Impulse);
    }

    void Check_Repair()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + pickup_offset, transform.forward, out hit, pickup_Range, layer_Mask))
        {
            if (hit.transform.gameObject.GetComponentInChildren<Machine_Function>().needRepair)
            {
                speed = 0f;

                if (repair_t <= 0f)
                {
                    hit.transform.gameObject.GetComponentInChildren<Machine_Function>().machineHealth = 100;
                    hit.transform.gameObject.GetComponentInChildren<Machine_Function>().needRepair = false;
                    repair_t = repair_time;
                    speed = 5f;
                    repairing = false;
                }
                else
                {
                    Repair_Sound();
                    repair_t -= Time.deltaTime;
                    
                }
                    
            }
        }
    }

    void CheckBlockPlacement()
    {
        Vector3 playerPoint = new Vector3(transform.position.x+xPlacementOffset * transform.forward.x, transform.position.y, transform.position.z+zPlacementOffset * transform.forward.z);
        Vector3 placementPoint = playerPoint + transform.forward * placementRange;
        blockPlacement.transform.position = Vector3.Lerp(blockPlacement.transform.position, new Vector3(Mathf.Round(placementPoint.x), placementPoint.y+0.1f, Mathf.Round(placementPoint.z)), indicatorSpeed);
        obstructPlacement.transform.position = Vector3.Lerp(obstructPlacement.transform.position, new Vector3(Mathf.Round(placementPoint.x), placementPoint.y + 0.5f, Mathf.Round(placementPoint.z)), indicatorSpeed);
        Debug.DrawRay(placementPoint, Vector3.up, Color.blue);
        
        if (holding && (Physics.OverlapBox(blockPlacement.transform.position + new Vector3(0, 0.5f, 0), obstructPlacement.transform.lossyScale * 0.5f, Quaternion.identity, obstructMask).Length > 0)){
            obstructed = true;
        }
        else{
            obstructed = false;
        }



        
        if (holding && !obstructed)
        {
            blockPlacement.SetActive(true);
        }
        if (!holding)
        {
            blockPlacement.SetActive(false);           
        }
        if (obstructed)
        {
            obstructPlacement.SetActive(true);
            blockPlacement.SetActive(false);
        }
        if (!obstructed)
        {
            obstructPlacement.SetActive(false);
        }
    }
    //private void OnDrawGizmos()
    //{
    //    Vector3 playerPoint = new Vector3(transform.position.x + xPlacementOffset * transform.forward.x, transform.position.y, transform.position.z + zPlacementOffset * transform.forward.z);
    //    Vector3 placementPoint = playerPoint + transform.forward * placementRange;
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireCube(blockPlacement.transform.position + new Vector3(0, 0.5f, 0), obstructPlacement.transform.lossyScale * 0.5f);
    //}



    //FMOD bools

    void Rotate_Sound()
    {
        Debug.Log("Audio.Log: Player is rotating");
        Grab_Drop_Rotate.setParameterByName("GDR", 2); //changes sound parameter
        Grab_Drop_Rotate.start(); //plays sound
    }

    void Grab_Sound()
    {
        Debug.Log("Audio.Log: Player is grabbing");
        Grab_Drop_Rotate.setParameterByName("GDR", 1); //changes sound parameter
        Grab_Drop_Rotate.start(); //plays sound
    }

    void Throw_Sound()
    {
        Debug.Log("Audio.Log: Player is dropping/throwing");
        Grab_Drop_Rotate.setParameterByName("GDR", 3); //changes sound parameter
        Grab_Drop_Rotate.start(); //plays sound
    }

    void Repair_Sound()
    {
        Debug.Log("Audio.Log: Player is repairing");
        Grab_Drop_Rotate.setParameterByName("GDR", 4); //changes sound parameter
        Grab_Drop_Rotate.start(); //plays sound
    }


    void Animations()
    {
        if (player_Input.GetAxisRaw("Horizontal") != 0f || player_Input.GetAxisRaw("Vertical") != 0f)
        {
            animator.SetBool("isWalk", true);
            animator.SetBool("isIdle", false);

        }
        else
        {
            animator.SetBool("isIdle", true);
            animator.SetBool("isWalk", false);
        }
    }
}
