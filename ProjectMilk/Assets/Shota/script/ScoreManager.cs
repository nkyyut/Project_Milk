using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    private int nowOniCount;
    private int oldOniCount;

    [SerializeField] OniCount oniCount;

    private int TotalScore;

    private void Start()
    {
        nowOniCount = oldOniCount = oniCount.Count();
        TotalScore = 0;
    }

    private void Update()
    {
        nowOniCount = oniCount.Count();
        if(nowOniCount != oldOniCount)
        {
           TotalScore += AddScore(oldOniCount - nowOniCount);
           oldOniCount = nowOniCount;
        }
    }

    private int AddScore(int difference)
    {
        int rank;
        int score = 0;
        if(difference < 4)
        {
            rank = 0;
        }else if(difference < 6)
        {
            rank = 1;
        }
        else
        {
            rank = 2;
        }

        switch (rank)
        {
            case 0:
                score = difference * 10;
                break;
            case 1:
                score = difference * 20;
                break;
            case 2:
                score = difference * 30;
                break;
            default:
                break;
        }
        return score;
    }

    public int GetOnihitodeScore()
    {
        return TotalScore;
    }
}
