using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine;

public class PauseManager : MonoBehaviour {

    
    public SceneTransition ST_ToTitle;
    public TotalManager TotalManager;
    public GameObject TranslucentPanel;
    /////////////////////         A                     B                    X                Y///////////////
    string[] KeyNameArray={ "JoystickButton0", "JoystickButton1", "JoystickButton2", "JoystickButton3" };
    float PressTime;
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
        PressTime=0;
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
    }
    public void SetPauseManagerState_Idle()
    {
        NowState = PAUSE_MANAGER_STATE.IDLE;
        ChangeColorToTransparent();
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
        if (Input.anyKey)//なにか押されたら
        {
            //それがAボタンなら
            if (Input.GetKey("joystick button 0"))
            {
                PressTime += Time.deltaTime;
                //長押しされたらPlayへ
                if (PressTime >= KiyohitoConst.Const.PressTimeLimit)
                {
                    Debug.Log("in");
                    PressTime = 0;
                    TotalManager.SetPlay();
                }
            }
            //それがBボタンなら
            else if (Input.GetKey("joystick button 1"))
            {
                PressTime += Time.deltaTime;
                //長押しされたら""へ
                if (PressTime >= KiyohitoConst.Const.PressTimeLimit)
                {
                    Debug.Log("in");
                    PressTime = 0;
                }
            }
            //それがXボタンなら
            else if (Input.GetKey("joystick button 2"))
            {
                PressTime += Time.deltaTime;
                //長押しされたらタイトルへ
                if (PressTime >= KiyohitoConst.Const.PressTimeLimit)
                {
                    Debug.Log("in");
                    PressTime = 0;
                    ST_ToTitle.Transition();
                }
            }
            //それがYボタンなら
            else if (Input.GetKey("joystick button 3"))
            {
                PressTime += Time.deltaTime;
                //長押しされたら""へ
                if (PressTime >= KiyohitoConst.Const.PressTimeLimit)
                {
                    Debug.Log("in");
                    PressTime = 0;
                }
            }
            

        }
        else
        {
            //離されたら押し時間を初期化
            PressTime = 0;
        }


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
