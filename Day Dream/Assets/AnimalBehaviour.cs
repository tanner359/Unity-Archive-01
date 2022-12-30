using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AnimalBehaviour : MonoBehaviour
{
    float moveSpeed = 2;


    // This should only run on the Host's side and will update to the clients


    // Start is called before the first frame update
    void Start()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        WanderCycle();
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
            //anim.SetBool("IsMoving", false);
            waitSeconds -= Time.deltaTime;
            moveTarget = new Vector2((moveDirection * (transform.position.x + GetRandomNum(0, 100))), transform.position.y + GetRandomNum(-100, 100));

        }
        if (waitSeconds < 0)
        {

            SetDirection(moveDirection);

            if (moveDuration > 0 && moveDirection != 0)
            {
                //anim.SetBool("IsMoving", true);
                moveDuration -= Time.deltaTime;
                gameObject.transform.position = Vector2.MoveTowards(transform.position, moveTarget, moveSpeed * Time.deltaTime);
            }
            else
            {
                moveDirection = GetRandomNum(-1, 2);
                waitSeconds = Random.Range(2f, 10f);
                moveDuration = Random.Range(2, 5);
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
