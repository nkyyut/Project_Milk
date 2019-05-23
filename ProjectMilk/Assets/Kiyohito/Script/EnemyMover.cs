using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class EnemyMover : MonoBehaviour {

    [System.Serializable]
    public struct Route
    {
        public string Movement;
        public float MoveTime;
    }
    public Route[] RouteArray;
    Route[] ReturnRoute;


    [SerializeField] float MoveSpeed;
    [SerializeField] float MoveLimit_Ver;
    [SerializeField] float MoveLimit_Hor;
    [SerializeField] bool LoopFlg;

    bool RoundFlg;
    Vector3 InitPos;
    float Delta;
    float MoveLimitTime;
    int SerchEndPoint;
    int RouteNumber;
    int RouteArrayLength;
    bool Switch;
    Transform MyTransform;

    enum ENEMY_MOVE_STATE
    {
        IDLE,
        VERTICAL,
        HORIZONTAL,
        RIGHT,
        LEFT,
        UP,
        DOWN


    } ENEMY_MOVE_STATE NowEnemyState;


    // Use this for initialization
    void Start() {
        //RouteArrayLength =  Marshal.SizeOf(RouteArray)/ Marshal.SizeOf(RouteArray[0]);
        MyTransform = this.gameObject.transform;
        NowEnemyState = ENEMY_MOVE_STATE.IDLE;
        InitPos = MyTransform.localPosition;
        Switch = true;
        RoundFlg = false;
        RouteNumber = 0;
        SerchEndPoint=RouteArray.Length;
        ReturnRoute = new Route[RouteArray.Length];
        NextMovement();
    }

    // Update is called once per frame
    void Update() {
        Switching();
        //Debug.Log("NowEnemyState"+NowEnemyState);
        //Debug.Log("RouteNumber"+RouteNumber);
        Debug.Log(RoundFlg);
    }

    void NextMovement()
    {
        Route route;

        if (RouteNumber >= SerchEndPoint||RouteNumber<0)
        {
            if (LoopFlg)
            {
                if(RoundFlg)
                    RouteNumber++;
                else RouteNumber--;
                
                RoundFlg = !RoundFlg;
                
                //RouteNumber = 0;
            }
            else
            {
                SetNowEnemyState_IDLE();
                return;
            }
        }

        if (!RoundFlg)
        {
            switch (RouteArray[RouteNumber].Movement)
            {
                case "Vertical":
                    SetNowEnemyState_VERTICAL();
                    MoveLimitTime = RouteArray[RouteNumber].MoveTime;

                    break;
                case "Horizontal":
                    SetNowEnemyState_HORIZONTAL();
                    MoveLimitTime = RouteArray[RouteNumber].MoveTime;
                    break;
                case "Right":
                    SetNowEnemyState_RIGHT();
                    MoveLimitTime = RouteArray[RouteNumber].MoveTime;

                    route.Movement = "Left";
                    route.MoveTime = MoveLimitTime;
                    ReturnRoute[RouteNumber] = route;
                    break;
                case "Left":
                    SetNowEnemyState_LEFT();
                    MoveLimitTime = RouteArray[RouteNumber].MoveTime;
                    route.Movement = "Right";
                    route.MoveTime = MoveLimitTime;
                    ReturnRoute[RouteNumber] = route;
                    break;
                case "Up":
                    SetNowEnemyState_UP();
                    MoveLimitTime = RouteArray[RouteNumber].MoveTime;
                    route.Movement = "Down";
                    route.MoveTime = MoveLimitTime;
                    ReturnRoute[RouteNumber] = route;
                    break;
                case "Down":
                    SetNowEnemyState_DOWN();
                    MoveLimitTime = RouteArray[RouteNumber].MoveTime;
                    route.Movement = "Up";
                    route.MoveTime = MoveLimitTime;
                    ReturnRoute[RouteNumber] = route;
                    break;
            }
        }
        else
        {
            switch (ReturnRoute[RouteNumber].Movement)
            {
                case "Right":
                    SetNowEnemyState_RIGHT();
                    MoveLimitTime = ReturnRoute[RouteNumber].MoveTime;
                    break;
                case "Left":
                    SetNowEnemyState_LEFT();
                    MoveLimitTime = ReturnRoute[RouteNumber].MoveTime;
                    break;
                case "Up":
                    SetNowEnemyState_UP();
                    MoveLimitTime = ReturnRoute[RouteNumber].MoveTime;
                    break;
                case "Down":
                    SetNowEnemyState_DOWN();
                    MoveLimitTime = ReturnRoute[RouteNumber].MoveTime;
                    break;
            }
        }
        //RouteNumber++;
        if (!RoundFlg) RouteNumber++;
        else RouteNumber--;



    }

    void Switching()
    {
        switch (NowEnemyState)
        {
            case ENEMY_MOVE_STATE.IDLE:
                break;
            case ENEMY_MOVE_STATE.VERTICAL:
                VerticalMove();
                break;
            case ENEMY_MOVE_STATE.HORIZONTAL:
                HorizontalMove();
                break;
            case ENEMY_MOVE_STATE.RIGHT:
                RightMove();
                break;
            case ENEMY_MOVE_STATE.LEFT:
                LeftMove();
                break;
            case ENEMY_MOVE_STATE.UP:
                UpMove();
                break;
            case ENEMY_MOVE_STATE.DOWN:
                DownMove();
                break;
        }

        Delta += Time.deltaTime;

    }


    void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag != "InvisibleObjects") return;

        ReturnRoute[--RouteNumber].MoveTime = Delta;
        Debug.Log("RouteNumber" + RouteNumber);
        Debug.Log("Delta"+Delta);
        Debug.Log("ReturnRoute[0].MoveTime"+ ReturnRoute[0].Movement);
        Debug.Log("ReturnRoute[1].MoveTime" + ReturnRoute[1].Movement);
        RoundFlg = !RoundFlg;
        RouteNumber++;
        NextMovement();

    }


    void VerticalMove()
    {
        if (MoveLimitTime<Delta)
        {
            Delta = 0;
            Switch = !Switch;
        }
        if (Switch)
        {
            transform.Translate(Vector3.up * Time.deltaTime/**MoveSpeed*/);
        }
        else transform.Translate(-Vector3.up * Time.deltaTime/**MoveSpeed*/);
    }

    void HorizontalMove()
    {
        if (MoveLimitTime > Delta)
        {
            Delta = 0;
            Switch = !Switch;
        }
        if (Switch)
        {
            transform.Translate(Vector3.right * Time.deltaTime * MoveSpeed/**MoveSpeed*/);
        }
        else transform.Translate(-Vector3.right * Time.deltaTime * MoveSpeed/**MoveSpeed*/);
    }

    void LeftMove()
    {

        if (MoveLimitTime > Delta)
        {
            transform.Translate(Vector3.left * Time.deltaTime*MoveSpeed);
        }
        else
        {
            Delta = 0;
            NextMovement();

        }
    }

    void RightMove()
    {
        if (MoveLimitTime > Delta)
        {
            transform.Translate(Vector3.right * Time.deltaTime * MoveSpeed);
        }
        else
        {
            Delta = 0;
            NextMovement();
        }
    }

    void UpMove()
    {
        if (MoveLimitTime > Delta)
        {
            transform.Translate(Vector3.up * Time.deltaTime * MoveSpeed);
        }
        else
        {
            Delta = 0;
            NextMovement();
        }
    }
    void DownMove()
    {
        if (MoveLimitTime > Delta)
        {
            transform.Translate(Vector3.down * Time.deltaTime * MoveSpeed);
        }
        else
        {
            Delta = 0;
            NextMovement();
        }
    }


    void SetNowEnemyState_IDLE() { NowEnemyState = ENEMY_MOVE_STATE.IDLE; }
    void SetNowEnemyState_VERTICAL() { NowEnemyState = ENEMY_MOVE_STATE.VERTICAL; }
    void SetNowEnemyState_HORIZONTAL() { NowEnemyState = ENEMY_MOVE_STATE.HORIZONTAL; }
    void SetNowEnemyState_UP() { NowEnemyState = ENEMY_MOVE_STATE.UP; }
    void SetNowEnemyState_DOWN() { NowEnemyState = ENEMY_MOVE_STATE.DOWN; }
    void SetNowEnemyState_RIGHT() { NowEnemyState = ENEMY_MOVE_STATE.RIGHT; }
    void SetNowEnemyState_LEFT() { NowEnemyState = ENEMY_MOVE_STATE.LEFT; }

}
