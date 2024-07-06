using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour
{

    private static JoyStick instance;


    public static JoyStick Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<JoyStick>();
            }

            return instance;
        }
    }

    public GameObject smallStick;
    public GameObject stick_Bg;
    Vector3 stickFirst_Pos;
    public Vector3 joyVec;
    Vector3 joystickFirstPos;
    float stickRadius;

    void Start()
    {
        stickRadius = stick_Bg.gameObject.GetComponent<RectTransform>().sizeDelta.y / 2;
        joystickFirstPos = stick_Bg.transform.position;
    }

/*    void Update()
    {
    }*/

    public void PointDown()
    {
        stick_Bg.transform.position = Input.mousePosition;
        smallStick.transform.position = Input.mousePosition;
        stickFirst_Pos = Input.mousePosition;
        if (!PlayerMovement.Instance.ani.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        {
            //Debug.Log ( "WALK!" );
            PlayerMovement.Instance.ani.SetBool("Attack", false);
            PlayerMovement.Instance.ani.SetBool("Idle", false);
            PlayerMovement.Instance.ani.SetBool("Walk", true);
        }
    }


    // 드래그
    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector3 dragPos = pointerEventData.position;
        joyVec = (dragPos - stickFirst_Pos).normalized;

        float stickDis = Vector3.Distance(dragPos, stickFirst_Pos);

        if (stickDis < stickRadius) 
        {
            smallStick.transform.position = stickFirst_Pos + joyVec.normalized * stickDis;
        }
        else
        {
            smallStick.transform.position = stickFirst_Pos + joyVec.normalized * stickRadius;
        }
    }

    // 드래그 끝.
    public void DragEnd()
    {
        joyVec = Vector3.zero;
        if (!PlayerMovement.Instance.ani.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            //            Debug.Log ( "IDLE!" );
            PlayerMovement.Instance.ani.SetBool("Attack", false);
            PlayerMovement.Instance.ani.SetBool("Walk", false);
            PlayerMovement.Instance.ani.SetBool("Idle", true);
        }
        stick_Bg.transform.position = joystickFirstPos;
        smallStick.transform.position = joystickFirstPos;
    }
}
