using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsScr : MonoBehaviour
{
    public Transform PlayerPos, BodyguardPos;
    public SpriteRenderer spriteRenderer;
    public GameObject[] ChatBubles;
    public int ChatCounter;
    public PlayerMovementScr playerMovementScr;
    public GameObject Level1, Level2, Grid1, Grid2;
    public Animator CamAnimator;
    public EnemyBoss1Scr enemyBoss1Scr;
    public GameObject BossHp;
    public GameObject TileBlock;
    void Start()
    {
        ChatCounter = 0;
        StartCoroutine(StartGame());
        playerMovementScr.CanMove = false;
        CamAnimator.SetTrigger("Entrance");
    }

    void Update()
    {
        float Distance = BodyguardPos.position.x - PlayerPos.position.x;
        if(Distance < 20)
        {
            if(Distance < 0 )
                spriteRenderer.flipX = false;
            else 
                spriteRenderer.flipX = true;
        }        
    }
    
    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(3);
        NextChat();
    }
    
    public void NextChat()
    {
        ChatBubles[ChatCounter].SetActive(true);
        
    }
    public void CloseChat()
    {
        ChatBubles[ChatCounter].SetActive(false);
        ChatCounter++;
    }
    public void CanMoveFunc()
    {
        playerMovementScr.CanMove =! playerMovementScr.CanMove;
    }
    public void ChangeMap()
    {
        Level1.SetActive(false); 
        Grid1.SetActive(false);
        Grid2.SetActive(true);
        Level2.SetActive(true);
        PlayerPos.position = new Vector3(-109,7.34f,0);
        CamAnimator.SetTrigger("Entrance");
    }
    public void BossFight()
    {
        enemyBoss1Scr.LoadHp = true;
        BossHp.SetActive(true);
    }
    public void AfterBoss()
    {
        StartCoroutine(AfterbossWait());
    }
    IEnumerator AfterbossWait()
    {
        yield return new WaitForSeconds(15);
        TileBlock.SetActive(false);
    }
}
