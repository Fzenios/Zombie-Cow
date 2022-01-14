using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOneHitScr : MonoBehaviour
{
    EnemyMeleeHealthScr enemyMeleeHealthScr;
    void Start()
    {
        enemyMeleeHealthScr = GetComponent<EnemyMeleeHealthScr>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.transform.tag == "Player")
        {
            enemyMeleeHealthScr.TakeDmg(100000, "Melee");            
        }        
    }
}
