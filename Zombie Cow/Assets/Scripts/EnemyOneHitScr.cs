using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOneHitScr : MonoBehaviour
{
    public float CurrentHp; 
    void Start()
    {
        CurrentHp = 5;        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.transform.tag == "Player")
        {
            Destroy(gameObject);
        }        
    }
}
