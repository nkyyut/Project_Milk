using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TinAnagoMover : MonoBehaviour {

    
    float PullBackSpeed;
    float PullBackLimit;

    Vector3 TinAnagoInitialPos;/*チンアナゴの初期位置*/

    enum TinAnagoState
    {
        IDLE,
        HIDE,
        SHOW
    } TinAnagoState NowTinAnagoState;

    // Use this for initialization
    void Start() {
        PullBackSpeed = 1.0f;
        PullBackLimit = 20.0f;
        TinAnagoInitialPos = this.transform.position;
        NowTinAnagoState = TinAnagoState.IDLE;
    }

    // Update is called once per frame
    void Update() {
        TinAnagoMoveManegement();
        if (Input.GetMouseButton(1)) SetTinAnagoState_HIDE();
        if (Input.GetMouseButton(0)) SetTinAnagoState_SHOW();
    }

    //TinAnagoのステートによる動きをマネジメント
    void TinAnagoMoveManegement()
    {
        switch (NowTinAnagoState)
        {
            case TinAnagoState.IDLE:
                break;
            case TinAnagoState.HIDE:
                if (!CheckHyde())
                {
                    SetTinAnagoState_IDLE();
                    break;
                }
                if (CheckPullBackLimit()) PullBacker();
                if (CheckDisappearLimit()) Disappear();
                break;
            case TinAnagoState.SHOW:
                Debug.Log("Push" + CheckPushOutLimit());
                Debug.Log("App" + CheckAppearLimit());
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
        NewPos = this.transform.position;
        NewPos.y -= PullBackSpeed;
        this.gameObject.transform.position = NewPos;

       
    }
    //引っ込めるか？のチェック
    bool CheckPullBackLimit()
    {
        float PullBackValue;
        PullBackValue = TinAnagoInitialPos.y - this.transform.position.y;
        if (PullBackValue > PullBackLimit) {
            return false;
        }
        return true;
        
    }

    //押し出せるか？
    bool CheckPushOutLimit()
    {
        if (TinAnagoInitialPos.y > this.gameObject.transform.position.y) return true;
        return false;
    }
    //押し出す実行関数
    void PushOuter()
    {
        Vector3 NewPos;
        NewPos = this.transform.position;
        NewPos.y += PullBackSpeed;
        this.gameObject.transform.position = NewPos;
    }
    
    //出現させていいか？
    bool CheckAppearLimit()
    {
        float AppearPercentage;
        AppearPercentage = (TinAnagoInitialPos.y - this.transform.position.y) / PullBackLimit;
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
        AppearPercentage = 1-(TinAnagoInitialPos.y - this.transform.position.y) / PullBackLimit;
        Debug.Log("AppearPer" + AppearPercentage);
        this.gameObject.GetComponent<Image>().fillAmount = AppearPercentage;
    }


    //消していいか？のチェック
    bool CheckDisappearLimit()
    {
        float DisappearPercentage;
        DisappearPercentage = 1.0f - (TinAnagoInitialPos.y - this.transform.position.y) / PullBackLimit;
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
        DisappearPercentage = 1.0f - (TinAnagoInitialPos.y - this.transform.position.y) / PullBackLimit;
        this.gameObject.GetComponent<Image>().fillAmount = DisappearPercentage;
    }




    //TinAnagoのステートセット関数
    public void SetTinAnagoState_IDLE()
    {
        NowTinAnagoState = TinAnagoState.IDLE;
    }
    public void SetTinAnagoState_HIDE()
    {
        NowTinAnagoState = TinAnagoState.HIDE;
    }
    public void SetTinAnagoState_SHOW()
    {
        NowTinAnagoState = TinAnagoState.SHOW;
    }
}
