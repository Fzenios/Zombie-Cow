using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScr : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D PlayerRb;
    public float MoveSpeed, JumpSpeed;
    public float JumpReset;
    float JumpResetCur;
    public float DashDmg;
    BoxCollider2D PlayerBox;
    [SerializeField] LayerMask groundLayer, EnemyLayer;
    public KeyCode DashKey;
    public Vector2 DashSpeed;
    bool CanDash;
    [HideInInspector] public bool isDashing;
    Coroutine Dashmove;
    float Movement;
    [HideInInspector] public bool EnemyPushed;
    int Dir;
    public Animator animator;
    public GameObject GunObj;
    int JumpCounter;
    public bool CanMove;
    public AllData allData;
    
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
        if(EventsScr.AllCanMove)
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
                    transform.localScale = new Vector3(4,4,0);
                    Dir = 1;
                } 
                
                if(Input.GetKey(KeyCode.A) && !isDashing)
                {
                    transform.localScale = new Vector3(-4,4,0);
                    Dir = -1;  
                } 
            }
        }

        if(isGrounded() || isGroundedEnemy())
        {
            JumpResetCur = 0;
            JumpCounter = 0;
            CanDash = true;
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
        if(EventsScr.AllCanMove)
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
    }

    public bool isGrounded()
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
        if(CanDash)
        {
            CanDash = false;
            isDashing = true;
            PlayerRb.velocity = new Vector2(0f, 0f);
            if(Dir == 1)
                PlayerRb.AddForce(DashSpeed, ForceMode2D.Impulse);
            else 
                PlayerRb.AddForce(-DashSpeed, ForceMode2D.Impulse);
            PlayerRb.gravityScale = 0;
            animator.SetBool("Charge", true);
            yield return new WaitForSeconds(0.5f);
            CanDash = false;
            PlayerRb.gravityScale = 3;
            //yield return new WaitForSeconds(0.5f);
            animator.SetBool("Charge", false);
            isDashing = false;
        }
    }
    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.transform.tag == "Enemy" && isDashing)
        {
            if(other.transform.GetComponent<EnemyRangeHealthScr>() != null)
                other.transform.GetComponent<EnemyRangeHealthScr>().TakeDmg(DashDmg, "Melee");
            else if (other.transform.GetComponent<EnemyMeleeHealthScr>() != null)
                other.transform.GetComponent<EnemyMeleeHealthScr>().TakeDmg(DashDmg, "Melee"); 
            else 
                other.transform.GetComponent<EnemyOneHitScr>().TakeDmg(DashDmg, "Melee");             
                
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
    IEnumerator StopPush(Rigidbody2D EnemyRb)
    {
        yield return new WaitForSeconds(1);
        if(EnemyRb != null)
            EnemyRb.velocity = new Vector2(0f, 0f);
    } 
}
