using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealthScr : MonoBehaviour
{
    public float HpMax, HpCurrent;
    public float DashDmg;
    public TMP_Text HealthTxt;
    PlayerMovementScr playerMovementScr;
    void Start()
    {
        playerMovementScr = GetComponent<PlayerMovementScr>();
        HpCurrent = HpMax;
    }

    void Update()
    {
        HealthTxt.text = HpCurrent.ToString();
    }
    public void TakeDmg(float Damage)
    {
        HpCurrent -= Damage;
        if(HpCurrent <= 0)
            Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        
    }
    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.collider.tag == "Enemy")
        {
            if(playerMovementScr.isDashing)
                other.transform.GetComponent<EnemyHealthScr>().TakeDmg(DashDmg, "Melee");                    
        }
    }
}
