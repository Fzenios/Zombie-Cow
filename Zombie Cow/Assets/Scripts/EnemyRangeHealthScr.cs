using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyRangeHealthScr : MonoBehaviour
{
    public float MaxHp, CurrentHp;
    public float EnemyTouchDmg;
    public Slider HealthSlider;
    float Distance;
    Transform PlayerPos;
    int Dir;
    Animator animator;
    BoxCollider2D Collider;
    Rigidbody2D EnemyRb;
    EnemyRangeMovement enemyRangeMovement;
    public EnemyShootScr enemyShootScr;
    bool Invincible;
    PlayerHealthScr playerHealthScr;
    EventsScr eventsScr;   
    [HideInInspector] public bool TookDamage;
    
    void Start()
    {
        CurrentHp = MaxHp;
        HealthSlider.maxValue = MaxHp;
        PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealthScr = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthScr>();
        Collider = GetComponent<BoxCollider2D>();
        EnemyRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemyRangeMovement = GetComponent<EnemyRangeMovement>();
        eventsScr = GameObject.FindGameObjectWithTag("PublicScripts").GetComponent<EventsScr>();
        Invincible = false;
        TookDamage = false;
    }

    void Update()
    {
        HealthSlider.value = CurrentHp;        
    }

    public void TakeDmg(float Damage, string TypeDmg)
    {
        if(TypeDmg == "Range")
        {
            if(!Invincible)
            {
                CurrentHp -= Damage;
                StartCoroutine(GotHit());
            }
            if(CurrentHp <= 0)
            {
                animator.SetTrigger("Death");
                Destroy(gameObject, 5);
                Dying();
                playerHealthScr.GainHP(1);
            }
        }
        if(TypeDmg == "Melee")
        {
            if(!Invincible)
            {
                CurrentHp -= Damage;
                StartCoroutine(GotHit());
            }
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
        enemyRangeMovement.enabled = false;
        enemyShootScr.enabled = false;
        animator.SetBool("Walk", false);
        animator.SetBool("Throw", false);
        EnemyRb.velocity = new Vector2(0f, 0f);
        Collider.enabled = false;
        EnemyRb.gravityScale = 0;
    }
    
    IEnumerator GotHit()
    {
        TookDamage = true;
        Invincible = true;
        yield return new WaitForSeconds(0.2f);
        Invincible = false;
        yield return new WaitForSeconds(4);
        TookDamage = false;
        
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
