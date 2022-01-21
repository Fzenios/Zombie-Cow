using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerHealthScr : MonoBehaviour
{
    public float HpMax, HpCurrent;
    public float DashDmg;
    [HideInInspector] public bool Invincible;
    PlayerMovementScr playerMovementScr;
    Rigidbody2D PlayerRb;
    public Vector3 ForceBack;
    public Animator CameraAnimator, PlayerAnimator;
    public SpriteRenderer PlayerSprite;
    public GameObject[] Hearts;
    
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
        
        //HealthTxt.text = HpCurrent.ToString();
    }
    public void TakeDmg(float Damage, int Dir)
    {
        if(!Invincible)
        {
            Invincible = true;

            HpCurrent -= Damage;
            if(HpCurrent < 0)
                HpCurrent = 0;

            for (int i = (int)HpCurrent; i < HpMax; i++)
            {
                Hearts[i].SetActive(false);
            }

            if(HpCurrent <= 0)
                StartCoroutine(CheckDeath());
                

            CameraAnimator.SetTrigger("CamTrig");

            if(Dir == 1)
                PlayerRb.AddForce(ForceBack, ForceMode2D.Impulse);
            else 
                PlayerRb.AddForce(-ForceBack, ForceMode2D.Impulse);
            
            StartCoroutine(EnemyPushed());
            PlayerAnimator.SetTrigger("Damage");

            StartCoroutine(InvincibleStat());
        }
    }
    IEnumerator CheckDeath()
    {
        yield return new WaitForSeconds(1);
        if(HpCurrent <= 0)
            Destroy(gameObject);
    }
    IEnumerator InvincibleStat()
    {
        PlayerSprite.color = new Color32(255,255,255,140);   
        yield return new WaitForSeconds(0.2f);
        PlayerSprite.color = new Color32(255,255,255,255);   
        yield return new WaitForSeconds(0.2f);
        PlayerSprite.color = new Color32(255,255,255,140);   
        yield return new WaitForSeconds(0.2f);
        PlayerSprite.color = new Color32(255,255,255,255);   
        yield return new WaitForSeconds(0.2f);
        PlayerSprite.color = new Color32(255,255,255,140);   
        yield return new WaitForSeconds(0.2f);
        PlayerSprite.color = new Color32(255,255,255,255); 
        yield return new WaitForSeconds(0.2f);
        PlayerSprite.color = new Color32(255,255,255,140);   
        yield return new WaitForSeconds(0.2f);
        PlayerSprite.color = new Color32(255,255,255,255); 
        yield return new WaitForSeconds(0.2f);
        PlayerSprite.color = new Color32(255,255,255,140);   
        yield return new WaitForSeconds(0.2f);
        PlayerSprite.color = new Color32(255,255,255,255); 
        yield return new WaitForSeconds(0.2f);        
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
            Destroy(gameObject);                             
        }
    }
}
