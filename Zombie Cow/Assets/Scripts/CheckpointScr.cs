using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScr : MonoBehaviour
{
    public AllData allData;
    public EventsScr eventsScr;
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            allData.CheckpointColliders(transform.position, eventsScr.ChatCounter);
            allData.CheckPointCounter++;
            eventsScr.CheckpointPassed();
            gameObject.SetActive(false);       
        }        
    }
}
