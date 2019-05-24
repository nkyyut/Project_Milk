using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUIManager : MonoBehaviour
{
    public ParticleSystem bubbleEffects;
    private float waitTime;
    private bool startFLG;
    public AudioSource BGMManager;
    public AudioClip bubbleSe;

    public UIGuageMover startButton;
    public UIGuageMover endButton;

    private const float PressLimit = 1.0f;

    private float endPressTime;
    private float startPressTime;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("joystick button 1"))
        {
            startPressTime += Time.deltaTime;
            if (startPressTime <= PressLimit)
            {
                startButton.FillUp(startPressTime);
            }
            else
            {
                startGame();
            }
        }

        if(!Input.GetKey("joystick button 1") && startPressTime <= PressLimit)
        {
            if (startPressTime > 0)
            {
                startPressTime -= Time.deltaTime;
                startButton.SetFillDownFlg();
            }
        }

        if (Input.GetKey("joystick button 0"))
        {
            endPressTime += Time.deltaTime;
            if (endPressTime <= PressLimit)
            {
                endButton.FillUp(endPressTime);
            }
            else
            {
                Application.Quit();
            }
        }

        if (startFLG)
        {
            waitTime += Time.deltaTime;
        }

        if (waitTime > 3)
        {
            FadeManager.Instance.LoadScene("StoryScene", 0.3f);
        }
    }

    /// <summary>
    /// スタートボタンを押した時に、
    /// バブルのエフェクトを起動させ、
    /// エフェクト終了時にシーンを遷移させる処理
    /// </summary>
    public void startGame()
    {
        if (!startFLG)
        {
            BGMManager.PlayOneShot(bubbleSe);
            bubbleEffects.Play();
            startFLG = true;
        }
    }
}
