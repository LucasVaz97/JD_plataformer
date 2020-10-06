using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AttackScript : MonoBehaviour
{
    int damage=1;
    Player player;
    Animator slashAnimator;
    float direction;
    Rigidbody2D playerRb;
    public float atkDown;


    private void Start()
    {
        player = FindObjectOfType<Player>();
        slashAnimator = GetComponent<Animator>();
        playerRb = player.GetComponent<Rigidbody2D>();
        atkDown = player.atkDirect;
     
    }
    


    public void GetUp(){
        playerRb.velocity = new Vector2(playerRb.velocity.x, 16);
  
       }


    public float getAtkDirect()
    {
        return player.atkDirect;

    }
  


}
