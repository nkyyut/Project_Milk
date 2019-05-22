//かずき 5/13

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTest : MonoBehaviour
{
    public PlayerController Playercontrol;

    public GameObject Player;
    public GameObject SangoFree;
    [SerializeField] Vector3 OffsetPos;
    Vector3 targetPos;
    Vector3 CameraPos;

    private Vector3 CtransformBox;
    private Quaternion CrotationBox;

    public Transform Settransform;
    public Transform SetFreetransform;

    bool FreeFlg;
    public bool PmoveFlg;

    float InputH;
    float InputV;

    public enum SCENE_TYPE
    {
        MAIN,
        PAUSE,
        FREECAMERA,
    }
    public SCENE_TYPE scene;



    void Start()
    {
        transform.position = Player.transform.position - OffsetPos;
        targetPos = Player.transform.position;
        CameraPos = transform.position;
        Settransform.position = CameraPos;
        FreeFlg = false;
    }


    void Update()
    {
        if (Input.GetAxis("TriggerL") < 0)
            scene = SCENE_TYPE.FREECAMERA;
        else 
            scene = SCENE_TYPE.MAIN;


        switch (scene)
        {
            case SCENE_TYPE.MAIN:
                if(FreeFlg == true)
                {
                    transform.position = CtransformBox;
                    targetPos = Player.transform.position;
                    transform.rotation = CrotationBox;
                    CameraPos = Settransform.position - Player.transform.position;
                    FreeFlg = false;
                }
                if (Playercontrol.chang != true)
                {
                    InputH = Input.GetAxisRaw("HorizontalR");
                    InputV = Input.GetAxisRaw("VerticalR");
                }
                else
                {
                    InputH = 0;
                    InputV = 0;
                }
                if(FreeFlg == false)
                {
                    CtransformBox = transform.position;
                    CrotationBox = transform.rotation;
                }

                break;
            case SCENE_TYPE.PAUSE:
                InputH = 0;
                InputV = 0;
                break;
            case SCENE_TYPE.FREECAMERA:
                
                if (FreeFlg == false)
                {
                    transform.position = SetFreetransform.position;
                    transform.rotation = SetFreetransform.rotation;
                    FreeFlg = true;
                }
                targetPos = SangoFree.transform.position;
                InputH = Input.GetAxisRaw("HorizontalR");
                InputV = Input.GetAxisRaw("VerticalR");
                
                break;
        }
    }


    private void LateUpdate()
    {

        switch (scene)
        {
            case SCENE_TYPE.MAIN:
                Settransform.position += Player.transform.position - targetPos;
                targetPos = Player.transform.position;
                transform.position = Vector3.Lerp(transform.position, Settransform.position, 2.0f * Time.deltaTime);


                Settransform.transform.RotateAround(Player.transform.position, Vector3.up, InputH * 1.5f);
                Settransform.transform.RotateAround(Player.transform.position, -transform.right, InputV * 1.5f);


                transform.RotateAround(Player.transform.position, Vector3.up, InputH * 1.5f);
                transform.RotateAround(Player.transform.position, -transform.right, InputV * 1.5f);


                Settransform.transform.rotation = Player.transform.rotation;

                if (InputH != 0 || InputV != 0)
                {
                    CameraPos = Settransform.position - Player.transform.position;
                }
                break;

            case SCENE_TYPE.FREECAMERA:

                transform.position += SangoFree.transform.position - targetPos;
               
                targetPos = SangoFree.transform.position;

                

                transform.RotateAround(SangoFree.transform.position, Vector3.up, InputH * 1.5f);
                transform.RotateAround(SangoFree.transform.position, -transform.right, InputV * 1.5f);

                

                break;
        }
        
    }
}
