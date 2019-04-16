using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    Vector3 target;
    Vector3 Gravity;

    public GameObject chilld;

    public bool gravityFlg = false;
    [SerializeField] bool colliderFlg = false;
    float movespeed = 0.01f;



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
        if (colliderFlg == false && gravityFlg == false)
            transform.position += new Vector3(0, -0.098f, 0);
        else if (colliderFlg == false && gravityFlg == true)
        {
            transform.position += new Vector3((target.x - transform.position.x) * 2 * 0.005f, 0, (target.z - transform.position.z) * 2 * 0.005f);
        }
        TransformChange(moveForward, cameraForward);

    }
    private void TransformChange(Vector3 MForward, Vector3 CForward)
    {
        if (MForward != Vector3.zero)
        {
            transform.position += MForward * movespeed;
            if (gravityFlg == false)
                transform.rotation = Quaternion.LookRotation(MForward);
            else
            {
                transform.rotation = Quaternion.LookRotation(MForward);
                transform.Rotate(0, 0, 90);
            }
        }
        //   else Debug.Log(Physics.gravity.z);

    }
    private void GravityChang()
    {

        Gravity = target - transform.position;
        Physics.gravity = new Vector3(Gravity.x, (target.y - Gravity.y), Gravity.z);


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
