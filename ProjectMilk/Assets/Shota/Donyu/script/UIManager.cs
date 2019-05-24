using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    private Color alphaColor;

    private float helfTransparent = 64; // 透過度合い
    private float unTransparent = 255; // 無透過

    private bool isTransparent = true;

    private const float PressLimit = 1.0f;
    private float BackPressTime = 0.0f;
    private float NextPressTime = 0.0f;
    private float SkipPressTime = 0.0f;

    public PageManager pageManager;
    public CutSceneManager cutscenemanager;

    public Image[] Buttom;

    public Sprite[] BbuttomImage;
    public UIGuageMover SkipUIGuage;
    public UIGuageMover NextUIGuage;
    public UIGuageMover BackUIGuage;


    private void Update()
    {
        if (isTransparent)
        {
            AllTransparentOn();
        }
        if(!isTransparent)
        {
            AllTransparentOff();
        }

        if(pageManager.GetNowPage() == 3)
        {
            Buttom[3].sprite = BbuttomImage[1];
        }
        else
        {
            Buttom[3].sprite = BbuttomImage[0];
        }

        if(Input.GetKey("joystick button 0"))
        {
            SetTransparentFlg(false);
            BackPressTime += Time.deltaTime;
            if (BackPressTime <= PressLimit)
            {
                BackUIGuage.FillUp(BackPressTime);
            }
            else
            {
                if(pageManager.GetNowPage() != 0)
                {
                    pageManager.PageBack();
                    ParametersReset();
                }
            }
        }
        if (Input.GetKey("joystick button 1"))
        {
            SetTransparentFlg(false);
            NextPressTime += Time.deltaTime;
            if (NextPressTime <= PressLimit)
            {
                NextUIGuage.FillUp(NextPressTime);
            }
            else
            {
                if(pageManager.GetNowPage() == 3)
                {
                    LoadMainScene();
                }
                pageManager.PageNext();
                ParametersReset();
            }
        }
        if (Input.GetKey("joystick button 2"))
        {
            SetTransparentFlg(false);
            SkipPressTime += Time.deltaTime;
            if (SkipPressTime <= PressLimit)
            {
                SkipUIGuage.FillUp(SkipPressTime);
            }
            else
            {
                TimeReset();
                LoadMainScene();
            }
        }
        if (!Input.GetKey("joystick button 0")&&
            !Input.GetKey("joystick button 1")&&
            !Input.GetKey("joystick button 2"))
        {
            SetTransparentFlg(true);
        }

        if (!Input.GetKey("joystick button 0"))
        {
            if (BackPressTime > 0)
            {
                BackUIGuage.SetFillDownFlg();
                BackPressTime -= Time.deltaTime;
            }
        }
        if (!Input.GetKey("joystick button 1"))
        {
            if (NextPressTime > 0)
            {
                NextUIGuage.SetFillDownFlg();
                NextPressTime -= Time.deltaTime;
            }
        }
        if (!Input.GetKey("joystick button 2"))
        {
            if (SkipPressTime > 0)
            {
                SkipUIGuage.SetFillDownFlg();
                SkipPressTime -= Time.deltaTime;
            }
        }
    }

    public void TransparentOff(Image img)
    {
        alphaColor = img.color;
        alphaColor.a = unTransparent/255;
        img.GetComponent<Image>().color = alphaColor;
    }
    public void TransparentOn(Image img)
    {
        alphaColor = img.color;
        if (alphaColor.a > helfTransparent/255)
        {
            alphaColor.a -= Time.deltaTime;
        }
        img.GetComponent<Image>().color = alphaColor;
    }

    public void AllTransparentOff()
    {
        for (int i = 0; i < Buttom.Length; i++)
        {
            TransparentOff(Buttom[i]);
        }
    }

    public void AllTransparentOn()
    {
        for(int i = 0; i < Buttom.Length; i++)
        {
            TransparentOn(Buttom[i]);
        }
    }

    private void TimeReset()
    {
        BackPressTime = 0.0f;
        NextPressTime = 0.0f;
        SkipPressTime = 0.0f;
    }

    private void SetTransparentFlg(bool b)
    {
        isTransparent = b;
    }

    private void LoadMainScene()
    {
        Debug.Log("メインへ遷移");
        FadeManager.Instance.LoadScene("GameMainScene", 0.5f);
    }

    private void ParametersReset()
    {
        TimeReset();
        pageManager.FlowReset();
        cutscenemanager.InitializeTime();
    }
}
