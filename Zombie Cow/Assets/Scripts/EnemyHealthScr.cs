using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthScr : MonoBehaviour
{
    public float MaxHp, CurrentHp;
    public float EnemyTouchDmg;
    public Slider HealthSlider;
    float Distance;
    Transform PlayerPos;
    int Dir;
    
    void Start()
    {
        CurrentHp = MaxHp;
        HealthSlider.maxValue = MaxHp;
        PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        HealthSlider.value = CurrentHp;   
        
        Distance = PlayerPos.position.x - transform.position.x;
            if(Distance < 0)
            {
                Dir = 1;
            } 
            else if(Distance > 0)
            {
                Dir = -1;
            }     
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
            other.gameObject.GetComponent<PlayerHealthScr>().TakeDmg(EnemyTouchDmg, Dir);
        }           
    }
}
