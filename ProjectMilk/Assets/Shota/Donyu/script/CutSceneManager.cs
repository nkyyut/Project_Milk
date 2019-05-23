using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneManager : MonoBehaviour {

    public float ViewTime = 1.0f;
    public float FadeStayTime = 0.5f;
    public float ReadTextTime = 0.3f;
    public float ElapsedTime = 0.0f;

    public PageManager pagemanager;

    private void Update()
    {
        flow();
    }

    private void flow()
    {

        switch (pagemanager.GetFlow())
        {
            case 0:
                if (ElapsedTime < ViewTime)
                {
                    CountTime();
                }
                else
                {
                    InitializeTime();
                    pagemanager.FlowNext();
                }
                break;
            case 1:
                if (ElapsedTime < FadeStayTime)
                {
                    CountTime();
                }
                else
                {
                    InitializeTime();
                    pagemanager.FlowNext();
                }
                break;
            case 2:
                if (ElapsedTime < ReadTextTime)
                {
                    CountTime();
                }
                else
                {
                    InitializeTime();
                    pagemanager.FlowNext();
                }
                break;
            default:
                break;
        }
    }

    private void CountTime()
    {
        ElapsedTime += Time.deltaTime;
    }

    public void InitializeTime()
    {
        ElapsedTime = 0.0f;
    }
}
