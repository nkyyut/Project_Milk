using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OniCount : MonoBehaviour {

    [SerializeField] Text OnihitodeNum;
    [SerializeField] float interval; // 更新タイミング

    float Timer;

    GameObject[] Onihitode;

    bool isKiritori = false;


    private void Start()
    {
        OnihitodeNum.text = Count().ToString();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            isKiritori = true;
        }

        if (isKiritori)
        {
            Timer += Time.deltaTime;
            if(Timer > interval)
            {
                OnihitodeNum.text = Count().ToString();
                Timer = 0;
                isKiritori = false;
            }
        }

    }

    private int Count()
    {
        Onihitode = GameObject.FindGameObjectsWithTag("Onihitode");

        return Onihitode.Length;
    }
}
