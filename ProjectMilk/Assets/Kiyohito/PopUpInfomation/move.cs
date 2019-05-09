using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.UpArrow)){
             float speed = 1;
             Vector3 velocity = gameObject.transform.rotation * new Vector3(0,0,speed);
             gameObject.transform.position += velocity * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            float speed = 1;
            Vector3 velocity = gameObject.transform.rotation * new Vector3(0, 0, -speed);
            gameObject.transform.position += velocity * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            float speed = 1;
            Vector3 velocity = gameObject.transform.rotation * new Vector3(speed, 0, 0);
            gameObject.transform.position += velocity * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            float speed = 1;
            Vector3 velocity = gameObject.transform.rotation * new Vector3(-speed, 0, 0);
            gameObject.transform.position += velocity * Time.deltaTime;
        }

	}
}
