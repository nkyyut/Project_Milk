using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float MoveSpeed = 15;
    public Vector3 MoveDir;
    Rigidbody rigitbody;
	// Use this for initialization
	void Start () {

        rigitbody = this.gameObject.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        
        MoveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

    }

    void FixedUpdate()
    {
        rigitbody.MovePosition(rigitbody.position + transform.TransformDirection(MoveDir) * MoveSpeed * Time.deltaTime);
    }
}
