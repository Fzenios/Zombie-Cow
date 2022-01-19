using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyguardScr : MonoBehaviour
{
    public EventsScr eventsScr;
    public Collider2D DoorCollider;
    public Collider2D GuardCollider;
    /*void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.collider.tag == "Milk")
        {
            eventsScr.CanMoveFunc();
            eventsScr.NextChat();
            DoorCollider.isTrigger = true;
        }
    }*/
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Milk")
        {
            eventsScr.CanMoveFunc();
            eventsScr.NextChat();
            DoorCollider.isTrigger = true;
            GuardCollider.enabled = false;
        }
        
    }
}
