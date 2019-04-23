//かずき

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
        targetPos = Player.transform.position;
        CameraPos = transform.position;
        transform.position = targetPos + CameraPos;
        Settransform.position = targetPos + CameraPos;
    }

    
    void Update()
    {
        
         InputH = Input.GetAxisRaw("HorizontalR");
         InputV = Input.GetAxisRaw("VerticalR");
        

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
