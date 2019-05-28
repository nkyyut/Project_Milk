using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OniCount : MonoBehaviour {

    [SerializeField] Text OnihitodeNum;
    GameObject[] Onihitode;

    private void Start()
    {
        OnihitodeNum.text = Count().ToString();
    }

    private void FixedUpdate()
    {
        OnihitodeNum.text = Count().ToString();
    }

    public int Count()
    {
        Onihitode = GameObject.FindGameObjectsWithTag("Onihitode");

        return Onihitode.Length;
    }
}
