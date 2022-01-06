using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScr : MonoBehaviour
{
    Rigidbody2D PlayerRb;
    public float MoveSpeed, JumpSpeed;
    public float JumpReset;
    public float JumpResetCur;

    BoxCollider2D PlayerBox;
    [SerializeField]
    LayerMask groundLayer;
    public KeyCode DashKey;
    public Vector2 DashSpeed;
    bool CanDash;
    public bool isDashing;
    void Start()
    {
        PlayerRb = GetComponent<Rigidbody2D>();
        PlayerBox = GetComponent<BoxCollider2D>();
        CanDash = true;
    }

    void Update()
    {
        
    }
    void FixedUpdate() 
    {
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            if(!isDashing)
                PlayerRb.velocity = new Vector2(Input.GetAxis("Horizontal") * MoveSpeed, PlayerRb.velocity.y); 
        
        if(Input.GetKey(KeyCode.Space))
        {
            if(!isDashing)
            {
                if(JumpResetCur <= JumpReset)
                {
                    PlayerRb.velocity = new Vector2(PlayerRb.velocity.x, JumpSpeed);
                    JumpResetCur += Time.deltaTime;
                }
            }
        }
        
        if(CanDash)
        {
            if(Input.GetKey(DashKey) && Input.GetKey(KeyCode.D))
                StartCoroutine(DashMove(1));
            else if(Input.GetKey(DashKey) && Input.GetKey(KeyCode.A))
                StartCoroutine(DashMove(-1));
        }

        if(isGrounded())
        {
            JumpResetCur = 0;
        }

    }
    bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(PlayerBox.bounds.center, PlayerBox.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    
    IEnumerator DashMove(float Dir)
    {
        isDashing = true;
        CanDash = false;
        PlayerRb.velocity = new Vector2(PlayerRb.velocity.x, 0f);
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
    
}
