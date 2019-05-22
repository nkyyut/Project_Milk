using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Info_LookAt : MonoBehaviour {
    public GameObject Camera;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.LookAt(Camera.transform);
      
	}
}
