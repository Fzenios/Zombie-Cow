using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public Animator PlayerAnimator, GameOverPlayerAnimator, GameOverAnimator;
    public GameObject GameOverMenu;
    public static bool AllCanMove;
    public AllData allData;
    bool RestartBool;
    public GameObject[] Checkpoints;
    void Start()
    {
        ChatCounter = allData.LastChatCounter;
        if(allData.LastCheckPointObj != null)
            allData.LastCheckPointObj.GetComponent<BoxCollider2D>().isTrigger = false;

        StartCoroutine(StartGame());
        playerMovementScr.CanMove = false;
        CamAnimator.SetTrigger("Entrance");
        AllCanMove = true;
        RestartBool = false;
        
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
        if(ChatCounter == 0)
            NextChat();
        else
            playerMovementScr.CanMove = true;
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
        IEnumerator AfterbossWait()
        {
            yield return new WaitForSeconds(15);
            TileBlock.SetActive(false);
        }
    }
    
    public void ThrowCredits()
    {
        PlayerAnimator.SetTrigger("HeadBang");
    }
    public void RestartGame()
    {
        if(!RestartBool)
        {
            RestartBool = true;
            GameOverPlayerAnimator.SetTrigger("Restart");
            GameOverAnimator.SetTrigger("Restart");
            
            StartCoroutine(WaitForGameOver());
            IEnumerator WaitForGameOver()
            {
                yield return new WaitForSeconds(2f);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
