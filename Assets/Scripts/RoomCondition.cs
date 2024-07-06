using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCondition : MonoBehaviour
{
    List<GameObject> enemyListInRoom = new List<GameObject>();
    public bool playerInThisRoom = false;
    public bool isClearRoom = false;

    private void Start()
    {
        
    }


    private void Update()
    {
        if (playerInThisRoom)
        {
            if (enemyListInRoom.Count <= 0 && !isClearRoom)
            {
                isClearRoom = true;
                Debug.Log("Clear");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInThisRoom = true;
            PlayerTargeting.Instance.MonsterList = new List<GameObject>(enemyListInRoom);
            Debug.Log("Enter New Room! Mob Count : " + PlayerTargeting.Instance.MonsterList.Count);
        }

        if (other.CompareTag("Enemy"))
        {
            enemyListInRoom.Add(other.gameObject);
            Debug.Log("Mob name : " + other.gameObject.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInThisRoom = false;
            Debug.Log("Player Exit");
        }
        
        if (other.CompareTag("Enemy"))
        {
            enemyListInRoom.Remove(other.gameObject);
        }
    }

}
