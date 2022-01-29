using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkScr : MonoBehaviour
{
    Rigidbody2D MilkRb;
    public float MilkDmg;
    void Start()
    {
        MilkRb = GetComponent<Rigidbody2D>();
        //MilkRb.AddForce(BulletPos.position, ForceMode2D.Impulse);
        Destroy(gameObject, 8);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Enemy")
        {
            if(other.transform.GetComponent<EnemyRangeHealthScr>() != null)
                other.transform.GetComponent<EnemyRangeHealthScr>().TakeDmg(MilkDmg,"Range");
            else if (other.transform.GetComponent<EnemyMeleeHealthScr>() != null)
                other.transform.GetComponent<EnemyMeleeHealthScr>().TakeDmg(MilkDmg,"Range");
            else 
                other.transform.GetComponent<EnemyOneHitScr>().TakeDmg(MilkDmg,"Range");
            Destroy(gameObject);
        }
        
        if(other.tag == "Boss")
        {
            other.GetComponent<EnemyBoss1Scr>().TakeDmg(MilkDmg,"Range");        
            Destroy(gameObject);
        }
        if(other.tag != "Player" && other.tag != "Milk" && other.tag != "Colliders" )
            Destroy(gameObject);
        
    }        
}
