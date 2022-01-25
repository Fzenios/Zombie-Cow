using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeScr : MonoBehaviour
{
    Transform PlayerPos;
    public float EnemySafeDistance, EnemyUnSafeDistance, DistanceToActivate;
    public float EnemySpeed;
    float Distance , DistanceWithPlayer;
    public Vector2 DashSpeed;
    bool FightStart;
    public float drawline;
    bool IsCharging, CanCharge;
    Rigidbody2D EnemyRb;
    public float ChargeDmg, PunchDmg;
    int Dir;
    Animator animator;
    void Start()
    {
        IsCharging = false;
        FightStart = false;
        CanCharge = true;
        PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;
        EnemyRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Dir = 1;
    }
    void Update()
    {
        if(EventsScr.AllCanMove)
        {
            Debug.DrawLine(transform.position, new Vector3(transform.position.x + drawline, transform.position.y, transform.position.z), Color.cyan);
            DistanceWithPlayer = Vector2.Distance(transform.position,PlayerPos.position);
            if(DistanceWithPlayer < DistanceToActivate)
            {
                FightStart = true;
            }
            else
            {
                FightStart = false;
                EnemyRb.velocity = new Vector2(0f, 0f);
                animator.SetBool("Walk", false);
            }
            if(FightStart)
            {
                
                if(Vector2.Distance(transform.position,PlayerPos.position) > EnemySafeDistance )
                    transform.position = Vector2.MoveTowards(transform.position, PlayerPos.position, Time.deltaTime * EnemySpeed);
                else if (Vector2.Distance(transform.position,PlayerPos.position) < EnemyUnSafeDistance )
                    transform.position = Vector2.MoveTowards(transform.position, PlayerPos.position, -Time.deltaTime * EnemySpeed);
                
                if(Vector2.Distance(transform.position,PlayerPos.position) >= 0.8 && !IsCharging)
                    animator.SetBool("Walk", true);
                else
                    animator.SetBool("Walk", false);

                if(Vector2.Distance(transform.position,PlayerPos.position) < 0.8f && !IsCharging)
                    animator.SetBool("Punch", true);
                else
                    animator.SetBool("Punch", false);

                if(CanCharge)
                {
                    int RandomInt = Random.Range(0,500);
                    if(RandomInt == 0)
                    {
                        IsCharging = true;
                        CanCharge = false;
                        animator.SetBool("Walk", false);
                        animator.SetTrigger("Charge");
                        StartCoroutine(Charge());
                    }
                }

                Distance = PlayerPos.position.x - transform.position.x;
                if(Distance < 0)
                {
                    transform.localScale = new Vector3(-4,4f,0); 
                    Dir = 1;
                }
                else if(Distance > 0)
                {
                    transform.localScale = new Vector3(4,4f,0);
                    Dir = -1;   
                }
            } 
        }       
    }
    IEnumerator Charge()
    {
        EnemyRb.velocity = new Vector2(0f, 0f);
        if(Distance < 0)
            EnemyRb.AddForce(-DashSpeed, ForceMode2D.Impulse);
        else if(Distance > 0)
            EnemyRb.AddForce(DashSpeed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(1.5f);
        EnemyRb.velocity = new Vector2(0f, 0f);
        IsCharging = false;
        yield return new WaitForSeconds(5);
        CanCharge = true;
        
    }
    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.transform.tag == "Player" && IsCharging)
        {
            other.gameObject.GetComponent<PlayerHealthScr>().TakeDmg(ChargeDmg, Dir);
        }    
        if(other.transform.tag == "Player")
        {
            StartCoroutine(StopMove());
        }       
    }
    void OnCollisionStay2D(Collision2D other) 
    {
        if(other.transform.tag == "Player" && IsCharging)
        {
            other.gameObject.GetComponent<PlayerHealthScr>().TakeDmg(ChargeDmg, Dir);
        }    
        if(other.transform.tag == "Player")
        {
            StartCoroutine(StopMove());
        }  
    }
    void OnTriggerEnter2D(Collider2D other)        
    {   
        if(other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealthScr>().TakeDmg(PunchDmg, Dir);
           // StartCoroutine(StopMove());
        }   
        if(other.tag == "BossTrig")
        {
            Destroy(gameObject);
        }   
    }
    void OnTriggerStay2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealthScr>().TakeDmg(PunchDmg, Dir);
            //StartCoroutine(StopMove());
        }       
    }
    IEnumerator StopMove()
    {
        yield return new WaitForSeconds(1.5f);
        EnemyRb.velocity = new Vector2(0f, 0f);
    }
}
