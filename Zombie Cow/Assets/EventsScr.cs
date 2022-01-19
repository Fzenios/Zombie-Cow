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
    void Start()
    {
        ChatCounter = 0;
        StartCoroutine(StartGame());
        playerMovementScr.CanMove = false;
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
}
