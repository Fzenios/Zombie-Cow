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

    public void CheckpointColliders(Vector2 CheckPointPos, int ChatCounter)
    {
        LastCheckPointPos = CheckPointPos;
        LastChatCounter = ChatCounter;
    }  
}
