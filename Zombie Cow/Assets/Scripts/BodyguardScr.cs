using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyguardScr : MonoBehaviour
{
    public EventsScr eventsScr;
    public Collider2D DoorCollider;
    public Collider2D GuardCollider;
    public Transform PlayerPos;
    SpriteRenderer spriteRenderer;
    void Start() 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();        
    } 

    void Update() 
    {
        float Distance = transform.position.x - PlayerPos.position.x;
        if(Distance < 20)
        {
            if(Distance < 0 )
                spriteRenderer.flipX = false;
            else 
                spriteRenderer.flipX = true;
        }      
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Milk" && eventsScr.ChatCounter > 5)
        {
            eventsScr.CanMoveFunc();
            eventsScr.NextChat();
            DoorCollider.isTrigger = true;
            GuardCollider.enabled = false;
        }
        
    }
}
