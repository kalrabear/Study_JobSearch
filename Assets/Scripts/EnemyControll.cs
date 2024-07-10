using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControll : MonoBehaviour
{
    public GameObject enemy_Canvas;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Potato"))
        {
            enemy_Canvas.GetComponent<EnemyHpbar>().Dmg();
        }
    }
}
