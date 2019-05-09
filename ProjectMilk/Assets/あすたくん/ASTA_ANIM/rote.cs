using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rote : MonoBehaviour {

    public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.Rotate(0, speed * Time.deltaTime, 0);
	}
}
