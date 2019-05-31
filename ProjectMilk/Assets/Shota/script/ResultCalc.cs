// 5/31 玉那覇臣　BGM切り替え
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultCalc : MonoBehaviour
{

    public GameObject ResultManager;
    public GameObject ResultPanel;
    public GameObject _BGMManager;

    public OniCount onicount;
    public TimeManager timemanager;
    public DurableValueManager durablevaluemanager;
    public ScoreManager scoremanager;

    [SerializeField] private AudioClip _ResultBGM;

    private AudioSource _audiosource_BGM;

    private bool resultflg;

    private void Start()
    {
        resultflg = false;
        _audiosource_BGM = _BGMManager.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (onicount.Count() == 0)
        {
            if (!resultflg)
            {
                ResultManager.SetActive(true);
                ResultPanel.SetActive(true);
                ResultManager.GetComponent<ResultScript>().Score = GetResultScore();
                resultflg = true;
                Debug.Log(GetResultScore());
                _audiosource_BGM.Stop();
                Invoke("OnResultBGM" , 2.0f);
            }
        }
    }

    public int GetResultScore()
    {
        float Score = 0;
        // スコア　＝　オニヒトデ討伐ポイント+-*/　残り時間　+-*/　のこりHP  
        Debug.Log("討伐P" + scoremanager.GetOnihitodeScore());
        Debug.Log("残り時間" + timemanager.GetScoreTime());
        Debug.Log("のこりHP" + durablevaluemanager.GetDurableValue());
        Score = scoremanager.GetOnihitodeScore() + timemanager.GetScoreTime() + durablevaluemanager.GetDurableValue();

        return (int)Score;
    }

    public void OnResultBGM()
    {
        _audiosource_BGM.clip = _ResultBGM;
        _audiosource_BGM.Play();
    }
}
