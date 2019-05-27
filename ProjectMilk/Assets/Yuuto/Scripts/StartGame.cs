using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

    public ParticleSystem bubbleEffects;
    private float waitTime;
    private bool startFLG;
    public AudioSource BGMManager;
    public AudioClip bubbleSe;

	// Use this for initialization
	void Start () {
        waitTime = 0.0f;
        startFLG = false;
	}
	
	// Update is called once per frame
	void Update () {
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
        if (startFLG)
        {
            waitTime += Time.deltaTime;
        }

        if (waitTime > 3)
        {
            FadeManager.Instance.LoadScene("StoryScene", 0.3f);
        }
    }
}
