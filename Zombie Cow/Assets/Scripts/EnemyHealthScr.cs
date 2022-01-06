using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthScr : MonoBehaviour
{
    public float MaxHp, CurrentHp;
    public float EnemyDmg;
    void Start()
    {
        CurrentHp = MaxHp;
    }

    void Update()
    {
        
    }

    public void TakeDmg(float Damage, string TypeDmg)
    {
        if(TypeDmg == "Range")
            CurrentHp -= Damage;
        if(TypeDmg == "Melee")
        {
            CurrentHp -= Damage;
        }
        
        if(CurrentHp <= 0)
            Destroy(gameObject);
    }
    
    void OnTriggerEnter2D(Collider2D other) 
    {

    }
    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.transform.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealthScr>().TakeDmg(EnemyDmg);
        }           
    }
}
