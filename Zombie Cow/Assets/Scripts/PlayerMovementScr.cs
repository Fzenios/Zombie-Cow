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
    
    void Start()
    {
        PlayerRb = GetComponent<Rigidbody2D>();
        PlayerBox = GetComponent<BoxCollider2D>();
        CanDash = true;
        
    }
    
    void Update() 
    {
        if(CanDash && !EnemyPushed)
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
        if(Input.GetKey(KeyCode.D))
        {
            transform.localScale = new Vector3(1,1,1);
            Dir = 1;
        } 
        if(Input.GetKey(KeyCode.A))
        {
            transform.localScale = new Vector3(-1,1,0);
            Dir = -1;  
        }      
    }

    void FixedUpdate() 
    {
           //Movement = Input.GetAxis("Horizontal") * MoveSpeed;
              //  PlayerRb.velocity = new Vector2(Movement, PlayerRb.velocity.y);
                if(!isDashing && !EnemyPushed)
                {
                    PlayerRb.velocity = new Vector2(Input.GetAxis("Horizontal") * MoveSpeed, PlayerRb.velocity.y); 
                }
            
                //PlayerRb.velocity = new Vector2(0f, 0f);
        
        if(Input.GetKey(KeyCode.Space) && !isDashing)
        {
            if(JumpResetCur <= JumpReset)
            {
                PlayerRb.velocity = new Vector2(PlayerRb.velocity.x, JumpSpeed);
                JumpResetCur += Time.deltaTime;
            }
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
        CanDash = false;
        PlayerRb.velocity = new Vector2(0f, 0f);
        if(Dir == 1)
            PlayerRb.AddForce(DashSpeed, ForceMode2D.Impulse);
        else 
            PlayerRb.AddForce(-DashSpeed, ForceMode2D.Impulse);
        PlayerRb.gravityScale = 0;
        yield return new WaitForSeconds(0.5f);
        PlayerRb.gravityScale = 3;
        yield return new WaitForSeconds(0.5f);
        CanDash = true;
        isDashing = false;
    }
    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.transform.tag == "Enemy" && isDashing)
        {
            StopCoroutine(Dashmove);

            other.transform.GetComponent<EnemyHealthScr>().TakeDmg(DashDmg, "Melee");  

            CanDash = true;
            isDashing = false;
            PlayerRb.gravityScale = 3;
        }  
        if(other.transform.tag == "Boss" && isDashing)     
        {
            StopCoroutine(Dashmove);

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
