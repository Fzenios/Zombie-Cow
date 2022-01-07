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
    bool IsCharging;
    Rigidbody2D EnemyRb;
    public float ChargeDmg;
    void Start()
    {
        IsCharging = false;
        FightStart = false;
        PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;
        EnemyRb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Debug.DrawLine(transform.position, new Vector3(transform.position.x + drawline, transform.position.y, transform.position.z), Color.cyan);
        DistanceWithPlayer = Vector2.Distance(transform.position,PlayerPos.position);
        if(DistanceWithPlayer < DistanceToActivate)
            FightStart = true;
        
        if(FightStart)
        {
            if(Vector2.Distance(transform.position,PlayerPos.position) > EnemySafeDistance )
                transform.position = Vector2.MoveTowards(transform.position, PlayerPos.position, Time.deltaTime * EnemySpeed);
            else if (Vector2.Distance(transform.position,PlayerPos.position) < EnemyUnSafeDistance )
                transform.position = Vector2.MoveTowards(transform.position, PlayerPos.position, -Time.deltaTime * EnemySpeed);

            if(!IsCharging)
            {
                int RandomInt = Random.Range(0,500);
                if(RandomInt == 0)
                {
                    IsCharging = true;
                    StartCoroutine(Charge());

                }
            }

            Distance = PlayerPos.position.x - transform.position.x;
            if(Distance < 0)
                transform.localScale = new Vector3(1,1.5f,0); 
            else if(Distance > 0)
                transform.localScale = new Vector3(-1,1.5f,0);   
        }        
    }
    IEnumerator Charge()
    {
        EnemyRb.velocity = new Vector2(0f, 0f);
        if(Distance < 0)
            EnemyRb.AddForce(-DashSpeed, ForceMode2D.Impulse);
        else if(Distance > 0)
            EnemyRb.AddForce(DashSpeed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(2);
        EnemyRb.velocity = new Vector2(0f, 0f);
        yield return new WaitForSeconds(5);
        IsCharging = false;
    }
    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.transform.tag == "Player" && IsCharging)
        {
            other.gameObject.GetComponent<PlayerHealthScr>().TakeDmg(ChargeDmg);
        }           
    }
}
