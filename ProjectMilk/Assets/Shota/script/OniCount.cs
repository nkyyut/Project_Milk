using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OniCount : MonoBehaviour {

    [SerializeField] Text OnihitodeNum;
    [SerializeField] float interval; // 更新タイミング

    float Timer;

    GameObject[] Onihitode;

    int kiritoriPhase = 0;


    private void Start()
    {
        OnihitodeNum.text = Count().ToString();
    }

    private void FixedUpdate()
    {
        if (Input.GetAxis("RT_Botton") == -1)
        {
            kiritoriPhase = 1;
        }
        if(Input.GetAxis("RT_Botton") == 0)
        {
            if (kiritoriPhase == 1)
            {
                kiritoriPhase = 2;
            }
        }

        if (kiritoriPhase == 2)
        {
            Timer += Time.deltaTime;
            if(Timer > interval)
            {
                OnihitodeNum.text = Count().ToString();
                Timer = 0;
                kiritoriPhase = 0;
            }
        }
    }

    private int Count()
    {
        Onihitode = GameObject.FindGameObjectsWithTag("Onihitode");

        return Onihitode.Length;
    }
}
