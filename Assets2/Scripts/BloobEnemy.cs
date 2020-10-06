using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloobEnemy : MonoBehaviour {
    Rigidbody2D bloobRigidbody;
    [SerializeField] int Hp;
    bool isLocked = false;
    public bool isImune;
    float lockedCounter;
    float imuneCounter;
    float imuneTime = 0.3f;
    float turnTime;
    [SerializeField] int velocityMultiplier = 1;
    int layerMask;
    public bool jump;
    bool grounded;

    // Use this for initialization

    void Start()
    {
        layerMask = LayerMask.NameToLayer("Ground");


        bloobRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckWall();
        CheckDown();

        //TurnTime();
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
        isImune = true;
        isLocked = true;
        if (grounded)
        {
            bloobRigidbody.velocity = new Vector2(10f * (direction), Mathf.Abs(direction * 8f));

        }
        else
        {
            bloobRigidbody.velocity = new Vector2(15f * (direction), bloobRigidbody.velocity.y);

        }

        if (Hp <= 0)
        {
            Destroy(gameObject);

        }
    }


    private void TurnTime()
    {
        velocityMultiplier = velocityMultiplier * -1;
        transform.localScale = new Vector2(transform.localScale.x * -1, 1f);

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
        float offset = 0.2f;
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(1 * velocityMultiplier, 0), 1, 1 << layerMask);
        Debug.DrawRay(transform.position, new Vector2(1 * velocityMultiplier, 0), Color.green);
        if (hit.collider != null)
        {
            TurnTime();


        }
    }

    private void CheckDown()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(velocityMultiplier * 0.5f, 0, 0), new Vector2(0, -1), 1, 1 << layerMask);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, new Vector2(0, -0.5f), 1, 1 << layerMask);
        if (hit2.collider != null)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
        Debug.DrawRay(transform.position + new Vector3(velocityMultiplier * 0.5f, 0, 0), new Vector2(0, -1), Color.red);
        if (hit.collider == null && hit2.collider != null)
        {
            TurnTime();

        }

    }

    IEnumerator JumpRoutine()
    {

        bloobRigidbody.AddForce(transform.up * 20, ForceMode2D.Impulse);
        jump = false;
        yield return new WaitForSeconds(3f);
        jump = true;
    }



}
