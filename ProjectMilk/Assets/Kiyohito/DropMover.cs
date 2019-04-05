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
    PIECE_STATE DropState;
    Vector3 CameraForward;
    float Sway_HorizontalLimit, Sway_HorizontalVessel;



    /*テスト用変数*/
    float DropSpeed;
    float SwaySpeed;


	// Use this for initialization
	void Start () {
        Sway_HorizontalVessel = 0;
        DropSpeed = 1.0f;
        SwaySpeed = 1.0f;
        DropState = PIECE_STATE.IDLE;
        Sway_HorizontalLimit=1.0f;
    }
	
	// Update is called once per frame
	void Update () {
        DropManegement();
    }

    void DropManegement()
    {
        switch (DropState)
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

    void PieceDropMove(float DropSpeed)
    {
        PieceDropMover_Vertical();
        PieceDropMover_Hrizontal();
    }

    void PieceDropMover_Vertical()
    {
        Vector3 DropDirection = new Vector3(0, -DropSpeed, 0);
        this.gameObject.transform.position += DropDirection * Time.deltaTime;
    }

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

    void PieceDelete()
    {
        Destroy(this.gameObject);
    }

}
