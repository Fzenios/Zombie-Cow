using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScr : MonoBehaviour
{
    public EventsScr eventsScr;
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            eventsScr.CheckpointColliders(transform.position);
            Destroy(gameObject,1);
        }        
    }
}
