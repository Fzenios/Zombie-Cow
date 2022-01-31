using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkScr : MonoBehaviour
{
    Rigidbody2D MilkRb;
    public float MilkDmg;
    public GameObject MilkSpill;
    void Start()
    {
        MilkRb = GetComponent<Rigidbody2D>();
        //MilkRb.AddForce(BulletPos.position, ForceMode2D.Impulse);
        Destroy(gameObject, 8);
    }
    void Update() 
    {
        float angle = Mathf.Atan2(MilkRb.velocity.y, MilkRb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Enemy")
        {
            SpillTheMilk();
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
            SpillTheMilk();
            other.GetComponent<EnemyBoss1Scr>().TakeDmg(MilkDmg,"Range");        
            Destroy(gameObject);
        }
        if(other.tag != "Player" && other.tag != "Milk" && other.tag != "Colliders" )
            Destroy(gameObject);
        
    }   
    void SpillTheMilk()
    {
        GameObject MilkSpillObj = Instantiate(MilkSpill, transform.position, transform.rotation);
        Destroy(MilkSpillObj, 2);
    }     
}
