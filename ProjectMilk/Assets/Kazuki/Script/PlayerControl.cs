//かずき 5/28

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public CameraTest CameraScene;

    [SerializeField] Vector3 StatePos;
    public float movespeed = 0.01f;
    public Vector3 playerForward;


    private bool PmoveFlg = false;
    public bool chang { get { return PmoveFlg; } }

    public enum SCENE_TYPE
    {
        MAIN,
        PAUSE,
        FREE,
    }
    public SCENE_TYPE scene;
    void Start()
    {
        // transform.position = StatePos;
        playerForward = new Vector3(transform.position.x, Camera.main.transform.position.y, transform.position.z);


    }


    void Update()
    {

       

        int Cscene = (int)CameraScene.Pscene;
        scene = (SCENE_TYPE)Cscene;

        switch (scene)
        {

            case SCENE_TYPE.MAIN:
                float InputH = Input.GetAxisRaw("Horizontal");
                float InputV = Input.GetAxisRaw("Vertical");

                if (InputH + InputV != 0)
                    PmoveFlg = true;
                else
                    PmoveFlg = false;


                playerForward = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z) - Camera.main.transform.position;



                Vector3 cameraForward = Vector3.Scale(Camera.main.transform.up, new Vector3(1, 1, 1)).normalized;
                Vector3 moveForward = cameraForward * InputV + Camera.main.transform.right * InputH;


                if (InputH != 0 || InputV != 0)
                {
                    //this.GetComponent<ParticleControl>().Flg = true;

                    transform.position += moveForward * movespeed;
                }

                // else this.GetComponent<ParticleControl>().Flg = false;
                break;
            case SCENE_TYPE.PAUSE:
                break;
        }

    }

}
