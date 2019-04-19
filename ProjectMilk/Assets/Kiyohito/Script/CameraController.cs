using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject MainCamera;              //メインカメラ格納用
    public GameObject PlayerObject;            //回転の中心となるプレイヤー格納用
    public GameObject CameraPoint;
    float RotateSpeed = 2.0f;            //回転の速さ
    float FollowSpeed = 2.0f;

    //呼び出し時に実行される関数
    void Start()
    {
        CameraPoint.transform.position=this.gameObject.transform.position;

    }


    //単位時間ごとに実行される関数
    void Update()
    {
        //rotateCameraの呼び出し
        RotateCamera();
        CheckSlipPosition();
    }

    //カメラを回転させる関数
    private void RotateCamera()
    {
        //Vector3でX,Y方向の回転の度合いを定義
        Vector3 angle = new Vector3(Input.GetAxis("Horizontal2") * RotateSpeed, Input.GetAxis("Vertical2") * RotateSpeed, 0);
        //transform.RotateAround()をしようしてメインカメラを回転させる
        MainCamera.transform.RotateAround(PlayerObject.transform.position, Vector3.up, angle.x);
        MainCamera.transform.RotateAround(PlayerObject.transform.position, transform.right, angle.y);
    }

    void CheckSlipPosition()
    {

    }
}
