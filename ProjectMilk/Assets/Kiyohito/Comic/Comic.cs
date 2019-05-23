using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comic : MonoBehaviour {
    [SerializeField] Sprite[] Page;
    float NowPage;
	// Use this for initialization
	void Start () {
        NowPage = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
