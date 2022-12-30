using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Pun;

public class Campfire_Behaviour : MonoBehaviour
{
    [SerializeField] CircleCollider2D searchZone;
    Vector2 targetLocation;
    public float moveSpeed = 3;
    public float runSpeed = 5;
    public bool run;

    //Animation stuff
    public Animator anim;

    Rigidbody2D rb;
    Hashtable playerProperties;
    GameManager GM;
    DayNightCycle DN_Cycle;


    [SerializeField] List<Collider2D> colliders = new List<Collider2D>();

    // This should only run on the Host's side and will update to the clients


    private void Awake()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            enabled = false;
        }
        GM = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        DN_Cycle = GM.transform.GetComponentInChildren<DayNightCycle>();
        rb = GetComponent<Rigidbody2D>();
        //worldProperties = GM.worldProperties;
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (DayNightCycle.GetStatus() == "Night")
        {
            if (collision.CompareTag("Enemy"))
            {
                targetLocation = new Vector2(transform.position.x, transform.position.y) + new Vector2(GetDirectionX(transform, collision.transform), GetDirectionY(transform, collision.transform));
                SetDirection(targetLocation.x);
                run = true;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (DayNightCycle.GetStatus() == "Night")
        {
            if (collision.CompareTag("Spook"))
            {
                targetLocation = new Vector2(transform.position.x, transform.position.y) + new Vector2(GetDirectionX(transform, collision.transform), GetDirectionY(transform, collision.transform));
                SetDirection(targetLocation.x);
                run = true;
            }
        }
    }

    
    private void FixedUpdate()
    {
        if(Vector2.Distance(transform.position, targetLocation) < 1)
        {
            run = false;
        }


        if (DayNightCycle.GetStatus() == "Night")
        {
            anim.SetBool("IsNight", true);

            if (run)
            {
                anim.SetBool("IsScared", true);
                
                gameObject.transform.position = Vector2.MoveTowards(transform.position, targetLocation, runSpeed * Time.deltaTime);
            }
            else
            {
                anim.SetBool("IsScared", false);
                WanderCycle();
            }
        }
        else
        {
            anim.SetBool("IsNight", false);
        }

    }

    public float GetDirectionX(Transform tran1, Transform tran2)
    {

        if (tran1.position.x - tran2.position.x > 0)
        {
            return tran1.position.x - tran2.position.x;
        }
        else
        {
            return tran1.position.x - tran2.position.x;
        }
    }
    public float GetDirectionY(Transform tran1, Transform tran2)
    {
        if (tran1.position.y - tran2.position.y > 0)
        {
            return tran1.position.y - tran2.position.y;
        }
        else
        {
            return tran1.position.y - tran2.position.y;
        }
    }

    public void SetDirection(float xValue)
    {
        if (xValue > 0)
        {
            gameObject.transform.localScale = new Vector3(Mathf.Abs(gameObject.transform.localScale.x), gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        }
        else if (xValue < 0)
        {
            gameObject.transform.localScale = new Vector3(-Mathf.Abs(gameObject.transform.localScale.x), gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        }
    }

    public float moveDuration = 2f;
    public float waitSeconds = 3f;
    float moveDirection;
    Vector2 moveTarget;

    public void WanderCycle()
    {
        if (waitSeconds > 0)
        {
            anim.SetBool("IsMoving", false);
            waitSeconds -= Time.deltaTime;
            moveTarget = new Vector2((moveDirection * (transform.position.x + GetRandomNum(0, 100))), transform.position.y + GetRandomNum(-100, 100));

        }
        if (waitSeconds < 0)
        {

            SetDirection(moveDirection);

            if (moveDuration > 0 && moveDirection != 0)
            {               
                anim.SetBool("IsMoving", true);
                moveDuration -= Time.deltaTime;
                gameObject.transform.position = Vector2.MoveTowards(transform.position, moveTarget, moveSpeed * Time.deltaTime);
            }
            else
            {
                moveDirection = GetRandomNum(-1, 2);
                waitSeconds = Random.Range(2f, 5f);
                moveDuration = Random.Range(2, 10);
            }
        }
    }
    public int GetRandomNum(int min, int max)
    {
        int randomNum = Random.Range(min, max);
        if (randomNum == 0)
        {
            randomNum = Random.Range(min, max);
            if (randomNum != 0)
            {
                return randomNum;
            }
        }
        else
        {
            return randomNum;
        }
        return min;
    }


}
