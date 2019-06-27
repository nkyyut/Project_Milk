//かずき 5/28

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTest : MonoBehaviour
{

    public PlayerControl Playercontrol;

    public GameObject Player;
    public GameObject SangoFree;
    public PauseManager Pause;
    public GameOverManager GameOverManager;

    [SerializeField] Vector3 OffsetPos;
    Vector3 targetPos;
    Vector3 CameraPos;
    public Transform cameraPoint;
    public bool outoFlg;
    PauseManager.PAUSE_MANAGER_STATE pause;
    private Vector3 CtransformBox;
    private Quaternion CrotationBox;

    public Transform Settransform;
    public Transform SetFreetransform;

    bool FreeFlg;
    private bool MoveFlg;
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

    public SCENE_TYPE Pscene { get { return scene; } }



    void Start()
    {
        transform.position = Player.transform.position - OffsetPos;
        targetPos = cameraPoint.transform.position;
        CameraPos = transform.position;
        Settransform.position = CameraPos;
        MoveFlg = false;
        FreeFlg = false;
        outoFlg = true;
    }


    void Update()
    {

        if (!GameOverManager.GameOverStartFlg)
        {

            pause = Pause.NowState;
        }
        else SetPause();
        




        switch (scene)
        {
            case SCENE_TYPE.MAIN:
                if (FreeFlg == true)
                {
                    transform.position = CtransformBox;
                    targetPos = cameraPoint.transform.position;
                    transform.rotation = CrotationBox;
                    CameraPos = Settransform.position - Player.transform.position;
                    outoFlg = true;
                    FreeFlg = false;
                }

                if (Playercontrol.chang != true)
                {
                    InputH = Input.GetAxisRaw("HorizontalR");
                    InputV = Input.GetAxisRaw("VerticalR");

                    if (InputH + InputV != 0)
                    {
                        MoveFlg = false;
                        outoFlg = false;
                    }
                    else if (MoveFlg == false)
                        MoveFlg = true;

                }
                else
                {
                    if (MoveFlg == true)
                        MoveFlg = false;
                    outoFlg = true;
                    InputH = 0;
                    InputV = 0;
                }
                break;

            case SCENE_TYPE.PAUSE:
                InputH = 0;
                InputV = 0;
                break;

            case SCENE_TYPE.FREECAMERA:
                outoFlg = false;
                if (FreeFlg == false)
                {
                    CtransformBox = Settransform.position;
                    CrotationBox = transform.rotation;
                    transform.position = SetFreetransform.position;
                    transform.rotation = SetFreetransform.rotation;

                    FreeFlg = true;
                }
                targetPos = SetFreetransform.transform.position;
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
                Settransform.position += cameraPoint.position - targetPos;
                targetPos = cameraPoint.position;
                //transform.position = Vector3.Lerp(transform.position, Settransform.position, 2.0f * Time.deltaTime);


                Settransform.transform.RotateAround(Player.transform.position, transform.up, InputH * 1.5f);
                Settransform.transform.RotateAround(Player.transform.position, -transform.right, InputV * 1.5f);


                transform.RotateAround(Player.transform.position, transform.up, InputH * 1.5f);
                transform.RotateAround(Player.transform.position, -transform.right, InputV * 1.5f);


                //Settransform.transform.rotation = Player.transform.rotation;



                if (InputH != 0 || InputV != 0)
                {
                    CameraPos = Settransform.position - Player.transform.position;
                }
                break;

            case SCENE_TYPE.FREECAMERA:

                transform.position += SetFreetransform.position - targetPos;

                targetPos = SetFreetransform.position;



                transform.RotateAround(SangoFree.transform.position, Vector3.up, InputH * 1.5f);
                transform.RotateAround(SangoFree.transform.position, -transform.right, InputV * 1.5f);



                break;
        }

    }

    public void SetPause() { scene = SCENE_TYPE.PAUSE; }
    public void SetMain() { scene = SCENE_TYPE.MAIN; }
}
