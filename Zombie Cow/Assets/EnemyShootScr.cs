using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootScr : MonoBehaviour
{
    public float EnemyShootTimer;
    float EnemyShootTimerCur;
    public float BulletSpeed;
    public Transform ShootPos;
    public GameObject EnemyBullet;
    public Transform Player;
    public float Distance;
    public float EnemyDamage;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 difference = Player.position - transform.position;
        Distance = Player.position.x - transform.position.x;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ );

        if(EnemyShootTimerCur >= EnemyShootTimer)
            {
                if(Distance > -15 && Distance < 15)
                    Shoot();
            }
        else
            EnemyShootTimerCur += Time.deltaTime;
    }

    void Shoot()
    {
        EnemyShootTimerCur = 0;
        GameObject BulletPre =  Instantiate(EnemyBullet, ShootPos.position, ShootPos.rotation);
        BulletPre.GetComponent<EnemyBulletScr>().BulletDmg = EnemyDamage;
        Rigidbody2D BulletRb = BulletPre.GetComponent<Rigidbody2D>();
        BulletRb.AddForce(ShootPos.right * BulletSpeed, ForceMode2D.Force);
    }
}
