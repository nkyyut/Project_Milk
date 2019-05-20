//かずき 5/13

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTest : MonoBehaviour
{

    public GameObject Player;
    [SerializeField] Vector3 OffsetPos;
    Vector3 targetPos;
    Vector3 CameraPos;

    public Transform Settransform;


    float InputH;
    float InputV;

    public enum SCENE_TYPE
    {
        MAIN,
        PAUSE,
    }
    public SCENE_TYPE scene;


    void Start()
    {
        transform.position = Player.transform.position - OffsetPos;
        targetPos = Player.transform.position;
        CameraPos = transform.position;
        Settransform.position = targetPos + CameraPos;
    }


    void Update()
    {
        if (scene == SCENE_TYPE.MAIN)
        {
            InputH = Input.GetAxisRaw("Horizontal2");
            InputV = -Input.GetAxisRaw("Vertical2");
        }

    }
    private void LateUpdate()
    {

        //Debug.Log("Chang");

        Settransform.position += Player.transform.position - targetPos;
        targetPos = Player.transform.position;
        transform.position = Vector3.Lerp(transform.position, Player.transform.position + CameraPos, 2.0f * Time.deltaTime);

        
        Settransform.transform.RotateAround(Player.transform.position, Vector3.up, InputH * 1.5f);
        Settransform.transform.RotateAround(Player.transform.position, -transform.right, InputV * 1.5f);
        

        transform.RotateAround(Player.transform.position, Vector3.up, InputH * 1.5f);
        transform.RotateAround(Player.transform.position, -transform.right, InputV * 1.5f);


        Settransform.transform.rotation = Player.transform.rotation;

            if (InputH != 0 || InputV != 0)
        {
            CameraPos = Settransform.position - Player.transform.position;
        }

    }
}
