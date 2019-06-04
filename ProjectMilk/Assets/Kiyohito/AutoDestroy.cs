using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
        float PlayTime = this.gameObject.GetComponent<AudioSource>().time;
        Debug.Log("PlayTime"+PlayTime);
        GameObject.Destroy(this.gameObject,PlayTime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
