//與儀清仁　2019/4/9
//時間の管理と時間UIの管理
//このCSの一番下にReadmeあるよ
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {
    public GameObject[] TinAnago;//時間UIのチンアナゴを格納
    float GameLimitTime;//ゲームの制限時間
    int TotalOfTinAnago;//チンアナゴの数
    float NowPlayTime;//現在の経過時間
    float TimeSpeed;//時間の進み具合基本1/*使う予定はないが一応*/

    /*時間を刻むか否か*/
    enum TIMEMANAGER_STATE
    {
        IDLE,/*何もしない*/
        TICK,/*時間を進ませる*/
    }TIMEMANAGER_STATE NowTimeManagerState/*現在のステート格納*/;

    // Use this for initialization
    void Start () {
        /*以下いろいろ初期化*/
        TotalOfTinAnago = TinAnago.Length;
        GameLimitTime = 180.0f;
        NowPlayTime = 0.0f;
        NowTimeManagerState = TIMEMANAGER_STATE.IDLE;
        TimeSpeed = 1.0f;
        /*以上いろいろ初期化*/
    }

    // Update is called once per frame
    void Update () {
        /*以下テスト用呼び出し*/
        if (Input.GetMouseButtonUp(0)) SetNowTimeManagerState_TICK();
        if (Input.GetMouseButtonUp(1)) SetNowTimeManagerState_IDLE();
        Debug.Log(NowPlayTime);
        /*以上*/


        Switching();
        
	}

    /*現在のステートによるスイッチング*/
    void Switching()
    {
        switch(NowTimeManagerState)
        {
            case TIMEMANAGER_STATE.IDLE:
                break;
            case TIMEMANAGER_STATE.TICK:
                if (CheckLimitTime())
                {
                    SetNowTimeManagerState_IDLE();
                    break;
                }
                TickTimer();
                break;
        }

    }

    /*チンアナゴの管理*/
    void TinAnagoManager()
    {

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
