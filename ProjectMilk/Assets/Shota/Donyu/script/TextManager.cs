using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour {

    public PageManager pagemanager;

    public Text[] text;

    private Color fadecolor;

    private bool DrawFlg = false;

    private void Start()
    {
        fadecolor = text[0].color;
    }
    
    public void FadeOn(int cutnum)
    {
        if(fadecolor.a < 255/255)
        {
            fadecolor.a += Time.deltaTime;
        }
        text[cutnum].color = fadecolor;
    }

    public void FadeOff(int cutnum)
    {
        fadecolor.a = 0.0f;
        text[cutnum].color = fadecolor;
    }

    public void SetDrawFlg(bool b)
    {
        DrawFlg = b;
    }
}
