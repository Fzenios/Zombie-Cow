using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidersScr : MonoBehaviour
{
    public EventsScr eventsScr;
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            eventsScr.NextChat();
            eventsScr.CanMoveFunc();
            Destroy(gameObject, 1);    
        }      
    }
}
