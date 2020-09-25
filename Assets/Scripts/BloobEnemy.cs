using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloobEnemy : MonoBehaviour {

    Rigidbody2D bloobRigidbody;
    int Hp=50;
    bool isLocked=false;
    public bool isImune;
    float lockedCounter;
    float imuneCounter;
    float imuneTime=0.3f;
    float turnTime;
    int velocityMultiplier = 1;


    // Use this for initialization
    void Start () {
        bloobRigidbody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {

        TurnTime();
        InvulnerableTime();
        LockedTime(0.3f);



        if (isLocked == true){
            return;
        }

        bloobRigidbody.velocity = new Vector2(0f, 0f);
	
	}



    public void TakeDamage(float damage,float direction)
    {
        Hp = Hp - 1;
        isImune = true;
        isLocked = true;
        print("daaamaaageee");
        print(Hp);
        bloobRigidbody.velocity = new Vector2(16f*(direction), 8f);
        if (Hp <= 0)
        {
            Destroy(gameObject);
            
        }
    }


    private void TurnTime()
    {
        turnTime = turnTime + Time.deltaTime;

        if (turnTime > 4f)
        {
            turnTime = 0;
            velocityMultiplier = velocityMultiplier * -1;
            transform.localScale = new Vector2(transform.localScale.x*-1, 1f);

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







}
