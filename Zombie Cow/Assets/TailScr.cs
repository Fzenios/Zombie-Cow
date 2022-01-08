using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailScr : MonoBehaviour
{
    public float TailDamage;
    public int Length;
    public LineRenderer LineRend;
    public Vector3[] SegmentPoses;
    Vector3[] SegmentV;
    public Transform TargetDir;
    public float TargetDist;
    public float SmoothSpeed;
    public float TrailSpeed;
    public float WiggleSpeed;
    public float WiggleMagnitude;
    public Transform WiggleDir;
    Transform BossPos;
    
    void Start()
    {
        BossPos = GameObject.FindGameObjectWithTag("Boss").transform;
        LineRend.positionCount = Length;
        SegmentPoses = new Vector3[Length];
        SegmentV = new Vector3[Length];
        SegmentPoses[0] = TargetDir.position;

        for (int i = 1; i < SegmentPoses.Length; i++)
        {
            SegmentPoses[i] = TargetDir.position;
        }
        LineRend.SetPositions(SegmentPoses);
        Destroy(gameObject, 5);
    }

    void Update()
    {
        WiggleDir.localRotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time * WiggleSpeed) * WiggleMagnitude);

        SegmentPoses[0] = TargetDir.position;

        for (int i = 1; i < SegmentPoses.Length; i++)
        {
            SegmentPoses[i] = Vector3.SmoothDamp(SegmentPoses[i], SegmentPoses[i - 1] + TargetDir.right * TargetDist, ref SegmentV[i], SmoothSpeed + i / TrailSpeed);
        }
        LineRend.SetPositions(SegmentPoses);
    }
    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.transform.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealthScr>().TakeDmg(TailDamage);
            Destroy(gameObject);
        }        
    }
}
