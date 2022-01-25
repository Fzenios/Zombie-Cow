using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveEntrance : MonoBehaviour
{
    public EventsScr eventsScr;

    void OnTriggerEnter2D(Collider2D other) 
    {
        eventsScr.ChangeMap();
        Destroy(gameObject, 1);        
    }

}
