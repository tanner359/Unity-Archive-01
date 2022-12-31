using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Move_Script : MonoBehaviour
{
	
	private int moveTime;
	private Vector3 velocity;
	public GameObject standingTile;
	private GameObject homeTile;
	public GameObject theWholeLevel;
	public GameObject theSprite;

    private Blind_Script bl_scr;

    public bool can_Move;
    bool check_Start_Dialogue;

    public Animator transition_Anim;

    private AudioSource audioSrc;

    void Start()
    {
		velocity = Vector3.zero;
        moveTime = 10;
		RaycastHit hit;

		if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity))
        {
			standingTile = hit.collider.gameObject;
			homeTile = standingTile;
        }

        bl_scr = GetComponent<Blind_Script>();
        can_Move = true;
        check_Start_Dialogue = false;

        audioSrc = GetComponent<AudioSource>();
    }
	
	void PlayerMakeMove(Vector3 dirPoint) 
	{
		RaycastHit hit;

		if (Physics.Raycast(transform.position + dirPoint, Vector3.down, out hit, Mathf.Infinity))
        {
			if (hit.collider.gameObject.tag == "Tile")
			{

                standingTile.SendMessage("Stepped_Off", SendMessageOptions.DontRequireReceiver);
				moveTime = 10;
				theWholeLevel.BroadcastMessage("Turn_Reset", SendMessageOptions.DontRequireReceiver);
				standingTile = hit.collider.gameObject;
                Tile_Script scr = standingTile.GetComponent<Tile_Script>();
				transform.position = new Vector3(standingTile.transform.position.x, transform.position.y, standingTile.transform.position.z);
				theWholeLevel.BroadcastMessage("Take_Turn", SendMessageOptions.DontRequireReceiver);

				if (scr.looked_At && !bl_scr.is_Blind) 
				{
					Die();
				}

                if (bl_scr.is_Blind)
                {
                    bl_scr.moves_Left--;
                }

				standingTile.SendMessage("Stepped_On", SendMessageOptions.DontRequireReceiver);
			}

			if (hit.collider.gameObject.tag == "Enemy") 
			{
				//Something telling the player not to fucking do that.
			}
        }
	}
	
	public void Die ()
	{
        if (can_Move)
        {
            audioSrc.Play();
            can_Move = false;
            StartCoroutine(LoadScene());
        }
    }

    IEnumerator LoadScene()
    {
        transition_Anim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Toggle_Move()
    {
        can_Move = !can_Move;
    }

    void Update()
    {
        if (!check_Start_Dialogue)
            standingTile.SendMessage("Stepped_On", SendMessageOptions.DontRequireReceiver);

        if (moveTime <= 0 && can_Move)
        {
			if (Input.GetKeyDown(KeyCode.W))
            {
				PlayerMakeMove(new Vector3(0, 100, 1));
			}
            else if (Input.GetKeyDown(KeyCode.S))
            {
				PlayerMakeMove(new Vector3(0, 100, -1));
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
				PlayerMakeMove(new Vector3(1, 100, 0));
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
				PlayerMakeMove(new Vector3(-1, 100, 0));
            }
		}
        else
        {
			moveTime--;
		}

		theSprite.transform.position = Vector3.SmoothDamp(theSprite.transform.position, transform.position, ref velocity, 0.2f);

        if (standingTile.GetComponent<Tile_Script>().looked_At && !bl_scr.is_Blind)
        {
            Die();
        }
    }
}
