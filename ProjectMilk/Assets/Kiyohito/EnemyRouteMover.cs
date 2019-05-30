using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRouteMover : MonoBehaviour {

    [System.Serializable]
    public struct Route
    {
        public string Movement;
        public float MoveTime;
    }
    public Route[] RouteArray;
    public string[] ReflectionTagArray; 
    Route[] RoundRouteArray;



    [SerializeField] float MoveSpeed;
    [SerializeField] float MoveLimit_Ver;
    [SerializeField] float MoveLimit_Hor;
    [SerializeField] bool LoopFlg;

    bool Landing=false;
    bool[] PosSetFlgArray;
    bool IdleFlg;
    bool ReflectionFlg = false;
    bool HitedFlg=false;
    GameObject HitGameObject;
    Vector3 [] InitPos;
    float Delta;
    float MoveLimitTime;
    int SerchEndPoint;
    int RouteNumber;
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
        DOWN,
        WAIT

    }
    ENEMY_MOVE_STATE NowEnemyState;





    // Use this for initialization
    void Start () {
        MyTransform = this.gameObject.transform;
        NowEnemyState = ENEMY_MOVE_STATE.IDLE;
        Switch = true;
        RouteNumber = 0;
        SerchEndPoint = RouteArray.Length;
        
        if (LoopFlg==true)
            SerchEndPoint = RouteArray.Length*2;
        RoundRouteArray = new Route[SerchEndPoint];
        PosSetFlgArray = new bool [SerchEndPoint];
        InitPos = new Vector3[SerchEndPoint];
        SetReturnRoute();
        NextMovement();
       // NowEnemyState = ENEMY_MOVE_STATE.IDLE;
    }


	// Update is called once per frame
	void Update () {
        //Debug.Log("RouteNumber"+RouteNumber);
        //Debug.Log("NowEnemyState"+NowEnemyState);
        Switching();
        //Debug.Log("RouteNumber"+RouteNumber);
        //for (int i = 0; i < SerchEndPoint; i++)
        //{
        //    Debug.Log("RoundRouteArray[" + i + "].Movement" + RoundRouteArray[i].Movement);
        //    Debug.Log("RoundRouteArray[" + i + "].MoveTime" + RoundRouteArray[i].MoveTime);

        //}

        //Debug.Log(HitedFlg);
        //Debug.Log("↓");
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
            case ENEMY_MOVE_STATE.WAIT:
                WaitMove();
                break;
        }

        Delta += Time.deltaTime;

    }

    void SetReturnRoute()
    {
        for (int i = 0; i < RouteArray.Length; i++)
        {
            RoundRouteArray[i] = RouteArray[i];
            if (LoopFlg)
            {
                RoundRouteArray[SerchEndPoint - i - 1].Movement = OppositeReturn(RouteArray[i]).Movement;
                RoundRouteArray[SerchEndPoint - i - 1].MoveTime = OppositeReturn(RouteArray[i]).MoveTime;
            }
        }
    }

    Route OppositeReturn(Route Movement)
    {
        Route Opposite;
        Opposite.Movement = "err";
        Opposite.MoveTime = 99999;

        switch (Movement.Movement)
        {
            case "Up":
                Opposite.Movement = "Down";
                Opposite.MoveTime = Movement.MoveTime;
                break;
            case "Down":
                Opposite.Movement = "Up";
                Opposite.MoveTime = Movement.MoveTime;
                break;
            case "Right":
                Opposite.Movement = "Left";
                Opposite.MoveTime = Movement.MoveTime;
                break;
            case "Left":
                Opposite.Movement = "Right";
                Opposite.MoveTime = Movement.MoveTime;
                break;
            case "Wait":
                Opposite.Movement = "Wait";
                Opposite.MoveTime = Movement.MoveTime;
                break;

        }

        return Opposite;
    }

    void NextMovement()
    {



        if (RouteNumber >= SerchEndPoint&&!HitedFlg)
        {
            if (LoopFlg == true)
            {
                RouteNumber = 0;
                SetReturnRoute();
            }
            else {
                SetNowEnemyState_IDLE();
                return;
            }
        }

        //Debug.Log("in:"+RouteNumber);
        //Debug.Log("MoveLimitTime"+MoveLimitTime);
        //if (!RoundFlg) {
            switch (RoundRouteArray[RouteNumber].Movement)
            {
                case "Right":
                    SetNowEnemyState_RIGHT();
                    MoveLimitTime = RoundRouteArray[RouteNumber].MoveTime;
                    break;
                case "Left":
                    SetNowEnemyState_LEFT();
                    MoveLimitTime = RoundRouteArray[RouteNumber].MoveTime;
                    break;
                case "Up":
                    SetNowEnemyState_UP();
                    MoveLimitTime = RoundRouteArray[RouteNumber].MoveTime;
                    break;
                case "Down":
                    SetNowEnemyState_DOWN();
                    MoveLimitTime = RoundRouteArray[RouteNumber].MoveTime;
                    break;
                case "Wait":
                    SetNowEnemyState_WAIT();
                    MoveLimitTime = RoundRouteArray[RouteNumber].MoveTime;
                    break;
            }
        //}

        //if (!PosSetFlgArray[RouteNumber])
        //{
        //    //Debug.Log("in");
        //    InitializePos(RouteNumber);
        //    PosSetFlgArray[RouteNumber] = true;
        //}

        //if (RouteNumber == 0 || RouteNumber == RouteArray.Length)
        //{
        //    SetPosition(RouteNumber);
        //}
        RouteNumber++;
    }

    void InitializePos(int RouteNumber)
    {
        InitPos[RouteNumber] = this.transform.position;
    }

    void SetPosition(int RouteNumber)
    {
        this.transform.position = InitPos[RouteNumber];
    }

    void OnCollisionEnter(Collision other)
    {
        if (!HitedFlg)
        {
            for (int i = 0; i < ReflectionTagArray.Length; i++)
            {

                if (other.transform.tag == ReflectionTagArray[i])
                {
                    Debug.Log("hit");
                    RouteNumber = SerchEndPoint - RouteNumber;
                    RoundRouteArray[RouteNumber].MoveTime = Delta - Time.deltaTime;
                    Delta = 0;
                    //RouteNumber++;
                    NextMovement();
                    break;
                }
            }

        }
        HitedFlg = true;

    }

    void OnCollisionExit(Collision other)
    {
        HitedFlg = false;
    }



    //void OnTriggerEnter(Collider other)
    //{
    //    for (int i = 0; i < ReflectionTagArray.Length; i++)
    //    {
    //        if (other.transform.tag == ReflectionTagArray[i])
    //        {
    //            Debug.Log("hit");
    //            RouteNumber = SerchEndPoint - RouteNumber;
    //            RoundRouteArray[RouteNumber].MoveTime = Delta-Time.deltaTime;
    //            Delta = 0;
    //            //RouteNumber++;
    //            NextMovement();
    //            break;
    //        }
    //    }

    //}


    void VerticalMove()
    {
        if (MoveLimitTime < Delta)
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
            transform.Translate(Vector3.left * Time.deltaTime * MoveSpeed);
        }
        else
        {
            HitedFlg = false;
            Delta = 0;
            //Debug.Log(RouteNumber);
            //if (RouteNumber < SerchEndPoint && PosSetFlgArray[RouteNumber])
            //    SetPosition(RouteNumber);
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
            HitedFlg = false;
            Delta = 0;
            //Debug.Log(RouteNumber);
            //if (RouteNumber < SerchEndPoint && PosSetFlgArray[RouteNumber])
            //    SetPosition(RouteNumber);
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
            HitedFlg = false;
            Delta = 0;
            //Debug.Log(RouteNumber);
            //if (RouteNumber < SerchEndPoint && PosSetFlgArray[RouteNumber])
            //    SetPosition(RouteNumber);
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
            HitedFlg = false;
            Delta = 0;
            //Debug.Log(RouteNumber);
            //if (RouteNumber < SerchEndPoint && PosSetFlgArray[RouteNumber])
            //    SetPosition(RouteNumber);
            NextMovement();
        }
    }
    void WaitMove()
    {
        if(Delta>MoveLimitTime)
        {
            Delta = 0;
            NextMovement();
        }
    }


    public void SetNowEnemyState_IDLE() { NowEnemyState = ENEMY_MOVE_STATE.IDLE; }
    public void SetNowEnemyState_VERTICAL() { NowEnemyState = ENEMY_MOVE_STATE.VERTICAL; }
    public void SetNowEnemyState_HORIZONTAL() { NowEnemyState = ENEMY_MOVE_STATE.HORIZONTAL; }
    public void SetNowEnemyState_UP() { NowEnemyState = ENEMY_MOVE_STATE.UP; }
    public void SetNowEnemyState_DOWN() { NowEnemyState = ENEMY_MOVE_STATE.DOWN; }
    public void SetNowEnemyState_RIGHT() { NowEnemyState = ENEMY_MOVE_STATE.RIGHT; }
    public void SetNowEnemyState_LEFT() { NowEnemyState = ENEMY_MOVE_STATE.LEFT; }
    public void SetNowEnemyState_WAIT() { NowEnemyState = ENEMY_MOVE_STATE.WAIT; }

}
