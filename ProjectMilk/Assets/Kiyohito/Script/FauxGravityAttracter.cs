//與儀清仁　2019年/4/17
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravityAttracter : MonoBehaviour {

    public float Gravity = -10;

    public void Attract(GameObject body,Vector3 NormalVec)
    {
        //Vector3 GravityUp = (body.transform.position - transform.position).normalized;
        Vector3 GravityUp = NormalVec;
        Vector3 BodyUp = body.transform.up;


        body.GetComponent<Rigidbody>().AddForce(GravityUp*Gravity);
        Quaternion TargetRotation = Quaternion.FromToRotation(BodyUp,GravityUp) * body.transform.rotation;
        body.transform.rotation = Quaternion.Slerp(body.transform.rotation, TargetRotation, 50 * Time.deltaTime);
    }


}
