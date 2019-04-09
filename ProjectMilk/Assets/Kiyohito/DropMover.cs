/*與儀清仁　2019/4/9*/
//欠片の落ち方を管理
//このCSの一番下にReadmeあるよ
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropMover : MonoBehaviour {
    enum PIECE_STATE
    {
        IDLE,
        DROP,
        DELETE
    }
    PIECE_STATE PieceState;/*欠片のステート*/
    Vector3 CameraForward;/*カメラの向き*/
    float Sway_HorizontalLimit, Sway_HorizontalVessel;/*横揺れの制限と現在揺れ幅格納用変数*/



    /*テスト用変数*/
    float DropSpeed;
    float SwaySpeed;


	// Use this for initialization
	void Start () {
        /*以下いろいろ初期化*/
        Sway_HorizontalVessel = 0;
        DropSpeed = 1.0f;
        SwaySpeed = 1.0f;
        PieceState = PIECE_STATE.IDLE;
        Sway_HorizontalLimit=1.0f;
        /*以上*/
    }
	
	// Update is called once per frame
	void Update () {
        Switching();
    }

    void Switching()
    {
        switch (PieceState)
        {
            case PIECE_STATE.IDLE:
                /*何もしない*/
                break;
            case PIECE_STATE.DROP:
                /*落ちていく*/
                PieceDropMove(DropSpeed);
                break;
            case PIECE_STATE.DELETE:
                /*消しちゃう*/
                PieceDelete();
                break;
        }
    }
    /*欠片の落ちる実行関数*/
    void PieceDropMove(float DropSpeed)
    {
        //縦
        PieceDropMover_Vertical();
        /*横*/
        PieceDropMover_Hrizontal();
    }

    /*縦に落ちる実行関数*/
    void PieceDropMover_Vertical()
    {
        Vector3 DropDirection = new Vector3(0, -DropSpeed, 0);
        this.gameObject.transform.position += DropDirection * Time.deltaTime;
    }
    /*横に揺れる実行関数*/
    void PieceDropMover_Hrizontal()
    {
        Vector3 Pos = this.transform.position;

        Sway_HorizontalVessel += SwaySpeed*Time.deltaTime;

        Pos.x = Sway_HorizontalVessel;

        this.transform.position = Pos;

        if (Sway_HorizontalVessel > Sway_HorizontalLimit|| Sway_HorizontalVessel < -Sway_HorizontalLimit)
        {
            SwaySpeed *= -1;
        }
    }
    /*欠片を消しちゃう*/
    void PieceDelete()
    {
        Destroy(this.gameObject);
    }

    public void SetPieceState_IDLE() { PieceState = PIECE_STATE.IDLE; }
    public void SetPieceState_DROP() { PieceState = PIECE_STATE.DROP; }
    public void SetPieceState_DELETE() { PieceState = PIECE_STATE.DELETE; }

    /*Readme*************************************************************************************/
    //1.欠片を生成したらこいつをアタッチしてね
    //2.DropStateをDROPにしてね。
    //3.消したくなったらDropStateをDELETEにしてね
    //
    //
    //
    //
    /*******************************************************************************************/

}
