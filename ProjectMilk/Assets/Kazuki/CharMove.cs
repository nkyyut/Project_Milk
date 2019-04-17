using System.Collections;
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
        transform.Rotate(0,Angle,0);
	}
}
