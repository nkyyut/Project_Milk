using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PauseManager : MonoBehaviour {

    public GameObject TranslucentPanel;

    enum PAUSE_MANAGER_STATE
    {
        PAUSE,
        IDLE
    }PAUSE_MANAGER_STATE NowState;

	// Use this for initialization
	void Start () {
        NowState = PAUSE_MANAGER_STATE.IDLE;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Switching()
    {
        switch (NowState)
        {
            case PAUSE_MANAGER_STATE.PAUSE:
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
}
