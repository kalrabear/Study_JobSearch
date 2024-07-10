using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpbar : MonoBehaviour
{
    public Slider hp;
    public Slider back_Hp;
    public Transform enemy;
    public float maxHp = 1000f;
    public float curHp = 1000f;
    public Text hp_Text;

    public bool backHpHit = false;

    private void Update()
    {
        transform.position = enemy.position;
        //hp.value = curHp / maxHp;
        hp.value = Mathf.Lerp(hp.value, curHp / maxHp, Time.deltaTime * 5f);
        hp_Text.text = "" + curHp.ToString();

        if (backHpHit)
        {
            back_Hp.value = Mathf.Lerp(back_Hp.value, curHp / maxHp, Time.deltaTime * 10f);
            if (hp.value >= back_Hp.value - 0.01f)
            {
                backHpHit = false;
                back_Hp.value = hp.value;
            }
        }
    }


    public void Dmg()
    {
        curHp -= 300f;
        Invoke("BackHp", 0.5f);
    }

    private void BackHp()
    {
        backHpHit = true;
    }

}
