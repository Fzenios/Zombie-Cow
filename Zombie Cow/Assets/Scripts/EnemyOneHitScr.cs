using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyOneHitScr : MonoBehaviour
{
    public float MaxHp, CurrentHp;
    public float EnemyTouchDmg;
    public Slider HealthSlider;
    float Distance;
    Transform PlayerPos;
    int Dir;
    Animator animator;
    BoxCollider2D Collider;
    CircleCollider2D Collider2;
    Rigidbody2D EnemyRb;
    PlayerHealthScr playerHealthScr;
    EventsScr eventsScr;   
    void Start()
    {
        CurrentHp = MaxHp;
        HealthSlider.maxValue = 10;
        PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealthScr = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthScr>();
        Collider = GetComponent<BoxCollider2D>();
        Collider2 = GetComponent<CircleCollider2D>();
        EnemyRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        eventsScr = GameObject.FindGameObjectWithTag("PublicScripts").GetComponent<EventsScr>();
    }
    void Update()
    {
        HealthSlider.value = CurrentHp;      
    }
     
    public void TakeDmg(float Damage, string TypeDmg)
    {
        if(TypeDmg == "Range")
        {
            return;
        }
        else if(TypeDmg == "Melee")
        {
            CurrentHp -= 5;

            if(CurrentHp <= 0)
            { 
                animator.SetTrigger("DeathMetal");
                Dying();
                eventsScr.CrowdCounter++;
                playerHealthScr.GainHP(playerHealthScr.HpMax);
            }
        }    
    }

    void Dying()
    {
        EnemyRb.velocity = new Vector2(0f, 0f);
        Collider.enabled = false;
        Collider2.enabled = false;
        EnemyRb.gravityScale = 0;
    }
    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.transform.tag == "Player")
        {
            Distance = PlayerPos.position.x - transform.position.x;
            if(Distance < 0)
                Dir = 1;
            else if(Distance > 0)
                Dir = -1;
            playerHealthScr.TakeDmg(EnemyTouchDmg, Dir);
        }        
    }   
        
    void OnCollisionStay2D(Collision2D other) 
    {
        if(other.transform.tag == "Player")
        {
            playerHealthScr.TakeDmg(EnemyTouchDmg, Dir);
        }
    }
}
