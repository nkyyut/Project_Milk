//かずき2019/4/23

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTest : MonoBehaviour
{

    public GameObject Player;
    Vector3 targetPos;
    Vector3 CameraPos;

    public Transform Settransform;
    
    
    float InputH;
    float InputV;


    void Start()
    {
        Debug.Log("Initialize");
        targetPos = Player.transform.position;
        CameraPos = transform.position;
        //transform.position = targetPos + CameraPos;
        //Settransform.position = targetPos + CameraPos;
    }

    
    void Update()
    {
        
         InputH = Input.GetAxisRaw("Horizontal2");
         InputV = Input.GetAxisRaw("Vertical2");
        

    }
    private void LateUpdate()
    {
       
            Debug.Log("Chang");

        Settransform.position += Player.transform.position - targetPos;
        targetPos = Player.transform.position;
        transform.position = Vector3.Lerp(transform.position, Player.transform.position + CameraPos, 2.0f * Time.deltaTime);

        
        
        Settransform.transform.RotateAround(Player.transform.position, Vector3.up, InputH * 1.5f);
        Settransform.transform.RotateAround(Player.transform.position, -transform.right, InputV * 1.5f);
        transform.RotateAround(Player.transform.position, Vector3.up, InputH * 1.5f);
        transform.RotateAround(Player.transform.position, -transform.right, InputV * 1.5f);


        if (InputH != 0 || InputV != 0)
        {
           CameraPos = Settransform.position - Player.transform.position ;
        }

    }
}
