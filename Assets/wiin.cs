using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class wiin : MonoBehaviour
{
    BoxCollider2D winflag;
    // Use this for initialization
    void Start()
    {
        winflag = GetComponent<BoxCollider2D>();
    }



    // Update is called once per frame
    void Update()
    {
        //TurnTime();
        //InvulnerableTime();
        //LockedTime(0.3f);




    }


    void OnTriggerEnter2D(Collider2D other)
    {
        print("benis");
        Win();
    }

    public void Win()
    {
        SceneManager.LoadScene("endgame");
    }




}
