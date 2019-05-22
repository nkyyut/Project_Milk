using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalManager : MonoBehaviour {

    public TimeManager TimeManager;
    public DurableValueManager DurableValueManager;
    public PauseManager PauseManager;

    enum TOTAL_MANAGER_STATE
    {
        SET_PAUSE,
        SET_PLAY,
        KEEP
    }TOTAL_MANAGER_STATE NowState;
	// Use this for initialization
	void Start () {
        NowState = TOTAL_MANAGER_STATE.KEEP;
	}
	
	// Update is called once per frame
	void Update () {
        Switching();
        if (Input.GetKeyUp("joystick button 7"))
        {
            SetTotalManagerState_Pause();
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
