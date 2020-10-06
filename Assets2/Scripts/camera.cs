using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Cinemachine.Examples
{
    public class camera : MonoBehaviour
    {
        CinemachineVirtualCamera vcam;
        Player player;
        Rigidbody2D playerRigidbody;
   




        // Use this for initialization
        void Start()
        {
            player = FindObjectOfType<Player>();
            playerRigidbody = player.GetComponent<Rigidbody2D>();
            vcam = GetComponent<CinemachineVirtualCamera>();
            var composer = vcam.GetCinemachineComponent<CinemachineFramingTransposer>();
            //composer.m_LookaheadTime = 1;
            
            ;

            


        }

        // Update is called once per frame
        void Update()
        {
            
            Transition();
            //Debug.Log(playerRigidbody.velocity.y);
            
            



        }


        void Transition()
        {
           
            if (playerRigidbody.velocity.y > 0)
            {
                vcam.m_Priority = 10;
                //Debug.Log("camera1");
                return;
            }
            if (playerRigidbody.velocity.y <= -22)
            {
                vcam.m_Priority = 5;
                //Debug.Log("camera2");

            }
           


     
        }
    }

}
