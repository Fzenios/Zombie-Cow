using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AllData")]
public class AllData : ScriptableObject
{
    public Vector2 LastCheckPointPos;
    public int LastChatCounter;
    public int CheckPointCounter;
    public int CrowdCounter;
    public int CollidersCount;
    public float VolumeLock;

    public void CheckpointColliders(Vector2 CheckPointPos, int ChatCounter)
    {
        LastCheckPointPos = CheckPointPos;
        LastChatCounter = ChatCounter;
    }  
    public void NewGame()
    {
        LastCheckPointPos = Vector2.zero;
        LastChatCounter = 0;
        CheckPointCounter = 0;
        CrowdCounter = 0;
        CollidersCount = 0;
    }
}
