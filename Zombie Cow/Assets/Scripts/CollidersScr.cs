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
            eventsScr.CollidersCount++;
            eventsScr.NextChat();
            eventsScr.CanMoveFunc();
            Destroy(gameObject,1.5f);   
            gameObject.GetComponent<BoxCollider2D>().enabled = false; 
            StartCoroutine(GravityFix());  
        }      
    }
    public IEnumerator GravityFix()
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
