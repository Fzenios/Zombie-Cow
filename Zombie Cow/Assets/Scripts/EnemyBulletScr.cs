using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScr : MonoBehaviour
{
    Rigidbody2D BulletRb;
    [HideInInspector] public float BulletDmg;
    [HideInInspector] public int Dir;
    void Start()
    {
        BulletRb = GetComponent<Rigidbody2D>();
        //MilkRb.AddForce(BulletPos.position, ForceMode2D.Impulse);
        Destroy(gameObject, 8);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerHealthScr>().TakeDmg(BulletDmg, Dir);        
            Destroy(gameObject);
            return;
        }
        if(other.tag != "Enemy" && other.tag != "Milk"  )
            Destroy(gameObject);
        
    }
}
