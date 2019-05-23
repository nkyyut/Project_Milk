using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float MoveSpeed = 15;
    public Vector3 MoveDir;
    Rigidbody rigitbody;

    //かずき追加
    private bool PmoveFlg = false;
    public bool chang { get { return PmoveFlg; } }
    // Use this for initialization
    void Start () {

        rigitbody = this.gameObject.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        
        MoveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        if(Input.GetAxis("Horizontal") + Input.GetAxis("Vertical") != 0)
            PmoveFlg = true;
        else
            PmoveFlg = false;
    }

    void FixedUpdate()
    {
        rigitbody.MovePosition(rigitbody.position + transform.TransformDirection(MoveDir) * MoveSpeed * Time.deltaTime);
    }
}
