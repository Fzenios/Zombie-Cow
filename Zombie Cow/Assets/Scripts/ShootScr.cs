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
    int Dir;
    public Animator animator;
    public PlayerMovementScr playerMovementScr;
    public Camera MainCamera;
    
    void Start() 
    {
        Dir = 1;        
    }

    void Update()
    {   //ScreenToWorldPoint
        //Vector3 difference = Camera.main.WorldToScreenPoint(Input.mousePosition) - transform.position; 
        Vector3 difference = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -MainCamera.transform.position.z)) - transform.position; 
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        
        if(Input.GetKey(KeyCode.D))
            Dir = 1;
        if(Input.GetKey(KeyCode.A))
            Dir = -1;

        if(Dir == 1)
        {
            offset = 0;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset); 
        }
        if(Dir == -1)
        {
            offset = 180;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ - offset); 
        }  
        if(!playerMovementScr.isDashing)
        {
            if(CanShoot)
            {
                if(ShootTimerCur >= ShootTimer)
                {
                    if(Input.GetKey(ShootKey))
                    {
                        Shoot();
                        animator.SetTrigger("Shoot");
                    }
                }
                else
                    ShootTimerCur += Time.deltaTime;
            }
        }
    }
    void Shoot()
    {
        ShootTimerCur = 0;
        GameObject MilkPre =  Instantiate(Milk, ShootPos.position, ShootPos.rotation);
        Rigidbody2D MilkRb = MilkPre.GetComponent<Rigidbody2D>();
        if(Dir == 1)
            MilkRb.AddForce(ShootPos.right * BulletSpeed, ForceMode2D.Force);
        else
            MilkRb.AddForce(-ShootPos.right * BulletSpeed, ForceMode2D.Force);
    
    }
}
