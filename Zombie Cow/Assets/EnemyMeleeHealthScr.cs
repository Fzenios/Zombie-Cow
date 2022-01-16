using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMeleeHealthScr : MonoBehaviour
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
    EnemyMeleeScr enemyMeleeScr;
    bool Invincible;
    
    void Start()
    {
        CurrentHp = MaxHp;
        HealthSlider.maxValue = MaxHp;
        PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;
        Collider = GetComponent<BoxCollider2D>();
        Collider2 = GetComponent<CircleCollider2D>();
        EnemyRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemyMeleeScr = GetComponent<EnemyMeleeScr>();
        Invincible = false;
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
                return;
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
                return;
            }
        }    
    }

    void Dying()
    {
        if(enemyMeleeScr != null)
            enemyMeleeScr.enabled = false;
        animator.SetBool("Walk", false);
        animator.SetBool("Punch", false);
        EnemyRb.velocity = new Vector2(0f, 0f);
        Collider.enabled = false;
        Collider2.enabled = false;
        EnemyRb.gravityScale = 0;
    }
    
    IEnumerator GotHit()
    {
        Invincible = true;
        yield return new WaitForSeconds(0.2f);
        Invincible = false;
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
            other.gameObject.GetComponent<PlayerHealthScr>().TakeDmg(EnemyTouchDmg, Dir);
        }           
    }
    void OnCollisionStay2D(Collision2D other) 
    {
        if(other.transform.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealthScr>().TakeDmg(EnemyTouchDmg, Dir);
        }
    }
}
