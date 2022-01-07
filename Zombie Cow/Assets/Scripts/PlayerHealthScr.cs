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
    
    void Start()
    {
        playerMovementScr = GetComponent<PlayerMovementScr>();
        HpCurrent = HpMax;
        Invincible = false;
    }

    void Update()
    {
        HealthTxt.text = HpCurrent.ToString();
    }
    public void TakeDmg(float Damage)
    {
        if(!Invincible)
        {
            HpCurrent -= Damage;
            if(HpCurrent <= 0)
                Destroy(gameObject);

            Invincible = true;
            StartCoroutine(InvincibleStat());
        }
    }
    IEnumerator InvincibleStat()
    {
        yield return new WaitForSeconds(3);
        Invincible = false;
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        
    }
    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.collider.tag == "Enemy")
        {
                             
        }
    }
}
