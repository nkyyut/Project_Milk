//かずき 4/24

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    [SerializeField] Vector3 StatePos;
    public float movespeed = 0.01f;

    public enum SCENE_TYPE
    {
        MAIN,
        PAUSE,
    }
    public SCENE_TYPE scene;
    void Start()
    {
        //transform.position = StatePos;
    }


    void Update()
    {


        switch (scene)
        {

            case SCENE_TYPE.MAIN:
                float InputH = Input.GetAxisRaw("Horizontal");
                float InputV = Input.GetAxisRaw("Vertical");

                Vector3 cameraForward = Vector3.Scale(Camera.main.transform.up, new Vector3(1, 1, 1)).normalized;
                Vector3 moveForward = cameraForward * InputV + Camera.main.transform.right * InputH;

                if (InputH != 0 || InputV != 0)
                {
                    transform.position += moveForward * movespeed;
                    //transform.rotation = Quaternion.LookRotation(moveForward);
                }
                break;
            case SCENE_TYPE.PAUSE:
                break;
        }

    }

}
