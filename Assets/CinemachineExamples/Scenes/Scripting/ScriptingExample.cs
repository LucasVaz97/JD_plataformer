using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class WinFlag : MonoBehaviour
{

    BoxCollider2D winflag;
    int Hp = 50;
    bool isLocked = false;
    public bool isImune;
    float lockedCounter;
    float imuneCounter;
    float imuneTime = 0.3f;
    float turnTime;
    int velocityMultiplier = 1;


    // Use this for initialization
    void Start()
    {
        winflag = GetComponent<BoxCollider2D>();
    }



    // Update is called once per frame
    void Update()
    {
        print("bei");

        //TurnTime();
        //InvulnerableTime();
        //LockedTime(0.3f);




    }
    private void OnTriggerEnter(Collider2D other)
    {

          Debug.Log("entered");
        
    }
    public void Win()
    {
        SceneManager.LoadScene("endgame");
    }









}
