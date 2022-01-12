using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealthScr : MonoBehaviour
{
    public float HpMax, HpCurrent;
    public float DashDmg;
    public TMP_Text HealthTxt;
    public bool Invincible;
    PlayerMovementScr playerMovementScr;
    Rigidbody2D PlayerRb;
    public Vector3 ForceBack;
    public Animator animator;
    public SpriteRenderer PlayerSprite;
    
    void Start()
    {
        playerMovementScr = GetComponent<PlayerMovementScr>();
        HpCurrent = HpMax;
        Invincible = false;
        PlayerRb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HealthTxt.text = HpCurrent.ToString();

        /*if(Input.GetKey(KeyCode.D))
            Lookat = 1;
        if(Input.GetKey(KeyCode.A))
            Lookat = -1;   */
    }
    public void TakeDmg(float Damage, int Dir)
    {
        if(!Invincible)
        {
            Invincible = true;

            HpCurrent -= Damage;
            
            if(HpCurrent <= 0)
                Destroy(gameObject);

            animator.SetTrigger("CamTrig");

            if(Dir == 1)
                PlayerRb.AddForce(ForceBack, ForceMode2D.Impulse);
            else 
                PlayerRb.AddForce(-ForceBack, ForceMode2D.Impulse);
            
            StartCoroutine(EnemyPushed());

            StartCoroutine(InvincibleStat());
        }
    }
    IEnumerator InvincibleStat()
    {
        PlayerSprite.color = Color.
        yield return new WaitForSeconds(2);
        Invincible = false;
    }
    IEnumerator EnemyPushed()
    {
        playerMovementScr.EnemyPushed = true;
        yield return new WaitForSeconds(0.5f);
        playerMovementScr.EnemyPushed = false;
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        
    }
    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.collider.tag == "Trap")
        {
            Destroy(gameObject);                             
        }
    }
}
