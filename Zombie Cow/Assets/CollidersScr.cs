using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidersScr : MonoBehaviour
{
    public EventsScr eventsScr;
    Rigidbody2D PlayerRb;
    PlayerMovementScr playerMovementScr;
    void Start() 
    {
        PlayerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>(); 
        playerMovementScr = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementScr>();  
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            eventsScr.NextChat();
            eventsScr.CanMoveFunc();
            Destroy(gameObject, 3);    
            StartCoroutine(GravityFix());  
        }      
    }
    IEnumerator GravityFix()
    {   
        PlayerRb.gravityScale = 40;
        if(playerMovementScr.isDashing)
            {
                yield return new WaitForSeconds(1);
                PlayerRb.gravityScale = 40;
            }       
        
        yield return new WaitUntil(playerMovementScr.isGrounded);
        PlayerRb.gravityScale = 3;
    }
}
