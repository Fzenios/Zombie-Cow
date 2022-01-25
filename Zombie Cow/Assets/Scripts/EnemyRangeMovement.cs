using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeMovement : MonoBehaviour
{
    Transform PlayerPos;
    Rigidbody2D EnemyRb;
    public float EnemySafeDistance, EnemyUnSafeDistance;
    public float EnemySpeed;
    float Distance;
    bool FightStart;
    public float DistanceToActivate;
    float DistanceWithPlayer;
    public float drawline;
    [HideInInspector] public int Dir;
    Animator animator;
    void Start()
    {
        EnemyRb = GetComponent<Rigidbody2D>();
        PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;
        FightStart = false;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(EventsScr.AllCanMove)
        {
            Debug.DrawLine(transform.position, new Vector3(transform.position.x + drawline, transform.position.y, transform.position.z), Color.cyan);
            DistanceWithPlayer = Vector2.Distance(transform.position,PlayerPos.position);
            
            if(DistanceWithPlayer < DistanceToActivate)
                FightStart = true;
            else 
            {
                FightStart = false;
                EnemyRb.velocity = new Vector2(0f, 0f);
            }
        
            if(FightStart)
            {
                if(Vector2.Distance(transform.position,PlayerPos.position) > EnemySafeDistance )
                {
                    animator.SetBool("Walk", true);
                    transform.position = Vector2.MoveTowards(transform.position, PlayerPos.position, Time.deltaTime * EnemySpeed);
                }
                else if (Vector2.Distance(transform.position,PlayerPos.position) < EnemyUnSafeDistance )
                {
                    animator.SetBool("Walk", true);
                    transform.position = Vector2.MoveTowards(transform.position, PlayerPos.position, -Time.deltaTime * EnemySpeed);
                }
                else 
                    animator.SetBool("Walk", false);
                
                Distance = PlayerPos.position.x - transform.position.x;
                if(Distance < 0)
                {
                    transform.localScale = transform.localScale;
                    Dir = 1;
                } 
                else if(Distance > 0)
                {
                    transform.localScale = new Vector3(4,4,0);
                    Dir = -1;
                } 
            }
        }
    }
    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.transform.tag == "Player")
        {
            StartCoroutine(StopMove());
        }       
    }
    void OnTriggerEnter2D(Collider2D other)        
    {   
        if(other.tag == "BossTrig")
        {
            Destroy(gameObject);
        }   
    }
    IEnumerator StopMove()
    {
        yield return new WaitForSeconds(1.5f);
        EnemyRb.velocity = new Vector2(0f, 0f);
    }
}
