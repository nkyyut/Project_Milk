using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIGuageMover : MonoBehaviour {
    bool FillDownFlg;
	// Use this for initialization
	void Start () {
        FillDownFlg = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (FillDownFlg)
        {
            FillDown();
            if (this.gameObject.GetComponent<Image>().fillAmount <= 0)
            {
                FillDownFlg = true;
            }
        }
	}

    public void FillUp(float NowPressTime)
    {
        this.gameObject.GetComponent<Image>().fillAmount = (NowPressTime/KiyohitoConst.Const.PressTimeLimit);
    }
    public void FillDown()
    {
        this.gameObject.GetComponent<Image>().fillAmount-=1*Time.deltaTime;
        
    }

    public void SetFillDownFlg()
    {
        FillDownFlg = true;
    }
}
