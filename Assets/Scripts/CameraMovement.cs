using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private static CameraMovement instance;

    public static CameraMovement Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CameraMovement>();
            }
            return instance;
        }
    }


    public GameObject player;

    public float offsetY = 45f;
    public float offsetZ = -40f;

    Vector3 cameraPos;

    private void Start()
    {
        /*cameraPos.y = player.transform.position.y + offsetY;
        cameraPos.z = player.transform.position.z + offsetZ;*/

        transform.position = player.transform.position;
    }


    private void LateUpdate()
    {
        cameraPos.y = player.transform.position.y + offsetY;
        cameraPos.z = player.transform.position.z + offsetZ;

        transform.position = cameraPos;
    }

    public void CarmeraNextRoom()
    {
        cameraPos.x = player.transform.position.x;
    }
}
