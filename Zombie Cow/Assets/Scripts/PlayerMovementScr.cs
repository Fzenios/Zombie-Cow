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
    bool CanDash;
    public bool isDashing;
    Coroutine Dashmove;
    float Movement;
    public bool EnemyPushed;
    public EnemyBoss1Scr enemyBoss1Scr;
    int Dir;
    public Animator animator;
    public GameObject GunObj;
    
    void Start()
    {
        PlayerRb = GetComponent<Rigidbody2D>();
        PlayerBox = GetComponent<BoxCollider2D>();      
        isDashing = false;  
    }
    
    void Update() 
    {
        if(!isDashing && !EnemyPushed)
        {
            if(Input.GetKey(DashKey) && Input.GetKey(KeyCode.D))
                Dashmove = StartCoroutine(DashMove(1));
            else if(Input.GetKey(DashKey) && Input.GetKey(KeyCode.A))
                Dashmove = StartCoroutine(DashMove(-1));
        }

        if(isGrounded() || isGroundedEnemy())
        {
            JumpResetCur = 0;
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
        if(isDashing)
            GunObj.SetActive(false);
        else 
            GunObj.SetActive(true); 
    }

    void FixedUpdate() 
    {
           //Movement = Input.GetAxis("Horizontal") * MoveSpeed;
              //  PlayerRb.velocity = new Vector2(Movement, PlayerRb.velocity.y);
                if(!isDashing && !EnemyPushed)
                {
                    PlayerRb.velocity = new Vector2(Input.GetAxis("Horizontal") * MoveSpeed, PlayerRb.velocity.y); 
                    animator.SetFloat("Movement",  Mathf.Abs(Input.GetAxisRaw("Horizontal")));
                }
            
                //PlayerRb.velocity = new Vector2(0f, 0f);
        
        if(Input.GetKey(KeyCode.Space) && !isDashing)
        {
            if(JumpResetCur <= JumpReset)
            {
                PlayerRb.velocity = new Vector2(PlayerRb.velocity.x, JumpSpeed);
                JumpResetCur += Time.deltaTime;
                animator.SetBool("Jump", true);
            }
        }
        if(isGrounded() || isGrounded() || isDashing)
            animator.SetBool("Jump", false);
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
        CanDash = true;
        isDashing = false;
    }
    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.transform.tag == "Enemy" && isDashing)
        {
            StopCoroutine(Dashmove);
            animator.SetBool("Charge", false);

            other.transform.GetComponent<EnemyHealthScr>().TakeDmg(DashDmg, "Melee");  

            CanDash = true;
            isDashing = false;
            PlayerRb.gravityScale = 3;
        }  
        if(other.transform.tag == "Boss" && isDashing)     
        {
            StopCoroutine(Dashmove);
            animator.SetBool("Charge", false);

            other.transform.GetComponent<EnemyBoss1Scr>().TakeDmg(DashDmg, "Melee");  

            CanDash = true;
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
    
}
