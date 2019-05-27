using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Filter_Fade : MonoBehaviour {

    [SerializeField] float MaxFade = 128;
    [SerializeField] float MinFade = 0;

    private Color color;

    bool isFade = false;

    private void Start()
    {
        color = this.gameObject.GetComponent<Image>().color;
    }

    private void Update()
    {
        if (isFade)
        {
            FadeOn();
        }
        else
        {
            FadeOff();
        }
    }

    public void FadeOn()
    {
        if (color.a < MaxFade/255)
        {
            color.a += Time.deltaTime;
        }
        this.gameObject.GetComponent<Image>().color = color;
    }

    public void FadeOff()
    {
        color.a = MinFade;
        this.gameObject.GetComponent<Image>().color = color;
    }

    public void SetFadeFlg(bool b )
    {
        isFade = b;
    }
}
