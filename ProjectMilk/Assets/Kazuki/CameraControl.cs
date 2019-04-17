using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public GameObject Player;
    public GameObject Player2;
    Vector3 targetPos;
    Vector3 posPuls = Vector3.zero;
    short Flg = 0;
    // Use this for initialization
    void Start()
    {
        targetPos = Player.transform.position;

    }

    // Update is called once per frame
    void Update()
    {

        transform.position += Player.transform.position - posPuls - targetPos;
        targetPos = Player.transform.position - posPuls;
        //  Debug.Log(transform.position);
        float InputH = Input.GetAxisRaw("HorizontalR");
        float InputV = Input.GetAxisRaw("VerticalR");

        if (Player.GetComponent<PlayerControl>().gravityFlg == false)
        {
            transform.RotateAround(targetPos, Vector3.up, InputH);
        }
        else
        {
            if (Flg == 0)
            {
                posPuls = new Vector3(0, 1f, -0.3f);
                transform.RotateAround(targetPos, Vector3.forward, -InputH);
                transform.RotateAround(targetPos, Vector3.up, InputV);
                transform.Rotate(-90, -90, 90);
                Flg = 1;
            }
            transform.RotateAround(targetPos + posPuls, Vector3.forward, -InputH);
            transform.RotateAround(targetPos + posPuls, Vector3.up, -InputV);
        }
    }

}
