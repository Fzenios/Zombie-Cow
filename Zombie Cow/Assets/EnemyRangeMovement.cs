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
    void Start()
    {
        EnemyRb = GetComponent<Rigidbody2D>();
        PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;
        FightStart = false;
    }

    void Update()
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
                transform.position = Vector2.MoveTowards(transform.position, PlayerPos.position, Time.deltaTime * EnemySpeed);
            else if (Vector2.Distance(transform.position,PlayerPos.position) < EnemyUnSafeDistance )
                transform.position = Vector2.MoveTowards(transform.position, PlayerPos.position, -Time.deltaTime * EnemySpeed);
            
            Distance = PlayerPos.position.x - transform.position.x;
            if(Distance < 0)
                transform.localScale = new Vector3(1,1.5f,0); 
            else if(Distance > 0)
                transform.localScale = new Vector3(-1,1.5f,0); 
        }
    }
}
