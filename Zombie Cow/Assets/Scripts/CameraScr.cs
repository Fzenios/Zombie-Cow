using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScr : MonoBehaviour
{
    public Transform PlayerPos;
    void Start()
    {
        
    }    
    void Update()
    {
        transform.position = PlayerPos.position;
    }
}
