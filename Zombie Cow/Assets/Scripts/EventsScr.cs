using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EventsScr : MonoBehaviour
{
    public Transform PlayerPos, BandPos;
    public GameObject[] ChatBubbles, BubblesColliders;
    public int ChatCounter, CrowdCounter, CollidersCount;
    public PlayerMovementScr playerMovementScr;
    public ShootScr shootScr;
    public GameObject Level1, Level2, Grid1, Grid2;
    public Animator CamAnimator;
    public EnemyBoss1Scr enemyBoss1Scr;
    public GameObject BossHp;
    public GameObject TileBlock;
    public Animator PlayerAnimator, GameOverPlayerAnimator, GameOverAnimator;
    public static bool AllCanMove;
    public AllData allData;
    bool RestartBool;
    public GameObject Band, Credits, Hearts;
    public GameObject[] Checkpoints;
    public SpriteRenderer[] Clouds;
    CreditsScr creditsScr;
    MusicScr musicScr;
    bool PlayLastSong;
    float DistanceWithPlayer;
    public Slider VolumeSlider;
    
    
    void Start()
    { 
        ChatCounter = allData.LastChatCounter;
        CollidersCount = 0;
        CrowdCounter = 0;
        StartCoroutine(StartGame());
        CamAnimator.SetTrigger("Entrance");
        AllCanMove = true;
        RestartBool = false;
        creditsScr = GetComponent<CreditsScr>();  
        musicScr = GetComponent<MusicScr>();
        musicScr.GameSong("Play");
        shootScr.CanShoot = true;
        PlayLastSong = false;     
        if(Level1.activeSelf)
        {
            for (int i = 0; i < Clouds.Length; i++)
            {
                Clouds[i].sortingOrder = Random.Range(0,1000);
            }
        }
    }   
    void Update() 
    {
        if(PlayLastSong)
        {
            DistanceWithPlayer = Vector2.Distance(BandPos.position,PlayerPos.position);
            float MusicVolume = (DistanceWithPlayer / 2) * -1;
            if(MusicVolume < -20)
                MusicVolume = -20;
            musicScr.Mixer.SetFloat("YobVolume",MusicVolume); 
        }           
    } 
    IEnumerator StartGame()
    {
        if(allData.CheckPointCounter == 0)
            PlayerPos.position = new Vector2(-34.7f,-4.6f);
        else
            PlayerPos.position = new Vector2(allData.LastCheckPointPos.x, allData.LastCheckPointPos.y);
        
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
        
        for (int i = 0; i < allData.CheckPointCounter; i++)
            Checkpoints[i].SetActive(false);

        for (int i = 0; i < allData.CollidersCount; i++)
            BubblesColliders[i].SetActive(false);


        if(ChatCounter == 0)
            playerMovementScr.CanMove = false;
        else
            playerMovementScr.CanMove = true;
        

        yield return new WaitForSeconds(3);

        if(ChatCounter == 0)
            NextChat();
    }
    
    public void NextChat()
    {
        ChatBubbles[ChatCounter].SetActive(true);
    }
    public void CloseChat()
    {
        ChatBubbles[ChatCounter].SetActive(false);
        ChatCounter++;

    }
    public void CanMoveFunc()
    {
        playerMovementScr.CanMove =! playerMovementScr.CanMove;
    }
    public void CanShootFunc()
    {
        shootScr.CanShoot =! shootScr.CanShoot;
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
        enemyBoss1Scr.InvincibleChange();
    }
    public void AfterBoss()
    {
        Band.SetActive(true);
        for (int i = 0; i < allData.CrowdCounter; i++)
        {
            creditsScr.SpownCrowd();
        }
        BossHp.SetActive(false);
        StartCoroutine(AfterbossWait());
        IEnumerator AfterbossWait()
        {
            yield return new WaitForSeconds(3);
            CamAnimator.SetBool("ShakeLoop", true);
            yield return new WaitForSeconds(3);
            CamAnimator.SetBool("ShakeLoop", false);
            TileBlock.SetActive(false);
            musicScr.GameSong("Stop");
            musicScr.YobSong("Play");
            PlayLastSong = true;
        }
    }
    public void ThrowCredits()
    {
        PlayerAnimator.SetTrigger("HeadBang");
        Credits.SetActive(true);
        Hearts.SetActive(false);
    }
    public void RestartGame()
    {
        if(!RestartBool)
        {
            RestartBool = true;
            GameOverPlayerAnimator.SetTrigger("Restart");
            GameOverAnimator.SetTrigger("Restart");
            allData.VolumeLock = VolumeSlider.value;

            StartCoroutine(WaitForGameOver());
            IEnumerator WaitForGameOver()
            {
                yield return new WaitForSeconds(2f);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
    public void CheckpointPassed()
    {
        allData.CollidersCount += CollidersCount;
        CollidersCount = 0;
        allData.CrowdCounter += CrowdCounter;
        CrowdCounter = 0;
    }
}
