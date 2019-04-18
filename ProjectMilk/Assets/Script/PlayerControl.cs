using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    [SerializeField] Transform CenterOfBalance;
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

        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    transform.Rotate(
        //        new Vector3(0, -3f, 0),
        //        Space.Self
        //    );
        //}
        //else if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    transform.Rotate(
        //        new Vector3(0, 3f, 0),
        //        Space.Self
        //    );
        //}
        //else if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    transform.position =
        //        transform.position +
        //        (transform.forward * 3 * Time.fixedDeltaTime);
        //}
        //else if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    transform.position =
        //        transform.position +
        //        (transform.forward * 3 * Time.fixedDeltaTime);
        //}

        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 moveForward = cameraForward * InputV + Camera.main.transform.right * InputH;

        TransformChange(moveForward, cameraForward);

        RaycastHit hit;

        if(Physics.Raycast(CenterOfBalance.position,-transform.up,out hit,float.PositiveInfinity))
        {
            Quaternion q = Quaternion.FromToRotation(transform.up, hit.normal);
            transform.rotation *= q;

            if(hit.distance > 0.05f)
            {
                transform.position = transform.position + (-transform.up * Physics.gravity.magnitude * Time.fixedDeltaTime);
            }
        }

    }
    private void TransformChange(Vector3 MForward, Vector3 CForward)
    {
        if (MForward != Vector3.zero)
        {
            transform.position += MForward * movespeed;

            transform.rotation = Quaternion.LookRotation(MForward);

        }

    }



}
