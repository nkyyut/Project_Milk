using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
[DefaultExecutionOrder(-1)]
public class GameOverManager : MonoBehaviour {
    
    [SerializeField] GameObject LoseFrame;
    [SerializeField] GameObject TitleUIGuage;
    [SerializeField] GameObject ReTryUIGuage;
    [SerializeField] SceneTransition ST_ToTitle;
    [SerializeField] CanvasGroup CanvasGroup;
    [SerializeField] GameOver GameOver;
    [SerializeField] CameraTest CameraTest;
    [SerializeField] AnimCon AnimCon;
    float BPressTime;
    float YPressTime;
    bool OnePass;
    float Delta;
    public  bool GameOverStartFlg;
    enum GAMEOVER_STATE
    {
        IDLE,
        COVER_ANIMATION,
        FADE_IN,
        INPUT_RECEPTION
    }GAMEOVER_STATE Now_State;

	// Use this for initialization
	void Start () {
        //Now_State = GAMEOVER_STATE.INPUT_RECEPTION;
        OnePass = true;
        GameOverStartFlg = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonUp(0))
        {
            SetGameOverStart();
        }

        if (GameOver.finish&&OnePass)
        {
            SetNowState_FADE_IN();
            OnePass = false;
        }

        Switching();
    }

    void Switching()
    {
        switch (Now_State)
        {
            case GAMEOVER_STATE.IDLE:
                break;
            case GAMEOVER_STATE.COVER_ANIMATION:
                CoverAnimation();
                break;
            case GAMEOVER_STATE.FADE_IN:
                FadeIn();
                break;
            case GAMEOVER_STATE.INPUT_RECEPTION:
                InputReception();
                break;
        }
    }

    void FadeIn()
    {
        if (Input.anyKey)
        {
            CanvasGroup.alpha = 1;
            Delta = 0;
            SetNowState_INPUT_RECEPTION();
        }
        CanvasGroup.alpha += Time.deltaTime;
        Delta += Time.deltaTime;
        if (Delta >= 1)
        {
            Delta = 0;

            SetNowState_INPUT_RECEPTION();
        }
    }

    void CoverAnimation()
    {
        if (AnimCon.AnimEndCheck())
        {
            GameOver.SetGameOver();
        }
    }

    void InputReception()
    {

        if (Input.GetKeyUp("joystick button 1"))
        {
            PressTimeInitialize();
            TitleUIGuage.GetComponent<UIGuageMover>().SetFillDownFlg();
        }
        else if (Input.GetKeyUp("joystick button 3"))
        {
            PressTimeInitialize();
            ReTryUIGuage.GetComponent<UIGuageMover>().SetFillDownFlg();
        }

        if (Input.GetKey("joystick button 1"))
        {
            BPressTime += Time.deltaTime;
            Debug.Log("in");
            TitleUIGuage.GetComponent<UIGuageMover>().FillUp(BPressTime);
            //長押しされたら""へ
            if (BPressTime >= KiyohitoConst.Const.PressTimeLimit)
            {
                TitleUIGuage.GetComponent<UIGuageMover>().FillUp(BPressTime);
                PressTimeInitialize();
                ST_ToTitle.Transition();
            }
        }
        else if (Input.GetKey("joystick button 3"))
        {
            YPressTime += Time.deltaTime;
            ReTryUIGuage.GetComponent<UIGuageMover>().FillUp(YPressTime);
            //長押しされたら""へ
            if (YPressTime >= KiyohitoConst.Const.PressTimeLimit)
            {
                PressTimeInitialize();
                YPressTime = 0;
            }
        }
    }
    void PressTimeInitialize()
    {
        //離されたら押し時間を初期化
        BPressTime = 0;
        YPressTime = 0;
    }
    public void SetNowState_IDLE() { Now_State = GAMEOVER_STATE.IDLE;}
    public void SetNowState_COVER_ANIMATION(){ Now_State = GAMEOVER_STATE.COVER_ANIMATION;}
    public void SetNowState_FADE_IN(){ Now_State = GAMEOVER_STATE.FADE_IN; }
    public void SetNowState_INPUT_RECEPTION(){ Now_State = GAMEOVER_STATE.INPUT_RECEPTION; }

    public void SetGameOverStart()
    {
        CameraTest.SetPause();
        //Debug.Log("In");
        AnimCon.PlayGameOverAnim();
        SetNowState_COVER_ANIMATION();
        GameOverStartFlg = true;
    }
}
