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
    Transform Player;
    public float Distance;
    public float EnemyDamage;
    public EnemyRangeMovement enemyRangeMovement;
    public Animator animator;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if(EventsScr.AllCanMove)
        {
            Vector3 difference = Player.position - transform.position;
            //Distance = Player.position.x - transform.position.x;
            Distance = Vector2.Distance(transform.position,Player.position);
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ );

            if(EnemyShootTimerCur >= EnemyShootTimer)
                {
                    if(Distance <= enemyRangeMovement.DistanceToActivate)
                    {
                        animator.SetTrigger("Throw");
                        Shoot();
                    }
                }
            else
                EnemyShootTimerCur += Time.deltaTime;
        }
    }

    void Shoot()
    {
        EnemyShootTimerCur = 0;
        GameObject BulletPre =  Instantiate(EnemyBullet, ShootPos.position, ShootPos.rotation);
        BulletPre.GetComponent<EnemyBulletScr>().BulletDmg = EnemyDamage;
        BulletPre.GetComponent<EnemyBulletScr>().Dir = enemyRangeMovement.Dir;
        Rigidbody2D BulletRb = BulletPre.GetComponent<Rigidbody2D>();
        BulletRb.AddForce(ShootPos.right * BulletSpeed, ForceMode2D.Force);
    }
}
