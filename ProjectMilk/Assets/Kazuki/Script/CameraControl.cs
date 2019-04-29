using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public GameObject Player;
    public GameObject Player2;
    Vector3 targetPos;
    Vector3 posPuls = Vector3.zero;

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

       
            transform.RotateAround(targetPos, Vector3.up, InputH);
            transform.RotateAround(targetPos + posPuls, Vector3.right, InputV);
       
    }

}
