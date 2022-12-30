using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float movementSpeed;
    public float targetDistance;    
    Animator animator;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D targetInRange;
        transform.position = transform.position = new Vector2(transform.position.x + (-movementSpeed) * Time.deltaTime, transform.position.y);
        targetInRange = Physics2D.Raycast(transform.position + offset, Vector2.left, targetDistance);
        Debug.DrawRay(transform.position + offset, Vector2.left, Color.green, targetDistance);
        if (targetInRange.collider)
        {

            if (targetInRange.collider.gameObject.CompareTag("Player"))
            {                    
                    animator.Play("AxeSwing_Tank");                                    
                    Debug.Log("raycast hit player");
            }           
        }

        if (targetInRange.collider == false)
        {

            animator.SetBool("attack", false);
            Debug.Log("raycast did not hit player");

        }
    }


}
