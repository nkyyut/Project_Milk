using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EffectUIManager : MonoBehaviour {
    [SerializeField] CanvasGroup CanvasGroup;
    [SerializeField] GameObject EffectUIImage;
    [SerializeField] GameObject EffectUIText;
    [SerializeField] Sprite[] Image;
    [SerializeField] float FadeSpeed;
    float DeltaTime;
    float LimitTime=1;

    enum EFFECT_STATE
    {
        STAY,
        FADEOUT
    }EFFECT_STATE NowEffectState;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {
            SetEffectUI(6);
        }
        Switching();
	}

    void Switching()
    {
        switch (NowEffectState)
        {
            case EFFECT_STATE.STAY:
                Stay();
                break;
            case EFFECT_STATE.FADEOUT:
                FadeOut();
                break;
        }
    }
    
    public void SetEffectUI(int KillCount)
    {
        SetFadeOut();
        if (KillCount > 5)
        {
            SetEffectUIBig();
            ChangeText(KillCount);
        }
        else if (KillCount > 3)
        {
            SetEffectNormal();
            ChangeText(KillCount);
        }
        else if (KillCount >= 1)
        {
            SetEffectSmall();
            ChangeText(KillCount);
        }
        else { Debug.Log("errrrrrrrrrrrrrrrrrrrrrrr"); }
    }

    void SetEffectUIBig()
    {
        Debug.Log("BigIn");
        ChangeSprite(Image[2]);
    }
    void SetEffectNormal()
    {
        Debug.Log("NormalIn");
        ChangeSprite(Image[1]);
    }
    void SetEffectSmall()
    {
        Debug.Log("SmallIn");
        ChangeSprite(Image[0]);
    }

    void ChangeSprite(Sprite NewSprite)
    {
        EffectUIImage.GetComponent<Image>().sprite = NewSprite;
    }
    void ChangeText(int NewNumber)
    {
        EffectUIText.GetComponent<Text>().text = NewNumber.ToString();
    }

    void SetFadeOut()
    {
        CanvasGroup.alpha = 255;
        SetNowEnemyState_STAY();
    }

    void FadeOut()
    {
        CanvasGroup.alpha -= Time.deltaTime * FadeSpeed;
    }
    void Stay()
    {
        DeltaTime += Time.deltaTime;
        if (LimitTime < DeltaTime)
        {
            DeltaTime = 0;
            SetNowEnemyState_FADEOUT();
        }
    }

    void SetNowEnemyState_STAY() { NowEffectState = EFFECT_STATE.STAY; }
    void SetNowEnemyState_FADEOUT() { NowEffectState = EFFECT_STATE.FADEOUT; }
}
