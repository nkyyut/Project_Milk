using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    Vector3 target;
    Vector3 Gravity;
    
    public GameObject chilld;

    public bool gravityFlg = false;
    [SerializeField] bool colliderFlg = false;
    float movespeed = 0.01f;



	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        
        float InputH = Input.GetAxisRaw("Horizontal");
        float InputV = Input.GetAxisRaw("Vertical");

       // Debug.Log(InputH + "          " + InputV);

        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 moveForward = cameraForward * InputV + Camera.main.transform.right * InputH;
        if (gravityFlg == true)
        {
            cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 1, 1)).normalized;
            moveForward = cameraForward * InputV + Camera.main.transform.right * InputH;
        }

        if (gravityFlg == true)
        {
            if (moveForward != Vector3.zero)
                GravityChang();

        }
        if (colliderFlg == false)
            transform.position += new Vector3(0, -0.098f, 0);
        else if (colliderFlg == true && gravityFlg == true)
        {
            transform.position += new Vector3((target.x - transform.position.x) * 2 * 0.001f,0, (target.z - transform.position.z) * 2 * 0.001f) ;
        }
        TransformChange(moveForward);
        
    }
    private void TransformChange(Vector3 MForward)
    {
        if (MForward != Vector3.zero)
        {
            transform.position += MForward * movespeed;
            transform.rotation = Quaternion.LookRotation(MForward);

        }
     //   else Debug.Log(Physics.gravity.z);
        
    }
    private void GravityChang()
    {
       
        Gravity = target - transform.position;

        

        Physics.gravity = new Vector3(Gravity.x,(target.y - Gravity.y),Gravity.z);
        //Debug.Log(Gravity.y + "    " + transform.position.y + "     " + Physics.gravity.y);
       // Debug.Log(Gravity.x + "    " + transform.position.x + "     " + target.x + "     " + Physics.gravity.x);
        //if (Physics.gravity.y == transform.position.y)
        //    Debug.Log("一緒");
        //else Debug.Log("違う");
        //Debug.Log(Physics.gravity);

    }
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "area" && gravityFlg == false)
        {
            target = other.transform.position;
            gravityFlg = true;
           
        }
        colliderFlg = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        colliderFlg = false;
    }

}
