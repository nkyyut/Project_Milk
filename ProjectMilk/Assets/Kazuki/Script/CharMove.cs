﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMove : MonoBehaviour {

    GameObject Player;
    float Angle = 3f;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKey(KeyCode.LeftArrow))
        transform.position += new Vector3(-0.1f,0,0);
        if (Input.GetKey(KeyCode.RightArrow))
            transform.position += new Vector3(0.1f, 0, 0);
        if (Input.GetKey(KeyCode.UpArrow))
            transform.position += new Vector3(0, 0, 0.1f);
        if (Input.GetKey(KeyCode.DownArrow))
            transform.position += new Vector3(0f, 0, -0.1f);
    }
}