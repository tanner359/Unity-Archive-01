using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    public GameObject primaryHand;

    public int movement_Speed;
    public int running_Speed;
    bool onGround = true;
    public int lungeSpeed;
    public float lungeCooldown;
    float lungeDuration;

    Rigidbody2D PlayerRB;
    BoxCollider2D feetBC;
    EdgeCollider2D headCollider;
    public ContactFilter2D headContactFilter;
    public ContactFilter2D feetContactFilter;

    public float maxVelocity;
    public float initialVelocity = 0;
    float jumpTime = 0;
    public float jumpSpeed = 0;
    public float jumpHeight = 1;
    float jumpVelocity = 0;



    //dash vars

    float dashCooldown = 0;
    public int dashPower;
    bool dashReady = false;
    float dashduration = 0;
    float wait = 0.1f;
    float inputTime = 0;

    

    private void Start()
    {
        headCollider = gameObject.GetComponent<EdgeCollider2D>();
        feetBC = gameObject.GetComponent<BoxCollider2D>();
        PlayerRB = gameObject.GetComponent<Rigidbody2D>();
    }

    public bool getGrounded()
    {
        return onGround;
    }
    public int getDirection()
    {
        if (transform.localScale.x < 0)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();      
    }
    
    private void OnCollisionEnter2D(Collision2D collision){
        List<Collider2D> feetContacts = new List<Collider2D>();

        if (Physics2D.OverlapCollider(feetBC, feetContactFilter, feetContacts) > 0){
            onGround = true;
        }
    }
    public void Running(string direction, float speed){
        if(direction == "Left"){
            animator.SetBool("isRunning", true);
            gameObject.transform.position = transform.position + (transform.right * -speed) * Time.deltaTime;
        }
        else if(direction == "Right"){
            animator.SetBool("isRunning", true);
            gameObject.transform.position = transform.position + (transform.right * speed) * Time.deltaTime;
        }        
    }
    public void Walking(string direction, float speed){
        if (direction == "Left"){
            if (transform.localScale.x < 0){
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            animator.SetBool("isWalking", true);
            gameObject.transform.position = transform.position + (transform.right * -speed) * Time.deltaTime;
        }
        else if (direction == "Right"){
            if (transform.localScale.x > 0){
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            animator.SetBool("isWalking", true);
            gameObject.transform.position = transform.position + (transform.right * speed) * Time.deltaTime;
        }       
    }
    public void Lunge(string direction, int speed)
    {       
        if (lungeDuration > 0){
            animator.SetBool("Lunge", true);
            lungeDuration -= Time.deltaTime;
            if (direction == "Left"){
                gameObject.transform.position = transform.position + (transform.right * -speed) * Time.deltaTime;
            }
            else if (direction == "Right"){
                gameObject.transform.position = transform.position + (transform.right * speed) * Time.deltaTime;
            }
            else{
                animator.SetBool("Lunge", false);
            }
        }
    }
    public void Jump()
    {
        List<Collider2D> headCollisions = new List<Collider2D>();       
        jumpTime += Time.deltaTime * jumpSpeed;
        if (jumpTime < ((3 * Mathf.PI)/ 2) - maxVelocity && Physics2D.OverlapCollider(headCollider, headContactFilter, headCollisions) == 0){
            jumpVelocity = Mathf.Sin(0.9f * jumpTime) * jumpHeight;
        }
        else
        {
            jumpTime = ((3 * Mathf.PI) / 2) - maxVelocity;
            jumpVelocity = Mathf.Sin(0.9f * jumpTime) * jumpHeight;
        }
        PlayerRB.velocity = new Vector2(PlayerRB.velocity.x, jumpVelocity);
        animator.SetBool("isJumping", true);
        if (gameObject.GetComponent<Rigidbody2D>().velocity.y < 0){ // fall
            animator.SetBool("Falling", true);
        }      
    }
    public void Attack()
    {
        animator.SetTrigger("Attack");
    }
    public void GetInput()
    {
        if (lungeCooldown > 0){ //cooldown for lunge attack
            lungeCooldown -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown(KeyCode.Mouse0) && primaryHand.transform.childCount == 1)
        {
            Attack();
        }
        if (Input.GetAxisRaw("Horizontal") < 0){ //move left       
            Walking("Left", movement_Speed);
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetAxisRaw("Run") != 0){ // start running      
                Running("Left", running_Speed);
                if (Input.GetAxisRaw("Lunge") != 0 && lungeCooldown <= 0 && onGround || Input.GetKey(KeyCode.Mouse0) && lungeCooldown <= 0 && onGround){ // lunge if running and key is pressed
                    lungeCooldown = 3f;
                    lungeDuration = 1f;
                }
                if (lungeDuration > 0){
                    Lunge("Left", lungeSpeed);
                }
                else{
                    animator.SetBool("Lunge", false);
                }
            }
            else{
                lungeDuration = 0f;
                animator.SetBool("isRunning", false);
                animator.SetBool("Lunge", false);
            }
        }
        if (Input.GetAxisRaw("Horizontal") > 0){ //move right
            Walking("Right", movement_Speed);
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetAxisRaw("Run") != 0){ // start running
                Running("Right", running_Speed);
                if (Input.GetAxisRaw("Lunge") != 0 && lungeCooldown <= 0 || Input.GetKey(KeyCode.Mouse0) && lungeCooldown <= 0){ // lunge if running and key is pressed
                    lungeCooldown = 3f;
                    lungeDuration = 1f;
                }
                if (lungeDuration > 0){
                    Lunge("Right", lungeSpeed);
                }
                else{
                    animator.SetBool("Lunge", false);
                }
            }
            else{
                lungeDuration = 0f;
                animator.SetBool("isRunning", false);
                animator.SetBool("Lunge", false);
            }
        }
        if (Input.GetAxisRaw("Horizontal") == 0){
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
        }      
        if (Input.GetKeyDown(KeyCode.Space) && onGround == true || Input.GetKeyDown(KeyCode.JoystickButton0) && onGround == true){ // jump       
            onGround = false;
            jumpTime = initialVelocity;
        }
        if (!onGround){
            Jump();
        }
        else{
            animator.SetBool("isJumping", false);         
            animator.SetBool("Falling", false);           
        }        
        if (dashCooldown > 0){
            dashCooldown -= Time.deltaTime;
        }
        if (dashCooldown <= 0){
            
            if (Input.GetAxisRaw("Horizontal") != 0){
                inputTime += Time.deltaTime;
            }
            else{
                inputTime = 0;
            }
            if(inputTime > 0.15f){
                wait = 0f;
            }
            if (Input.GetAxisRaw("Horizontal") != 0 && wait <= 0 && inputTime < 0.15f){               
                wait = 0.2f;
            }          
            else if (Input.GetAxisRaw("Horizontal") != 0 && dashReady){
                dashduration = 0.5f;
                dashCooldown = 3.5f;
                dashReady = false;
            }           
            if (wait > 0){
                wait -= Time.deltaTime;
                if (Input.GetAxisRaw("Horizontal") == 0){
                    if(wait > 0f && wait < 0.1f){
                        dashReady = true;
                    }
                }
            }
            else if(wait <= 0){
                dashReady = false;
            }
        }
        if (dashduration > 0){
            dashduration -= Time.fixedDeltaTime;
            animator.SetBool("isDashing", true);
            gameObject.transform.position = gameObject.transform.position + (getDirection() * Vector3.right * dashPower * Time.deltaTime);
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(getDirection() * dashPower*10, 0), ForceMode2D.Force);
        }             
    }
}

