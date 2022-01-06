using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScr : MonoBehaviour
{
    bool CanShoot = true;
    public float ShootTimer;
    float ShootTimerCur;
    public float BulletSpeed;
    public KeyCode ShootKey;
    public Transform ShootPos;
    public GameObject Milk;
    public float offset;

    void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if(CanShoot)
        {
            if(ShootTimerCur >= ShootTimer)
            {
                if(Input.GetKey(ShootKey))
                    Shoot();
            }
            else
                ShootTimerCur += Time.deltaTime;
        }
    }
    void Shoot()
    {
        ShootTimerCur = 0;
        GameObject MilkPre =  Instantiate(Milk, ShootPos.position, ShootPos.rotation);
        Rigidbody2D MilkRb = MilkPre.GetComponent<Rigidbody2D>();
        MilkRb.AddForce(ShootPos.right * BulletSpeed, ForceMode2D.Force);
    
    }
}
