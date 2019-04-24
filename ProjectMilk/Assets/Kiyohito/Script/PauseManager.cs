using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PauseManager : MonoBehaviour {

    public GameObject TranslucentPanel;
    bool StickBeatFlg;//連続入力回避用フラグ
    int ChoiceNumber;
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
        ChoiceNumber = 0;
        NowChoice = PAUSE_MENU.PLAY;
        StickBeatFlg = true;//スティックで選択する際の管理フラグ
    }
	
	// Update is called once per frame
	void Update () {
        Switching();
        //Debug.Log("NowChoiceNumber"+ChoiceNumber);
       Debug.Log(NowChoice);
	}

    void Switching()
    {
        switch (NowState)
        {
            case PAUSE_MANAGER_STATE.PAUSE:
                MenuChoice();
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

    void MenuChoice()
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
}
