using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScr : MonoBehaviour
{
    public GameObject[] CrowdLast;
    public Transform CrowdList;
    
    public void SpownCrowd()
    {
        int RandomCrowd = Random.Range(0, CrowdLast.Length);
        float RandomX = Random.Range(145f, 162f);
        float RandomY = Random.Range(-84.86f, -85.3f);
        float RandomZ = Random.Range(1f, 7f);
        Vector3 CrowdPos = new Vector3(RandomX, RandomY, RandomZ);
        Instantiate(CrowdLast[RandomCrowd], CrowdPos, Quaternion.identity, CrowdList);
    }
}
