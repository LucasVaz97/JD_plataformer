using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AttackScript : MonoBehaviour
{
    int damage=1;
    Player player;
    Animator slashAnimator;
    float direction;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        slashAnimator = GetComponent<Animator>();
    }
    

   void OnTriggerEnter2D(Collider2D collision)
    {
        BloobEnemy enemy = collision.GetComponent<BloobEnemy>();
        if (enemy != null && enemy.isImune==false)
        {
            if (player.transform.position.x > collision.transform.position.x)
            {
                direction = -1;
            }
            else
            {
                direction = 1;
            }
            enemy.TakeDamage(1, direction);
            player.WhenHit();
            slashAnimator.SetTrigger("attack");

        }
        
    }

}
