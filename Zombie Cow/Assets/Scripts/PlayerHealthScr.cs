using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerHealthScr : MonoBehaviour
{
    public float HpMax, HpCurrent;
    public float DashDmg;
    public int InvincibleTime; 
    [HideInInspector] public bool Invincible;
    PlayerMovementScr playerMovementScr;
    Rigidbody2D PlayerRb;
    public Vector3 ForceBack;
    public Animator CameraAnimator, PlayerAnimator;
    public SpriteRenderer PlayerSprite;
    public GameObject[] Hearts;
    public GameObject GameOverMenu; 
    
    void Start()
    {
        playerMovementScr = GetComponent<PlayerMovementScr>();
        HpCurrent = HpMax;
        Invincible = false;
        PlayerRb = GetComponent<Rigidbody2D>();
        
        for (int i = 0; i < Hearts.Length; i++)
        {
            Hearts[i].SetActive(true);    
        }
    }

    void Update()
    {
        
    }
    public void TakeDmg(float Damage, int Dir)
    {
        if(!Invincible)
        {
            Invincible = true;

            //HpCurrent -= Damage;
            if(HpCurrent <= 0)
            {
                HpCurrent = 0;
                StartCoroutine(CheckDeath());
            }
            
            for (int i = (int)HpCurrent; i < HpMax; i++)
            {
                Hearts[i].SetActive(false);
            }

            CameraAnimator.SetTrigger("CamTrig");
            PlayerAnimator.SetTrigger("Damage");
            if(Dir == 1)
                PlayerRb.AddForce(ForceBack, ForceMode2D.Impulse);
            else if(Dir == -1)
                PlayerRb.AddForce(-ForceBack, ForceMode2D.Impulse);        
            
            StartCoroutine(EnemyPushed());
            StartCoroutine(InvincibleStat());
        }
    }
    IEnumerator CheckDeath()
    {
        yield return new WaitForSeconds(0.15f);
        if(HpCurrent <= 0)
            {
                EventsScr.AllCanMove = false;
                PlayerAnimator.SetTrigger("Dead");
                yield return new WaitForSeconds(0.5f);
                GameOverMenu.SetActive(true);
                PlayerRb.velocity = Vector2.zero;
            }
    }
    IEnumerator InvincibleStat()
    {
        for (int i = 0; i < InvincibleTime; i++)
        {  
            yield return new WaitForSeconds(0.2f);
            PlayerSprite.color = new Color32(255,255,255,140);   
            yield return new WaitForSeconds(0.2f);
            PlayerSprite.color = new Color32(255,255,255,255);
        }
        Invincible = false;
    }
    IEnumerator EnemyPushed()
    {
        playerMovementScr.EnemyPushed = true;
        yield return new WaitForSeconds(0.5f);
        playerMovementScr.EnemyPushed = false;
    }
    public void GainHP(float Hp)
    {
        HpCurrent += Hp;
        if(HpCurrent > HpMax)
            HpCurrent = HpMax;

        for (int i = 0; i < (int)HpCurrent; i++)
            {
                Hearts[i].SetActive(true);
            }
    }
    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.collider.tag == "Trap")
        {
            TakeDmg(4, 0);                         
        }
    }
}
