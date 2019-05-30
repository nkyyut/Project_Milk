using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultCalc : MonoBehaviour {

    public GameObject ResultManager;
    public GameObject ResultPanel;

    public OniCount onicount;
    public TimeManager timemanager;
    public DurableValueManager durablevaluemanager;
    public ScoreManager scoremanager;
    
    private bool resultflg;

    private void Start()
    {
        resultflg = false;
    }

    private void Update()
    {
        if(onicount.Count() == 0)
        {
            if (!resultflg)
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

        Score = scoremanager.GetOnihitodeScore() + timemanager.GetScoreTime() + durablevaluemanager.GetDurableValue();

        return (int)Score;
    }
}
