using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventsScr : MonoBehaviour
{
    public Transform PlayerPos;
    public GameObject[] ChatBubles;
    public int ChatCounter, CrowdCounter;
    public PlayerMovementScr playerMovementScr;
    public GameObject Level1, Level2, Grid1, Grid2;
    public Animator CamAnimator;
    public EnemyBoss1Scr enemyBoss1Scr;
    public GameObject BossHp;
    public GameObject TileBlock;
    public Animator PlayerAnimator, GameOverPlayerAnimator, GameOverAnimator;
    public static bool AllCanMove;
    public AllData allData;
    bool RestartBool;
    public GameObject[] Checkpoints;
    CreditsScr creditsScr;
    void Start()
    {
        ChatCounter = allData.LastChatCounter;

        StartCoroutine(StartGame());
        playerMovementScr.CanMove = false;
        CamAnimator.SetTrigger("Entrance");
        AllCanMove = true;
        RestartBool = false;
        creditsScr = GetComponent<CreditsScr>();
        
    }
    
    IEnumerator StartGame()
    {
        if(allData.CheckPointCounter == 0)
            PlayerPos.position = new Vector2(-34.7f,-4.6f);
        else
            PlayerPos.position = new Vector2(allData.LastCheckPointPos.x + 2, allData.LastCheckPointPos.y);
        
        if(allData.CheckPointCounter < 5)
        {
            Level1.SetActive(true);
            Grid1.SetActive(true);
            Level2.SetActive(false);
            Grid2.SetActive(false);
        }
        else
        {
            Level1.SetActive(false);
            Grid1.SetActive(false);
            Level2.SetActive(true);
            Grid2.SetActive(true);
        }
        
        if(allData.CheckPointCounter > 1)
        {
            Checkpoints[allData.CheckPointCounter-1].GetComponent<BoxCollider2D>().isTrigger = false;
        }

        yield return new WaitForSeconds(3);

        if(ChatCounter == 0)
        {
            NextChat();
        }
        else
        {
            playerMovementScr.CanMove = true;
        }
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
        allData.CrowdCounter += CrowdCounter;
        for (int i = 0; i < allData.CrowdCounter; i++)
        {
            creditsScr.SpownCrowd();
        }
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
            allData.CrowdCounter += CrowdCounter;
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
