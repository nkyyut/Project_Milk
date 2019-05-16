using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine;

public class PauseManager : MonoBehaviour {
    Input LogButton;
    public GameObject PauseUI_Canvas;
    public UIGuageMover HelpUIGuage;
    public UIGuageMover BackUIGuage;
    public UIGuageMover TitleUIGuage;
    public UIGuageMover ReTryUIGuage;
    public SceneTransition ST_ToTitle;
    public TotalManager TotalManager;
    public GameObject TranslucentPanel;
    /////////////////////         A                     B                    X                Y///////////////
    string[] KeyNameArray={ "JoystickButton0", "JoystickButton1", "JoystickButton2", "JoystickButton3" };
    float APressTime;
    float BPressTime;
    float XPressTime;
    float YPressTime;
    bool StickBeatFlg;//連続入力回避用フラグ
    enum PAUSE_MANAGER_STATE
    {
        PAUSE,
        IDLE
    }PAUSE_MANAGER_STATE NowState;

    enum PAUSE_MENU
    {
        PLAY,
        RETRY,
        TOTITLE,
        HELP,



        TOTAL
    }PAUSE_MENU NowChoice;

	// Use this for initialization
	void Start () {
        NowState = PAUSE_MANAGER_STATE.IDLE;
        NowChoice = PAUSE_MENU.PLAY;
        APressTime = 0;
        BPressTime = 0;
        XPressTime = 0;
        YPressTime = 0;
        StickBeatFlg = true;//スティックで選択する際の管理フラグ
    }
	
	// Update is called once per frame
	void Update () {
        Switching();
        //Debug.Log(NowChoice);
        //Debug.Log(Input.GetKey("joystick button 0"));
	}

    void Switching()
    {
        switch (NowState)
        {
            case PAUSE_MANAGER_STATE.PAUSE:
                LongPressVersion();
                break;
            case PAUSE_MANAGER_STATE.IDLE:
                break;
        }
    }

    public void SetPauseManagerState_Pause()
    {
        NowState = PAUSE_MANAGER_STATE.PAUSE;
        ChangeColorToTranslucent();
        PauseUI_Canvas.SetActive(true);
    }
    public void SetPauseManagerState_Idle()
    {
        NowState = PAUSE_MANAGER_STATE.IDLE;
        ChangeColorToTransparent();
        PauseUI_Canvas.SetActive(false);
    }

    void ChangeColorToTranslucent()
    {
        Color NewColor = TranslucentPanel.GetComponent<Image>().color;
        NewColor.a = KiyohitoConst.Const.TranslucentValue;
        TranslucentPanel.GetComponent<Image>().color = NewColor;
    }
    void ChangeColorToTransparent()
    {
        Color NewColor = TranslucentPanel.GetComponent<Image>().color;
        NewColor.a = 0;
        TranslucentPanel.GetComponent<Image>().color = NewColor;
    }

    void Choose()
    {
        float InclineH = Input.GetAxis("Horizontal");
        
        if (StickBeatFlg) {
            if (Mathf.Abs(InclineH) >= KiyohitoConst.Const.InclineRatio)
            {
                NowChoice += (int)Mathf.Sign(InclineH);
                StickBeatFlg = false;
            }
        }
        else
        {
            if(Mathf.Abs(InclineH) <= 1-KiyohitoConst.Const.InclineRatio)
            {
                StickBeatFlg = true;
            }
        }

        if (NowChoice >= PAUSE_MENU.TOTAL)
        {
            NowChoice = PAUSE_MENU.HELP;
        }
        else if(NowChoice <0)
        {
            NowChoice = PAUSE_MENU.PLAY;
        }
        
    }
    void Take()
    {
        if(Input.GetKeyUp("joystick button 0"))
        {
            switch (NowChoice)
            {
                case PAUSE_MENU.PLAY:
                    TotalManager.SetPlay();
                    break;
                case PAUSE_MENU.RETRY:
                    break;
                case PAUSE_MENU.TOTITLE:
                    ST_ToTitle.Transition();
                    break;
                case PAUSE_MENU.HELP:
                    break;
            }
        }
    }

    void ChoiceVersion()
    {
        Choose();
        Take();
    }

    void LongPressVersion()
    {
        if (Input.GetKeyUp("joystick button 0"))
        {
            PressTimeInitialize();
            BackUIGuage.SetFillDownFlg();
        }
        else if (Input.GetKeyUp("joystick button 1"))
        {
            PressTimeInitialize();
            TitleUIGuage.SetFillDownFlg();
        }
        else if (Input.GetKeyUp("joystick button 2"))
        {
            PressTimeInitialize();
            HelpUIGuage.SetFillDownFlg();
        }
        else if (Input.GetKeyUp("joystick button 3"))
        {
            PressTimeInitialize();
            ReTryUIGuage.SetFillDownFlg();
        }


        //それがAボタンなら
        if (Input.GetKey("joystick button 0"))
        {
            APressTime += Time.deltaTime;
            BackUIGuage.FillUp(APressTime);
            //長押しされたらPlayへ
            if (APressTime >= KiyohitoConst.Const.PressTimeLimit)
            {
                PressTimeInitialize();
                BackUIGuage.FillUp(0);
                TotalManager.SetPlay();
            }
        }

        //それがBボタンなら
        else if (Input.GetKey("joystick button 1"))
        {
            BPressTime += Time.deltaTime;
            TitleUIGuage.FillUp(BPressTime);
            //長押しされたら""へ
            if (BPressTime >= KiyohitoConst.Const.PressTimeLimit)
            {
                TitleUIGuage.FillUp(BPressTime);
                PressTimeInitialize();
                ST_ToTitle.Transition();
            }
        }
        //それがXボタンなら
        else if (Input.GetKey("joystick button 2"))
        {
            XPressTime += Time.deltaTime;
            HelpUIGuage.FillUp(XPressTime);
            //長押しされたらタイトルへ
            if (XPressTime >= KiyohitoConst.Const.PressTimeLimit)
            {
                PressTimeInitialize();
                XPressTime = 0;

            }
        }
        //それがYボタンなら
        else if (Input.GetKey("joystick button 3"))
        {
            YPressTime += Time.deltaTime;
            ReTryUIGuage.FillUp(YPressTime);
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
        APressTime = 0;
        BPressTime = 0;
        XPressTime = 0;
        YPressTime = 0;
    }

    void DownKeyCheck()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode code in Enum.GetValues(typeof(KeyCode)))
            {

                if (Input.GetKeyDown(code))
                {
                    for (int i = 0; i < KeyNameArray.Length; i++)
                    {
                        if (code.ToString() == KeyNameArray[i])
                        {
                            Debug.Log(code);
                        }
                    }
                    break;
                }
            }
            
        }
    }
}
