using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultCalc : MonoBehaviour {

    public GameObject ResultManager;
    public GameObject ResultPanel;
    public AnimCon AnimCon;
    public CameraTest CameraTest;
    public OniCount onicount;
    public TimeManager timemanager;
    public DurableValueManager durablevaluemanager;
    public ScoreManager scoremanager;
    
    private bool resultflg;
    bool AnimeStartFlg;
    private void Start()
    {
        AnimeStartFlg = true;
        resultflg = false;
    }

    private void Update()
    {
        if(onicount.Count() == 0)
        {
            if (AnimeStartFlg)
            {
                CameraTest.SetPause();
                timemanager.SetNowTimeManagerState_IDLE();
                AnimCon.PlayClearAnim();
                AnimeStartFlg = false;
            }
            if (AnimeStartFlg==false&&AnimCon.AnimEndCheck()&&!resultflg)
            {
                ResultManager.SetActive(true);
                ResultPanel.SetActive(true);
                ResultManager.GetComponent<ResultScript>().Score = GetResultScore();
                resultflg = true;
                Debug.Log(GetResultScore());
            }
        }
    }

    public int GetResultScore()
    {
        float Score = 0;
        // スコア　＝　オニヒトデ討伐ポイント+-*/　残り時間　+-*/　のこりHP  
        Debug.Log("討伐P"+scoremanager.GetOnihitodeScore());
        Debug.Log("残り時間" + timemanager.GetScoreTime());
        Debug.Log("のこりHP" + durablevaluemanager.GetDurableValue());
        Score = scoremanager.GetOnihitodeScore() + timemanager.GetScoreTime() + durablevaluemanager.GetDurableValue();

        return (int)Score;
    }
}
