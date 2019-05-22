//かずき 5/13

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
   
    [SerializeField] Vector3 StatePos;
    public float movespeed = 0.01f;
    private Vector3 playerForward;


    public enum SCENE_TYPE
    {
        MAIN,
        PAUSE,
        FREE,
    }
    public SCENE_TYPE scene;
    void Start()
    {
        transform.position = StatePos;
        
    }


    void Update()
    {

        CameraTest.SCENE_TYPE CameraScene;
        CameraScene = Camera.main.GetComponent<CameraTest>().scene;
        int Cscene = (int)CameraScene;
        scene = (SCENE_TYPE)Cscene;

        switch (scene)
        {

            case SCENE_TYPE.MAIN:
                float InputH = Input.GetAxisRaw("Horizontal");
                float InputV = Input.GetAxisRaw("Vertical");
                playerForward = new Vector3(transform.position.x, Camera.main.transform.position.y, transform.position.z) - Camera.main.transform.position;

                Vector3 cameraForward = Vector3.Scale(playerForward , new Vector3(1, 1, 1)).normalized;
                //Debug.Log(cameraForward);
                Vector3 moveForward = cameraForward * InputV + Camera.main.transform.right * InputH;

                if (InputH != 0 || InputV != 0)
                {
                    this.GetComponent<ParticleControl>().Flg = true;
                    transform.position += moveForward * movespeed;
                    transform.rotation = Quaternion.LookRotation(moveForward);
                }
                else this.GetComponent<ParticleControl>().Flg = false;
                break;
            case SCENE_TYPE.PAUSE:
                break;
        }

    }

}
