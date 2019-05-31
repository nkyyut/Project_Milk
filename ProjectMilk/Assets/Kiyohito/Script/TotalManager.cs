using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalManager : MonoBehaviour {

    // リザルトでポーズが開けてしまうのでフラグを使って修正 5/30與那覇
    private bool isResult;
    public TimeManager TimeManager;
    public DurableValueManager DurableValueManager;
    public PauseManager PauseManager;

    public OniCount onicount;

    enum TOTAL_MANAGER_STATE
    {
        SET_PAUSE,
        SET_PLAY,
        KEEP
    }TOTAL_MANAGER_STATE NowState;
	// Use this for initialization
	void Start () {
        NowState = TOTAL_MANAGER_STATE.KEEP;
        isResult = false;
	}
	
	// Update is called once per frame
	void Update () {
        Switching();
        if(onicount.Count() == 0)
        {
            isResult = true;
        }
        else if (DurableValueManager.GetDurableValue() <= 0)
        {

        }
        if (Input.GetKeyUp("joystick button 7"))
        {
            if (!isResult)
            {
                SetTotalManagerState_Pause();
            }
        }
	}

    void Switching()
    {
        switch (NowState)
        {
            case TOTAL_MANAGER_STATE.SET_PLAY:
                SetPlay();
                SetTotalManagerState_Keep();
                break;
            case TOTAL_MANAGER_STATE.SET_PAUSE:
                SetPause();
                SetTotalManagerState_Keep();
                break;
            case TOTAL_MANAGER_STATE.KEEP:
                break;
            
        }
    }

    public void SetPlay()
    {
        TimeManager.SetNowTimeManagerState_TICK();
        PauseManager.SetPauseManagerState_Idle();
    }

    public void SetPause()
    {
        TimeManager.SetNowTimeManagerState_IDLE();
        PauseManager.SetPauseManagerState_Pause();

    }

    void SetTotalManagerState_Play() { NowState = TOTAL_MANAGER_STATE.SET_PLAY; }
    void SetTotalManagerState_Pause() { NowState = TOTAL_MANAGER_STATE.SET_PAUSE; }
    void SetTotalManagerState_Keep() { NowState = TOTAL_MANAGER_STATE.KEEP; }
}
