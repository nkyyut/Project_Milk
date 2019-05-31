using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EffectUIManager : MonoBehaviour {
    [SerializeField] CanvasGroup CanvasGroup;
    [SerializeField] GameObject EffectUIImage;
    [SerializeField] GameObject EffectUIText;
    [SerializeField] Sprite[] Image;
    [SerializeField] AudioClip[] AudioClipArray;
    [SerializeField] float FadeSpeed;
    AudioSource AudioSource;
    float DeltaTime;
    float LimitTime=1;

    enum EFFECT_STATE
    {
        STAY,
        FADEOUT
    }EFFECT_STATE NowEffectState;

	// Use this for initialization
	void Start () {
        AudioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
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
        SetAudioClip(AudioClipArray[2]);
        AudioSource.Play();
    }
    void SetEffectNormal()
    {
        Debug.Log("NormalIn");
        ChangeSprite(Image[1]);
        SetAudioClip(AudioClipArray[1]);
        AudioSource.Play();
    }
    void SetEffectSmall()
    {
        Debug.Log("SmallIn");

        ChangeSprite(Image[0]);
        SetAudioClip(AudioClipArray[0]);
        AudioSource.Play();
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

    void SetAudioClip(AudioClip NextSound)
    {
        AudioSource.clip = NextSound;
    }

    void SetNowEnemyState_STAY() { NowEffectState = EFFECT_STATE.STAY; }
    void SetNowEnemyState_FADEOUT() { NowEffectState = EFFECT_STATE.FADEOUT; }
}
