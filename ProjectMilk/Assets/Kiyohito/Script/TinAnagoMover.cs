//與儀清仁　2019/4/9
//チンアナゴの動きを管理
//このCSの一番下にReadmeあるよ
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TinAnagoMover : MonoBehaviour {

    
    float PullBackSpeed;/*引っ込むスピード*/
    float PullBackLimit;/*引っ込む限界値*/

    Vector3 TinAnagoInitialPos;/*チンアナゴの初期位置*/
    RectTransform TinAnagoTra;/*チンアナゴのTransform*/
    Image TinAnagoImg;/*チンアナゴのImage*/

    enum TINANAGO_STATE/*チンアナゴのステート*/
    {
        IDLE,/*待機*/
        HIDE,/*隠れる*/
        SHOW/*出てくる*/
    } TINANAGO_STATE NowTinAnagoState;

    // Use this for initialization
    void Start() {
        Initialize();
    }

    // Update is called once per frame
    void Update() {
        Switching();
    }

    /*いろいろ初期化*/
    void Initialize()
    {
        try
        {
            /*以下いろいろ初期化*/
            PullBackSpeed = 100.0f * Screen.height / KiyohitoConst.Const.FoundationScreenSize;
            PullBackLimit = 60 * Screen.height / KiyohitoConst.Const.FoundationScreenSize;
            TinAnagoInitialPos = this.gameObject.GetComponent<RectTransform>().position;
            NowTinAnagoState = TINANAGO_STATE.IDLE;
            TinAnagoTra = this.gameObject.GetComponent<RectTransform>();
            TinAnagoImg = this.gameObject.GetComponent<Image>();
            /*以上*/
        }
        catch
        {
            Debug.Log("DurableValueManagerInitialize_err");

        }

    }

    //TinAnagoのステートによる動きをマネジメント
    void Switching()
    {
        switch (NowTinAnagoState)
        {
            case TINANAGO_STATE.IDLE:
                break;
            case TINANAGO_STATE.HIDE:
                if (!CheckHyde())
                {
                    SetTinAnagoState_IDLE();
                    break;
                }
                if (CheckPullBackLimit()) PullBacker();
                if (CheckDisappearLimit()) Disappear();
                break;
            case TINANAGO_STATE.SHOW:
                if (!CheckShow())
                {
                    SetTinAnagoState_IDLE();
                    break;
                }
                if (CheckPushOutLimit()) PushOuter();
                if (CheckAppearLimit())Appear();
                break;
        }
    }


    //引っ込めるか？消せるか？のチェック
    //どっちかだめならステートをIDLEにする
    bool CheckHyde()
    {
        if (CheckPullBackLimit() || CheckDisappearLimit()) return true;
        return false;
    }
    //押し出して良いか？出現させて良いか？のチェック
    //どっちかだめならステートをIDLEにする
    bool CheckShow()
    {
        if (CheckPushOutLimit() || CheckAppearLimit()) return true;
        return false;
    }

    //引っ込ませる実行関数
    void PullBacker()
    {
        Vector3 NewPos;
        NewPos = TinAnagoTra.position;
        NewPos.y -= PullBackSpeed*Time.deltaTime;
        TinAnagoTra.position = NewPos;

       
    }
    //引っ込めるか？のチェック
    bool CheckPullBackLimit()
    {
        float PullBackValue;
        PullBackValue = TinAnagoInitialPos.y - TinAnagoTra.position.y;
        if (PullBackValue > PullBackLimit) {
            return false;
        }
        return true;
        
    }

    //押し出せるか？
    bool CheckPushOutLimit()
    {
        if (TinAnagoInitialPos.y > TinAnagoTra.position.y) return true;
        return false;
    }
    //押し出す実行関数
    void PushOuter()
    {
        Vector3 NewPos;
        NewPos = TinAnagoTra.position;
        NewPos.y += PullBackSpeed*Time.deltaTime;
        TinAnagoTra.position = NewPos;
    }
    
    //出現させていいか？
    bool CheckAppearLimit()
    {
        float AppearPercentage;
        AppearPercentage = (TinAnagoInitialPos.y - TinAnagoTra.position.y) / PullBackLimit;
       
        if (AppearPercentage > 0.0f)
        {
            return true;
        }
        return false;
    }
    //出現させる実行関数
    void Appear()
    {
        float AppearPercentage;
        AppearPercentage = 1-(TinAnagoInitialPos.y - TinAnagoTra.position.y) / PullBackLimit;
        TinAnagoImg.fillAmount = AppearPercentage;
    }


    //消していいか？のチェック
    bool CheckDisappearLimit()
    {
        float DisappearPercentage;
        DisappearPercentage = 1.0f - (TinAnagoInitialPos.y - TinAnagoTra.position.y) / PullBackLimit;
        if (DisappearPercentage <= 1.0f)
        {
            return true;
        }
        return false;
    }
    //消す実行関数
    void Disappear()
    {
        float DisappearPercentage;
        DisappearPercentage = 1.0f - (TinAnagoInitialPos.y - TinAnagoTra.position.y) / PullBackLimit;
        TinAnagoImg.fillAmount = DisappearPercentage;
    }




    //TinAnagoのステートセット関数
    public void SetTinAnagoState_IDLE()
    {
        NowTinAnagoState = TINANAGO_STATE.IDLE;
    }
    public void SetTinAnagoState_HIDE()
    {
        NowTinAnagoState = TINANAGO_STATE.HIDE;
    }
    public void SetTinAnagoState_SHOW()
    {
        NowTinAnagoState = TINANAGO_STATE.SHOW;
    }



    /*Readme*************************************************************************************/
    //1.UIオブジェクトで任意でチンアナゴを並べる
    //2.すべてのチンアナゴオブジェクトにこのCSをアタッチする
    //
    //
    //引っ込み方に違和感がある場合はPullBackSpeedとPullBackLimitをいじってね
    //
    //
    /*******************************************************************************************/

}
