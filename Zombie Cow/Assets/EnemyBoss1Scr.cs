using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss1Scr : MonoBehaviour
{
    public float MaxHp, CurrentHp;
    public float EnemyTouchDmg;
    bool FightStart;
    float Distance , DistanceWithPlayer;
    public float EnemySafeDistance, EnemyUnSafeDistance, DistanceToActivate;
    Rigidbody2D EnemyRb;
    Transform PlayerPos;
    public float EnemySpeed;
    public float drawline;
    public GameObject Projectile;
    public Transform ProjectPos1, ProjectPos2, ProjectPos3;
    public bool RangeAttack;
    public Vector3 ProjectForce;
    public Vector3 ChargeForce;
    public Vector3 BackForce;
    public int Dir;
    public bool ChargeAttack;
    bool Attacking;
    int RandomAttack;
    public PlayerMovementScr playerMovementScr;
    
    void Start()
    {
        CurrentHp = MaxHp;
        FightStart = false;
        PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;
        EnemyRb = GetComponent<Rigidbody2D>();
        RangeAttack = false;
        
    }

    void Update()
    {
        DistanceWithPlayer = Vector2.Distance(transform.position,PlayerPos.position);
        if(DistanceWithPlayer < DistanceToActivate)
            FightStart = true;

        if(!Attacking)
        {
            RandomAttack = Random.Range(1,3);
            Attacking = true;
            switch (RandomAttack)
            {
                case 1: StartCoroutine(Rangeattack()); return;
                case 2: StartCoroutine(Chargeattack()); return;
                //case 3: StartCoroutine(Rangeattack()); return;
            }
            
        }
    
        if(FightStart)
        {
            
                if(Vector2.Distance(transform.position,PlayerPos.position) > EnemySafeDistance )
                    transform.position = Vector2.MoveTowards(transform.position, PlayerPos.position, Time.deltaTime * EnemySpeed);
                else if (Vector2.Distance(transform.position,PlayerPos.position) < EnemyUnSafeDistance )
                    transform.position = Vector2.MoveTowards(transform.position, PlayerPos.position, -Time.deltaTime * EnemySpeed);
            

            Distance = PlayerPos.position.x - transform.position.x;
            if(Distance < 0)
            {
                transform.localScale = new Vector3(2.5f,4,0);
                Dir = 1;
            } 
            else if(Distance > 0)
            {
                transform.localScale = new Vector3(-2.5f,4,0);
                Dir = -1;  
            } 
        } 
        if(RangeAttack)
        {   
            RangeAttack = false;
            GameObject Project1 = Instantiate(Projectile, ProjectPos1.position, ProjectPos1.rotation);
            GameObject Project2 = Instantiate(Projectile, ProjectPos2.position, ProjectPos2.rotation);
            GameObject Project3 = Instantiate(Projectile, ProjectPos3.position, ProjectPos3.rotation);
            Rigidbody2D Project1Rb = Project1.GetComponent<Rigidbody2D>();
            Rigidbody2D Project2Rb = Project2.GetComponent<Rigidbody2D>();
            Rigidbody2D Project3Rb = Project3.GetComponent<Rigidbody2D>();
            if(Dir == 1)
            {
                Project1Rb.AddForce(ProjectForce, ForceMode2D.Impulse);
                Project2Rb.AddForce(ProjectForce, ForceMode2D.Impulse);
                Project3Rb.AddForce(ProjectForce, ForceMode2D.Impulse);
            }
            else
            {
                Project1Rb.AddForce(-ProjectForce, ForceMode2D.Impulse);
                Project2Rb.AddForce(-ProjectForce, ForceMode2D.Impulse);
                Project3Rb.AddForce(-ProjectForce, ForceMode2D.Impulse);
            }
        }
        if(ChargeAttack)
        {
            ChargeAttack = false;
            if(Dir == 1)
                EnemyRb.AddForce(ChargeForce, ForceMode2D.Impulse);
            else
                EnemyRb.AddForce(-ChargeForce, ForceMode2D.Impulse);
        }
    }
    IEnumerator Rangeattack()
    {
        yield return new WaitForSeconds(4);
        RangeAttack = true;
        yield return new WaitForSeconds(3);
        RangeAttack = true;
        yield return new WaitForSeconds(3);
        RangeAttack = true;
        yield return new WaitForSeconds(4);
        Attacking = false;
    }
    IEnumerator Chargeattack()
    {
        yield return new WaitForSeconds(4);
        EnemyRb.velocity = new Vector2(0f, 0f);
        ChargeAttack = true;
        yield return new WaitForSeconds(3);
        EnemyRb.velocity = new Vector2(0f, 0f);
        ChargeAttack = true;
        yield return new WaitForSeconds(4);
        EnemyRb.velocity = new Vector2(0f, 0f);
        Attacking = false;
    }

    public void TakeDmg(float Damage, string TypeDmg)
    {
        
            if(TypeDmg == "Range")
                CurrentHp -= Damage;
            if(TypeDmg == "Melee")
                CurrentHp -= Damage;
        
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
            other.gameObject.GetComponent<PlayerHealthScr>().TakeDmg(EnemyTouchDmg);
            if(RandomAttack == 2)
            {   
                EnemyRb.velocity = new Vector2(0f, 0f);
                StartCoroutine(SendBack());  
                playerMovementScr.EnemyPushed = true;
                playerMovementScr.PlayerRb.velocity = new Vector2(0f, 0f);
                Rigidbody2D PlayerRb = other.gameObject.GetComponent<Rigidbody2D>();
                if(Dir == 1)
                    PlayerRb.AddForce(BackForce, ForceMode2D.Impulse);
                else
                    PlayerRb.AddForce(-BackForce, ForceMode2D.Impulse);
            }
        }           
    }
    IEnumerator SendBack()
    {
        yield return new WaitForSeconds(2);
        playerMovementScr.EnemyPushed = false;
        playerMovementScr.PlayerRb.velocity = new Vector2(0f, 0f);

    }
}
