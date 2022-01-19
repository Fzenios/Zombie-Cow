using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScr : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D PlayerRb;
    public float MoveSpeed, JumpSpeed;
    public float JumpReset;
    public float JumpResetCur;
    public float DashDmg;
    BoxCollider2D PlayerBox;
    [SerializeField] LayerMask groundLayer, EnemyLayer;
    public KeyCode DashKey;
    public Vector2 DashSpeed;
    public bool isDashing;
    Coroutine Dashmove;
    float Movement;
    public bool EnemyPushed;
    public EnemyBoss1Scr enemyBoss1Scr;
    int Dir;
    public Animator animator;
    public GameObject GunObj;
    int JumpCounter;
    public bool CanMove;
    
    void Start()
    {
        PlayerRb = GetComponent<Rigidbody2D>();
        PlayerBox = GetComponent<BoxCollider2D>();      
        isDashing = false;  
        JumpCounter = 0;
        Dir = 1;
    }
    
    void Update() 
    {
        if(CanMove)
        {
            Movement = Input.GetAxis("Horizontal");
            animator.SetFloat("Movement",  Mathf.Abs(Input.GetAxisRaw("Horizontal")));

            if(Input.GetKeyDown(KeyCode.Space) && !isDashing)
            {
                JumpResetCur = 0;

                if(JumpCounter == 0)
                    animator.SetTrigger("JumpTr"); 

                JumpCounter++;
                
            }
            
            if(Input.GetKey(KeyCode.Space) && !isDashing)
            { 
                if(JumpCounter < 2)
                {
                    
                    if(JumpResetCur <= JumpReset)
                    {
                        PlayerRb.velocity = new Vector2(PlayerRb.velocity.x, JumpSpeed);
                        JumpResetCur += Time.deltaTime;
                    }  
                }
            }
            
            if(!isDashing && !EnemyPushed)
            {
                if(Input.GetKey(DashKey) && Dir == 1)
                    Dashmove = StartCoroutine(DashMove(Dir));
                else if(Input.GetKey(DashKey) && Dir == -1)
                    Dashmove = StartCoroutine(DashMove(Dir));
            }
            
            if(Input.GetKey(KeyCode.D) && !isDashing)
            {
                transform.localScale = new Vector3(1,1,1);
                Dir = 1;
            } 
            
            if(Input.GetKey(KeyCode.A) && !isDashing)
            {
                transform.localScale = new Vector3(-1,1,0);
                Dir = -1;  
            } 
        }

        if(isGrounded() || isGroundedEnemy())
        {
            JumpResetCur = 0;
            JumpCounter = 0;
        }          
        
        if(isGrounded() || isGroundedEnemy() || isDashing)
            animator.SetBool("Jump", false);
        else
            animator.SetBool("Jump", true);
    
        if(isDashing)
            GunObj.SetActive(false);
        else 
            GunObj.SetActive(true); 
    }

    void FixedUpdate() 
    {
        if(CanMove)
        {
            if(!isDashing && !EnemyPushed)
            {
                PlayerRb.velocity = new Vector2(Movement * MoveSpeed, PlayerRb.velocity.y); 
            }
        }
        else
        {
            PlayerRb.velocity  = new Vector2(0,0);
            animator.SetFloat("Movement",  0);
        }
    }

    bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(PlayerBox.bounds.center, PlayerBox.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    bool isGroundedEnemy()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(PlayerBox.bounds.center, PlayerBox.bounds.size, 0, Vector2.down, 0.1f, EnemyLayer);
        return raycastHit.collider != null;
    }
    
    IEnumerator DashMove(float Dir)
    {
        isDashing = true;
        PlayerRb.velocity = new Vector2(0f, 0f);
        if(Dir == 1)
            PlayerRb.AddForce(DashSpeed, ForceMode2D.Impulse);
        else 
            PlayerRb.AddForce(-DashSpeed, ForceMode2D.Impulse);
        PlayerRb.gravityScale = 0;
        animator.SetBool("Charge", true);
        yield return new WaitForSeconds(0.5f);
        PlayerRb.gravityScale = 3;
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Charge", false);
        isDashing = false;
    }
    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.transform.tag == "Enemy" && isDashing)
        {
            if(other.transform.GetComponent<EnemyRangeHealthScr>() != null)
                other.transform.GetComponent<EnemyRangeHealthScr>().TakeDmg(DashDmg, "Melee");
            else
                other.transform.GetComponent<EnemyMeleeHealthScr>().TakeDmg(DashDmg,"Melee");  

            Rigidbody2D EnemyRb = other.transform.GetComponent<Rigidbody2D>(); 
            StartCoroutine(StopPush(EnemyRb));

            StopCoroutine(Dashmove);
            animator.SetBool("Charge", false);
            isDashing = false;
            PlayerRb.gravityScale = 3;
        }  
        if(other.transform.tag == "Boss" && isDashing)     
        {
            StopCoroutine(Dashmove);
            animator.SetBool("Charge", false);

            other.transform.GetComponent<EnemyBoss1Scr>().TakeDmg(DashDmg, "Melee"); 
            Rigidbody2D EnemyRb = other.transform.GetComponent<Rigidbody2D>(); 
            StartCoroutine(StopPush(EnemyRb));
            
            isDashing = false;
            PlayerRb.gravityScale = 3;
        }  
    }
    
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "BossTrig")
        {
            enemyBoss1Scr.FightStart = true;
        }
    }
    IEnumerator StopPush(Rigidbody2D EnemyRb)
    {
        yield return new WaitForSeconds(1);
        EnemyRb.velocity = new Vector2(0f, 0f);
    }
    
}
