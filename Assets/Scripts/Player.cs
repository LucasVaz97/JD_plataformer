using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class Player : MonoBehaviour
{

    // state

    bool isAlive = true;

    //component references

    Rigidbody2D playerRigidbody;
    Animator playerAnimator;
    BoxCollider2D playerColiderFeet;
    BoxCollider2D playerBodyColider;
    BoxCollider2D[] coliders;
    SpriteRenderer playerSprite;
    [SerializeField] GameObject playerHealth;



    // config

    [SerializeField] float runSpeed = 7f;
    [SerializeField] float jumpSpeed = 17f;
    int numberofJumps = 2;
    int numberofDashes = 1;
    int health = 100;
    int heart = 10;
    public float limit;
    float facingDirection = 1;
    float lockedTime;
    float ivunerableTime = 0;
    float lockedCounter = 0;
    float jumpCounter = 0;
    float damageKick;
    bool invulnerable = false;
    public bool isLocked = false;
    bool isGrounded;
    bool canJump = true;

    float attackCowdown;
    float attackDuration;
    bool isAttacking = false;

    float dashDuration;
    bool dashTrigger;
    float dashCowdown;
    bool isDashing = false;
    int canDash = 1;

    float idleTimer;
    float startTime;
    Color dmgCollor; 
    List<Animator> playerHealthList;




    public Collider2D attackPos;



    // Use this for initialization
    private void Awake()
    {
        playerHealthList = playerHealth.GetComponent<HealthScript>().heartList;
       

    }
    void Start()
    {
        Time.timeScale = 1;
        startTime = Time.time;
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        coliders = GetComponents<BoxCollider2D>();
        playerColiderFeet = GetComponent<BoxCollider2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        attackPos.enabled = false;
        dmgCollor= new Color(0.4513617f, 0.4513617f, 0.4513617f, 1);



    }


    // Update is called once per frame
    void FixedUpdate()

    {
        if (isAlive == false || isLocked == true || isDashing == true)
        {
            return;
        }
        Run();
        FlipSprite();

    }
    void Update()
    {
 
        InvulnerableTime();
        LockedTime(limit);
        Dash();
        Attack();
        JumpAnimation();



        if (isAlive == false || isLocked == true || isDashing == true)
        {
            return;
        }
     // Triggers sets variables of its respected function
        Jump();
        DashTrigger();
        AttackTrigger();
    }



    //Run funciontion 
    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, playerRigidbody.velocity.y);
        playerRigidbody.velocity = playerVelocity;
        bool isMoving = Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon;
        playerAnimator.SetBool("Running", isMoving);
     //Time to enter idle after speed = 0
        if (isMoving)
        {
            idleTimer = 0.1f;
            playerAnimator.SetBool("idle", false);
        }
        else
        {
            if (idleTimer >= 0)
            {
                idleTimer = idleTimer - Time.fixedDeltaTime;

            }
            else
            {
                playerAnimator.SetBool("idle", true);
            }
        }
    }

    //Sets Dash duration and Dashbool
    public void DashTrigger()
    {

        float controlThrow = CrossPlatformInputManager.GetAxis("Fire2");
  
        if (controlThrow < 0.5)
        {

            canDash = 1;
        }


        if (controlThrow > 0 && dashCowdown <= 0 && numberofDashes > 0 && canDash == 1)

        {
            dashTrigger = true;
        }

        if (dashTrigger && isAttacking == false)

        {
            isDashing = true;
            numberofDashes = numberofDashes - 1;
            isLocked = true;
            limit = 0.3f;
            dashDuration = 0.3f;
            dashCowdown = 0.5f;

        }
    }
 
    private void Dash()
    {
        playerAnimator.SetBool("Dashing", isDashing);
        if (isDashing && dashTrigger && isAttacking == false)
        {
            if (dashDuration >= 0)
            {

                playerRigidbody.gravityScale = 0;
                playerRigidbody.velocity = new Vector2(15f * facingDirection, 0f);
                coliders[1].offset = new Vector2(-0.02516603f, -0.2941289f);
                coliders[1].size = new Vector2(0.5949793f, 0.7919073f);
                dashDuration = dashDuration - Time.deltaTime;
                
            }
            else
            {
                playerRigidbody.gravityScale = 5f;
                playerRigidbody.velocity = new Vector2(0f, 0f);
                coliders[1].offset = new Vector2(-0.0251f, -0.1751f);
                coliders[1].size = new Vector2(0.5949f, 1.02994f);
                isDashing = false;
                dashTrigger = false;
                
                canDash = 0;

            }
        }

        if (dashCowdown > 0)
        {
            dashCowdown = dashCowdown - Time.deltaTime;
        }

    }


    private void AttackTrigger()
    {
        if (CrossPlatformInputManager.GetButtonDown("Fire1") && attackCowdown <= 0 && isDashing == false)
        {
            attackCowdown = 0.35f;
            attackDuration = 0.09f;
            isAttacking = true;
            playerAnimator.SetTrigger("Attack");
        }

    }

    private void Attack()
    {
        if (isAttacking)
        {
            if (attackDuration > 0)
            {
                attackPos.enabled = true;
                attackDuration = attackDuration - Time.deltaTime;

            }
            else
            {
                attackPos.enabled = false;
                isAttacking = false;
                runSpeed = 7;

            }
        }

        if (attackCowdown > 0)
        {
            attackCowdown = attackCowdown - Time.deltaTime;
        }

    }

    private void Jump()
    {
    
        if (playerColiderFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            playerAnimator.SetBool("grounded", true);
            numberofJumps = 2;
            numberofDashes = 1;
            jumpCounter = 0;
            isGrounded = true;

        }
        else
        {
            playerAnimator.SetBool("grounded", false);
            isGrounded = false;

        }
        if (CrossPlatformInputManager.GetButton("Jump") && numberofJumps > 0)
        {

            if (jumpCounter < 0.2f)
            {
                jumpCounter = jumpCounter + Time.deltaTime;
                playerRigidbody.velocity = Vector2.up * 10;
                playerAnimator.SetBool("jumping", true);
            }
            else
            {
                canJump = false;
                playerAnimator.SetBool("jumping", false);
            }
        }

        if (CrossPlatformInputManager.GetButtonUp("Jump"))
        {
            numberofJumps = numberofJumps - 1;
            jumpCounter = 0;
            canJump = true;
            playerAnimator.SetBool("jumping", false);
        }

    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !invulnerable)
        {
            invulnerable = true;
            startTime = Time.time;

            isLocked = true;
            limit = 0.5f;
            lockedCounter = 0;
            health = health - 1;
            heart = heart - 1;
            playerRigidbody.gravityScale = 5f;
            isDashing = false;
            dashTrigger = false;
            if (heart >= 0)
            {
                playerHealthList[heart].SetTrigger("damage");

            }
      
            if (health == 0)
            {
                isAlive = false;
                playerAnimator.SetTrigger("Dying");
            }

            if (playerRigidbody.transform.position.x > collision.gameObject.transform.position.x)
            {
                damageKick = 10f;
            }
            else
            {
                damageKick = -10f;

            }
            playerRigidbody.velocity = new Vector2(damageKick, 10f);

        }

    }

    private void LockedTime(float limit)
    {
        if (isLocked)
        {
            lockedCounter += Time.deltaTime;



            if (lockedCounter >= limit)
            {
                lockedCounter = 0;
                isLocked = false;
            }
        }
    }

   
    private void InvulnerableTime()
    {
      
        if (invulnerable)
        {

            ivunerableTime+= Time.deltaTime;
            float t = Mathf.Sin((Time.time - startTime) * 16f);
            playerSprite.color = Color.Lerp(Color.white, dmgCollor, t);

            if (ivunerableTime>= 1f)
            {
                ivunerableTime= 0;
                invulnerable = false;
                playerSprite.color = Color.white;
            }
        }
    }



    private void FlipSprite()


    {
        bool isMoving = Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon;


        if (isMoving && isAttacking == false)

        {
            facingDirection = Mathf.Sign(playerRigidbody.velocity.x);
            transform.localScale = new Vector2(facingDirection, 1f);

        }

    }


    public void WhenHit()
    {
        runSpeed =
            0f; //when attack animation is on change to 0;
    }

    private void JumpAnimation()
    {
        if (playerRigidbody.velocity.y <= -0.001)
        {
            playerAnimator.SetBool("Falling", true);

        }
        if (playerRigidbody.velocity.y >= 0)
        {
            playerAnimator.SetBool("Falling", false);
        }

        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            playerAnimator.SetTrigger("jump");

        }
        if (CrossPlatformInputManager.GetButtonUp("Jump"))
        {
          
            playerAnimator.SetBool("jumping", false);
        }


    }





}
