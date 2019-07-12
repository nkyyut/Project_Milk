//與儀清仁　2019/4/9
//時間の管理と時間UIの管理
//このCSの一番下にReadmeあるよ
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KiyohitoConst;
public class TimeManager : MonoBehaviour {

    public GameObject[] TimeTxtArray;
    public GameObject[] TimeFrameArray;
    public GameObject[]TinAnago_gameObject;
    public bool[] TinAnago_bool;
    public GameOverManager GameOverManager;
    int IntervalPoint;//IntervalTimeの係数
    int TotalOfTinAnago;//チンアナゴの数
    public float GameLimitTime;//ゲームの制限時間
    float NowPlayTime;//現在の経過時間
    float TimeSpeed;//時間の進み具合基本1/*使う予定はないが一応*/
    float PullBackInterval;//チンアナゴが引っ込む時間の間隔

    /*時間を刻むか否か*/
    enum TIMEMANAGER_STATE
    {
        IDLE,/*何もしない*/
        TICK,/*時間を進ませる*/
    }TIMEMANAGER_STATE NowTimeManagerState/*現在のステート格納*/;




    // Use this for initialization
    void Start () {
        Initialize();
    }

    // Update is called once per frame
    void Update () {
        /*以下テスト用呼び出し*/
        //if (Input.GetMouseButtonUp(0)) SetNowTimeManagerState_TICK();
        //if (Input.GetMouseButtonUp(1)) SetNowTimeManagerState_IDLE();
        /*以上*/


        Switching();
	}


    /*いろいろ初期化*/
    void Initialize()
    {
            /*以下いろいろ初期化*/
            TotalOfTinAnago = TinAnago_gameObject.Length;
            //GameLimitTime = 10.0f;
            NowPlayTime = 0.0f;
            NowTimeManagerState = TIMEMANAGER_STATE.TICK;
            TimeSpeed = 1.0f;
            IntervalPoint = 1;
            PullBackInterval = GameLimitTime / (TotalOfTinAnago + 1);
            TimeTxtArray[9].GetComponent<ChangeTimeTxt>().ChangeTxt(GameLimitTime);
            /*以上いろいろ初期化*/

    }

    /*現在のステートによるスイッチング*/
    void Switching()
    {
        switch(NowTimeManagerState)
        {
            case TIMEMANAGER_STATE.IDLE:
                break;
            case TIMEMANAGER_STATE.TICK:
                if (!CheckLimitTime())
                {
                    GameObject[] Onihitode = GameObject.FindGameObjectsWithTag("Onihitode");
                    if (Onihitode.Length > 0) { 
                        GameOverManager.SetGameOverStart();
                        SetNowTimeManagerState_IDLE();
                    }
                    break;
                }
                TickTimer();
                TinAnagoManager();
                break;
        }

    }

    
    

    /*チンアナゴの管理*/
    void TinAnagoManager()
    {
        if (IntervalPoint <= TotalOfTinAnago)
            if (NowPlayTime > PullBackInterval * IntervalPoint)
            {
                IntervalPoint++;
                int Index;
                Index = SearchTinAnagoBackmost() - 1;
                TinAnago_bool[Index] = false;
                HideCalling(Index);
                if (Index - 1 > 0)
                {
                    TimeFrameArray[Index - 1].SetActive(true);
                    TimeTxtArray[Index - 1].GetComponent<ChangeTimeTxt>().ChangeTxt(GameLimitTime - (int)NowPlayTime);
                }
                TimeFrameArray[Index].SetActive(false);
                
                Debug.Log(NowPlayTime);

            }   
    }

    /*チンアナゴ配列の最後尾をさがすよ*/
    int SearchTinAnagoBackmost()
    {
        int i;
        for (i=0; i < TotalOfTinAnago; i++)
        {
            if (TinAnago_bool[i] == true) continue;
            else
            {
                break;
            }
        }
        return i;
        

    }

    //チンアナゴの関数呼び出し
    void IdleCalling(int Index)
    {
        TinAnago_gameObject[Index].GetComponent<TinAnagoMover>().SetTinAnagoState_IDLE();
    }
    void HideCalling(int Index)
    {
        TinAnago_gameObject[Index].GetComponent<TinAnagoMover>().SetTinAnagoState_HIDE();
    }
    void ShowCalling(int Index)
    {
        TinAnago_gameObject[Index].GetComponent<TinAnagoMover>().SetTinAnagoState_SHOW();
    }

    /*時を刻む実行関数*/
    void TickTimer()
    {
        NowPlayTime += TimeSpeed*Time.deltaTime;
    }
    /*制限時間内かチェック*/
    bool CheckLimitTime()
    {
        if (NowPlayTime >= GameLimitTime)
        {
            return false;
        }
        return true;
    }

    //ステートを待機状態に
    public void SetNowTimeManagerState_IDLE() { NowTimeManagerState = TIMEMANAGER_STATE.IDLE; }
    //ステートを時刻状態に
    public void SetNowTimeManagerState_TICK() { NowTimeManagerState = TIMEMANAGER_STATE.TICK; }
    /*現在の時間を渡す*/
    public float GetNowPlayTime() { return NowPlayTime; }
    //制限時間ー経過時間を渡す　　5/28與那覇追加
    public float GetScoreTime() { return GameLimitTime - NowPlayTime; }


    /*Readme*************************************************************************************/
    //先にチンアナゴを並べてね
    //1.空オブジェクトにCSをアタッチする
    //2.Inspector上でTinAnago配列にチンアナゴオブジェクトを格納
    //
    //制限時間を変えるときはGameLimitTimeをいじってね
    //
    //
    /*******************************************************************************************/

}
