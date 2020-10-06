using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class wiin : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        //TurnTime();
        //InvulnerableTime();
        //LockedTime(0.3f);
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Start");
        }



    }




}
