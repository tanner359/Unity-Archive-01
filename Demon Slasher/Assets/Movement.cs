using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb;
    public float movementSpeed;
    public float jumpForce;
    public float fallForce;
    Animator animator;
    SpriteRenderer SpriteRen;
    public float jumpsLeft = 2;
    GameObject attacks;
    // Start is called before the first frame update
    void Start()
    {
        attacks = GameObject.Find("Attacks");
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        SpriteRen = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {       
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, 0));
            //GameObject.Find("Attacks").transform.rotation.SetLookRotation(Vector2.left);
            //animator.SetBool("Walk", true);
            //attacks.transform.SetPositionAndRotation(attacks.transform.position, Quaternion.Euler(0, 0, 0));
            transform.position = new Vector2(transform.position.x + (movementSpeed) * Time.deltaTime, transform.position.y);
        } //move right + start animation
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 180, 0));
            //animator.SetBool("Walk", true);
            //attacks.transform.SetPositionAndRotation(attacks.transform.position, Quaternion.Euler(0, 180, 0));
            transform.position = new Vector2(transform.position.x + (- movementSpeed) * Time.deltaTime, transform.position.y);                       
        } //move left + start animation
        if (Input.GetKey(KeyCode.A) == false && Input.GetKey(KeyCode.D) == false && Input.GetKey(KeyCode.LeftArrow) == false && Input.GetKey(KeyCode.RightArrow) == false)
        {
            //animator.SetBool("Walk", false);       
        } //animation stop
        if (Input.GetKeyDown(KeyCode.Space) && jumpsLeft > 0)
        {
            rb.AddForce(new Vector2(0, jumpForce));
            jumpsLeft--;
        } //jumping + no animation yet
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            rb.AddForce(new Vector2(0, -fallForce));
        } //quick fall + no animation yet        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider)
        {
            jumpsLeft = 2;
        }
    }
}
