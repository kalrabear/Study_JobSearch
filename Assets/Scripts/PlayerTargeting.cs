﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargeting : MonoBehaviour
{
    public static PlayerTargeting Instance // singlton     
    {
        get
        {
            if ( instance == null )
            {
                instance = FindObjectOfType<PlayerTargeting> ( );
                if ( instance == null )
                {
                    var instanceContainer = new GameObject ( "PlayerTargeting" );
                    instance = instanceContainer.AddComponent<PlayerTargeting> ( );
                }
            }
            return instance;
        }
    }
    private static PlayerTargeting instance;

    public bool getATarget = false;
    float currentDist = 0;      //현재 거리
    float closetDist = 100f;    //가까운 거리
    float TargetDist = 100f;   //타겟 거리
    int closeDistIndex = 0;    //가장 가까운 인덱스
    public int TargetIndex = -1;      //타겟팅 할 인덱스
    int prevTargetIndex = 0;
    public LayerMask layerMask;

    public float atkSpd = 1f;

    public List<GameObject> MonsterList = new List<GameObject> ( );
    //Monster를 담는 List 

    public GameObject PlayerBolt;  //발사체
    public Transform AttackPoint;

    void OnDrawGizmos()
    {
        if (getATarget)
        {
            for ( int i = 0 ; i < MonsterList.Count ; i++ )
            {
                if ( MonsterList[i] == null ) { return; }// 추가
                RaycastHit hit; //	
                bool isHit = Physics.Raycast ( transform.position, MonsterList[i].transform.GetChild ( 0 ).position - transform.position,//변경 
                                            out hit, 20f, layerMask );

                if ( isHit && hit.transform.CompareTag ("Enemy"))
                {
                    Gizmos.color = Color.green;
                }
                else
                {
                    Gizmos.color = Color.red;
                }
                Gizmos.DrawRay ( transform.position, MonsterList[i].transform.GetChild ( 0 ).position - transform.position );//변경 
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        SetTarget();
        AtkTarget();
    }
    
    void Attack()
    {
        PlayerMovement.Instance.ani.SetFloat ( "AttackSpeed", atkSpd );
        Instantiate (PlayerBolt, AttackPoint.position, transform.rotation);
    }

    void SetTarget()
    {
        if (MonsterList.Count != 0)
        {
            prevTargetIndex = TargetIndex;
            currentDist = 0f;
            closeDistIndex = 0;
            TargetIndex = -1;

            for (int i = 0 ; i < MonsterList.Count ; i++)
            {
                if ( MonsterList[i] == null ) { return; }   // 추가
                currentDist = Vector3.Distance ( transform.position, MonsterList[i].transform.GetChild ( 0 ).position );//변경 

                RaycastHit hit;
                bool isHit = Physics.Raycast (transform.position, MonsterList[i].transform.GetChild ( 0 ).position - transform.position,//변경 
                                            out hit, 20f, layerMask );

                if ( isHit && hit.transform.CompareTag ("Enemy"))
                {
                    if ( TargetDist >= currentDist  )
                    {
                        TargetIndex = i;

                        TargetDist = currentDist;

                        if (!JoyStickMovement.Instance.isPlayerMoving && prevTargetIndex != TargetIndex)  // 추// 추가
                        {
                            TargetIndex = prevTargetIndex;
                        }
                    }
                }

                if (closetDist >= currentDist)
                {
                    closeDistIndex = i;
                    closetDist = currentDist;
                }
            }

            if (TargetIndex == -1)
            {
                TargetIndex = closeDistIndex;
            }
            closetDist = 100f;
            TargetDist = 100f;
            getATarget = true;
        }

    }

    void AtkTarget()
    {
        if ( TargetIndex == -1 || MonsterList.Count == 0 )  // 추가 
        {
            PlayerMovement.Instance.ani.SetBool ("Attack", false);
            return;
        }
        if ( getATarget && !JoyStickMovement.Instance.isPlayerMoving && MonsterList.Count != 0 )
        {
//            Debug.Log ( "lookat : " + MonsterList[TargetIndex].transform.GetChild ( 0 ) );  // 변경
            transform.LookAt ( MonsterList[TargetIndex].transform.GetChild(0));     // 변경

            if (PlayerMovement.Instance.ani.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                PlayerMovement.Instance.ani.SetBool("Idle", false);
                PlayerMovement.Instance.ani.SetBool("Walk", false);
                PlayerMovement.Instance.ani.SetBool("Attack", true);
            }

        }
        else if (JoyStickMovement.Instance.isPlayerMoving)
        {
            if (!PlayerMovement.Instance.ani.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
            {
                PlayerMovement.Instance.ani.SetBool("Attack", false);
                PlayerMovement.Instance.ani.SetBool("Idle", false);
                PlayerMovement.Instance.ani.SetBool("Walk", true);
            }
        }
        else
        {
            PlayerMovement.Instance.ani.SetBool("Attack", false);
            PlayerMovement.Instance.ani.SetBool("Idle", true);
            PlayerMovement.Instance.ani.SetBool("Walk", false);
        }
    }
}
