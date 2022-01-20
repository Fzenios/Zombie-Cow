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
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.transform.tag == "Player")
        {
            if(other.gameObject.GetComponent<PlayerMovementScr>().isDashing)
                enemyMeleeHealthScr.TakeDmg(100000, "Melee");            
        }        
    }
}
