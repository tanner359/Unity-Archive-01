using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Animator animator;

    public float movementSpeed;
    public float rotateSpeed;

    public bool flying = true;
    private bool stickbug = true;

    // Update is called once per frame
    void Update()
    {
        Vector2 input_Vec = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (input_Vec.y > 0)
        {
            animator.SetBool("Walking", true);
            gameObject.transform.position += -transform.right * movementSpeed * Time.deltaTime;
        }
        else if(input_Vec.y < 0)
        { 
            gameObject.transform.position += transform.right * movementSpeed * Time.deltaTime;
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }
        if(input_Vec.x != 0)
        {
            transform.Rotate(0, input_Vec.x * rotateSpeed * Time.deltaTime, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("Flying", flying);
            flying = !flying;
            if (!flying)
            {

            }
        }       
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            animator.SetBool("Stickbugged", stickbug);
            stickbug = !stickbug;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            animator.SetTrigger("Attack");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            animator.SetTrigger("Tail");
        }
    }
}
