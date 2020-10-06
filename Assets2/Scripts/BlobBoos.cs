using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlobBoos : MonoBehaviour
{
    Rigidbody2D bloobRigidbody;
    [SerializeField] int Hp;
    bool isLocked = false;
    public bool isImune;
    float lockedCounter;
    float imuneCounter;
    float imuneTime = 0.3f;
    float turnTime;
    [SerializeField] int velocityMultiplier;
    [SerializeField] int facing;
    int layerMask;
    public bool jump;
    bool grounded;
    bool canTurn=true;
    public Transform meeple;
    Color boosCollor;
    SpriteRenderer bossSprite;
    LoadScene loadO;

    // Use this for initialization

    void Start()
    {
        bossSprite = GetComponent<SpriteRenderer>();
        layerMask = LayerMask.NameToLayer("Ground");
        meeple = this.gameObject.transform.GetChild(0);
        loadO = FindObjectOfType<LoadScene>();


        bloobRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        CheckWall();
        CheckDown();
        InvulnerableTime();
        LockedTime(0.3f);




        if (isLocked == true)
        {
            return;
        }
        if (grounded)
        {
            bloobRigidbody.velocity = new Vector2(velocityMultiplier * 2.5f, bloobRigidbody.velocity.y);

        }


        if (jump)
        {
            StartCoroutine("JumpRoutine");

        }




    }



    public void TakeDamage(float damage, float direction)
    {
        Hp = Hp - 1;
        bossSprite.color = bossSprite.color + new Color(0, -0.10f, 0);
        isImune = true;
        isLocked = true;
        if (Hp == 15)
        {
            velocityMultiplier = 4*facing;
        }
        if (Hp == 10)
        {
            velocityMultiplier = 6 * facing;
        }
        if (Hp == 5)
        {
            velocityMultiplier = 8 * facing;

        }

        if (grounded)
        {
            bloobRigidbody.velocity = new Vector2(7f *(direction),0);

        }
        else
        {
            bloobRigidbody.velocity = new Vector2(15f * (direction), bloobRigidbody.velocity.y);

        }

        if (Hp <= 0)
        {
            StartCoroutine("Win");

        }
    }


    private IEnumerator TurnRoutine()
    {
        int a = velocityMultiplier;
        facing = facing * -1;
        velocityMultiplier = 0;
        yield return new WaitForSeconds(1f);
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        StartCoroutine("JumpRoutine");
        yield return new WaitForSeconds(0.6f);
        StartCoroutine("JumpRoutine");
        velocityMultiplier = -1 * a;
        canTurn = true;
   
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
        if (isImune)
        {
            imuneCounter += Time.deltaTime;

            if (imuneCounter >= imuneTime)
            {
                imuneCounter = 0;
                isImune = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        float directionx;
        float directiony;
        if (collision.gameObject.tag == "Sword")
        {
            AttackScript s = collision.gameObject.GetComponent<AttackScript>();

            directionx = transform.position.x - collision.gameObject.transform.position.x;
            directiony = transform.position.y - collision.gameObject.transform.position.y;
            if (s.getAtkDirect() == -1)
            {

                TakeDamage(1, 0);
                s.GetUp();

            }
            else
            {
                TakeDamage(1, Mathf.Sign(directionx));

            }

        }


    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            bloobRigidbody.velocity = new Vector2(0f, 0f);

        }
    }

    private void CheckWall()
    {
        RaycastHit2D hit = Physics2D.Raycast(meeple.transform.position, new Vector2(1, 0),facing, 1 << layerMask);
        Debug.DrawRay(meeple.transform.position, new Vector2( facing, 0), Color.green);
        if (hit.collider != null)
        {
   
            if (canTurn)
            {
                StartCoroutine("TurnRoutine");

            }
           


        }
    }

    private void CheckDown()
    {
        RaycastHit2D hit = Physics2D.Raycast(meeple.transform.position + new Vector3(facing * 0.5f, 0, 0), new Vector2(0, -1), 1, 1 << layerMask);
        RaycastHit2D hit2 = Physics2D.Raycast(meeple.transform.position, new Vector2(0, -0.5f), 1, 1 << layerMask);
        if (hit2.collider != null)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
       
    }

    IEnumerator JumpRoutine()
    {
        print("pulei");
        bloobRigidbody.velocity = new Vector2(0, 13);
        yield return null;
    }

    IEnumerator Win()
    {
        loadO.load("Win");
        Destroy(gameObject);
        yield return null;
    }
}
