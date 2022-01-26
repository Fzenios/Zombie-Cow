using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AllData")]
public class AllData : ScriptableObject
{
    public Vector2 LastCheckPointPos;
    public int LastChatCounter;
    public GameObject LastCheckPointObj;

    public void CheckpointColliders(Vector2 CheckPointPos, GameObject CheckPointObj, int ChatCounter)
    {
        LastCheckPointPos = CheckPointPos;
        LastCheckPointObj = CheckPointObj;
        LastChatCounter = ChatCounter;
    }  
}
