using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBoss1Scr : MonoBehaviour
{
    public float MaxHp, CurrentHp;
    public float EnemyTouchDmg, EnemyheavyDmg;
    [HideInInspector] public bool FightStart;
    float Distance , DistanceWithPlayer;
    public float EnemySafeDistance, EnemyUnSafeDistance, DistanceToActivate;
    Rigidbody2D EnemyRb;
    Transform PlayerPos;
    public float EnemySpeed;
    public float drawline;
    public GameObject Projectile;
    public Transform ProjectPos1, ProjectPos2, ProjectPos3;
    bool RangeAttack;
    public Vector3 ProjectForce;
    public Vector3 ChargeForce;
    public Vector3 BackForce;
    public Vector3 JumpAttackSpeed;
    public int Dir;
    bool ChargeAttack, IsCharging;
    bool Attacking;
    int RandomAttack;
    public PlayerMovementScr playerMovementScr;
    public bool JumpAttack;
    public Transform JumpPos;
    public float JumpSpeed;
    bool IsJumping;
    bool CanMove;
    public Slider HealthSlider;
    Animator animator;
    
    
    void Start()
    {
        CurrentHp = MaxHp;
        HealthSlider.maxValue = MaxHp;
        FightStart = false;
        PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;
        EnemyRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        RangeAttack = false;
        JumpAttack = false;   
        IsJumping = false;
        IsCharging = false;   
        CanMove = true;  
    }

    void Update()
    {
        HealthSlider.value = CurrentHp;
        //DistanceWithPlayer = Vector2.Distance(transform.position,PlayerPos.position);

        if(FightStart)
        {
            if(!Attacking)
            {
                RandomAttack = Random.Range(1,4);
                Attacking = true;
                switch (RandomAttack)
                {
                    case 1: StartCoroutine(Rangeattack()); return;
                    case 2: StartCoroutine(Chargeattack()); return;
                    case 3: StartCoroutine(Jumpattack()); return;
                } 
            }
        }
    
        if(FightStart)
        {
            if(CanMove)    
            { 
                if(Vector2.Distance(transform.position,PlayerPos.position) > EnemySafeDistance )
                    transform.position = Vector2.MoveTowards(transform.position, PlayerPos.position, Time.deltaTime * EnemySpeed);
                else if (Vector2.Distance(transform.position,PlayerPos.position) < EnemyUnSafeDistance )
                    transform.position = Vector2.MoveTowards(transform.position, PlayerPos.position, -Time.deltaTime * EnemySpeed);
                
                animator.SetBool("Walk", true);
            }
            else
                animator.SetBool("Walk", false);

            Distance = PlayerPos.position.x - transform.position.x;
            if(Distance < 0)
            {
                transform.localScale = new Vector3(-6f,6,0);
                Dir = 1;
            } 
            else if(Distance > 0)
            {
                transform.localScale = new Vector3(6f,6,0);
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
        if(JumpAttack)
        {
            transform.position = Vector2.MoveTowards(transform.position, JumpPos.position, Time.deltaTime * JumpSpeed);
        }
    }
    IEnumerator Rangeattack()
    {   
        yield return new WaitForSeconds(3);
        CanMove = false;
        RangeAttack = true;
        yield return new WaitForSeconds(3);
        RangeAttack = true;
        yield return new WaitForSeconds(3);
        RangeAttack = true;
        CanMove = true;
        yield return new WaitForSeconds(4);
        Attacking = false;
    }
    IEnumerator Chargeattack()
    {
        yield return new WaitForSeconds(3);
        CanMove = false;
        EnemyRb.velocity = new Vector2(0f, 0f);
        ChargeAttack = true;
        IsCharging = true;
        yield return new WaitForSeconds(3);
        EnemyRb.velocity = new Vector2(0f, 0f);
        ChargeAttack = true;
        IsCharging = true;
        yield return new WaitForSeconds(4);
        CanMove = true;
        EnemyRb.velocity = new Vector2(0f, 0f);
        Attacking = false;
    }
    IEnumerator Jumpattack()
    {
        yield return new WaitForSeconds(3);
        CanMove = false;
        JumpAttack = true;
        IsJumping = false;
        EnemyRb.gravityScale = 0;
        int RandomInt = Random.Range(2, 7);
        yield return new WaitForSeconds(RandomInt);
        JumpAttack = false;
        EnemyRb.gravityScale = 1;
        EnemyRb.AddForce(JumpAttackSpeed, ForceMode2D.Impulse);
        IsJumping = true;
        yield return new WaitForSeconds(1);
        EnemyRb.velocity = new Vector2(0f, 0f);
        JumpAttack = true;
        IsJumping = false;
        EnemyRb.gravityScale = 0;
        RandomInt = Random.Range(2, 4);
        yield return new WaitForSeconds(RandomInt);
        JumpAttack = false;
        EnemyRb.gravityScale = 1;
        EnemyRb.AddForce(JumpAttackSpeed, ForceMode2D.Impulse);
        IsJumping = true;
        yield return new WaitForSeconds(2);
        EnemyRb.velocity = new Vector2(0f, 0f);
        CanMove = true;
        yield return new WaitForSeconds(2);
        IsJumping = false;
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
    
    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.transform.tag == "Player")
        {
            if(IsCharging || IsJumping)
            {   
                IsCharging = false;
                IsJumping = false;
                EnemyRb.velocity = new Vector2(0f, 0f);
                StartCoroutine(SendBack());  
                playerMovementScr.EnemyPushed = true;
                playerMovementScr.PlayerRb.velocity = new Vector2(0f, 0f);
                Rigidbody2D PlayerRb = other.gameObject.GetComponent<Rigidbody2D>();
                if(Dir == 1)
                    PlayerRb.AddForce(BackForce, ForceMode2D.Impulse);
                else
                    PlayerRb.AddForce(-BackForce, ForceMode2D.Impulse);
                other.gameObject.GetComponent<PlayerHealthScr>().TakeDmg(EnemyheavyDmg, Dir);
            }
            other.gameObject.GetComponent<PlayerHealthScr>().TakeDmg(EnemyTouchDmg, Dir);
        }           
    }
    IEnumerator SendBack()
    {
        yield return new WaitForSeconds(1);
        playerMovementScr.EnemyPushed = false;
        playerMovementScr.PlayerRb.velocity = new Vector2(0f, 0f);
    }
}
