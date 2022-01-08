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
            other.GetComponent<EnemyHealthScr>().TakeDmg(MilkDmg,"Range");        
            Destroy(gameObject);
            return;
        }
        if(other.tag == "Boss")
        {
            other.GetComponent<EnemyBoss1Scr>().TakeDmg(MilkDmg,"Range");        
            Destroy(gameObject);
            return;
        }
        if(other.tag != "Player" && other.tag != "Milk"  )
            Destroy(gameObject);
        
    }
}
