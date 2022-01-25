using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScr : MonoBehaviour
{
    public GameObject[] Crowd;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    
    public void SpownCrowd()
    {
        int RandomCrowd = Random.Range(0, Crowd.Length);
        if(!Crowd[RandomCrowd].activeSelf)
        {
            Crowd[RandomCrowd].SetActive(true);
            Crowd[RandomCrowd].GetComponent<Animator>().SetTrigger("Head");
        }
        else
            SpownCrowd();
    }
}
