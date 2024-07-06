using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private static PlayerMovement instance;

    public float moveSpeed;

    public static PlayerMovement Instance
    { 
        get 
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerMovement>();
                if (instance == null )
                {
                    var instanceContainer = new GameObject("PlayerMovement");
                    instance = instanceContainer.AddComponent<PlayerMovement>();
                }
            }
            return instance; 
        } 
    }


    Rigidbody rb;
    public Animator ani;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
    }


    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        rb.velocity = new Vector3(moveHorizontal * moveSpeed, rb.velocity.y, moveVertical * moveSpeed);
        if (JoyStickMovement.Instance.joyVec.x != 0 || JoyStickMovement.Instance.joyVec.y != 0)
        {
            rb.velocity = new Vector3(JoyStickMovement.Instance.joyVec.x, rb.velocity.y, JoyStickMovement.Instance.joyVec.y) * moveSpeed;
            rb.rotation = Quaternion.LookRotation(new Vector3(JoyStickMovement.Instance.joyVec.x, 0, JoyStickMovement.Instance.joyVec.y));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("NextRoom"))
        {
            Debug.Log("Next Room");
            StageMgr.Instance.NextStage();
        }
    }

}
