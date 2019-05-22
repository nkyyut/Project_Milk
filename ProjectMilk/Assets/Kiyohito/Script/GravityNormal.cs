//與儀清仁　2019年/4/17
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityNormal : MonoBehaviour {
    public Transform Bottom;

    [SerializeField] private Vector3 LocalGravity;
    private Rigidbody rBody;

    // Use this for initialization
    void Start () {
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        this.GetComponent<Rigidbody>().useGravity = false;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void FixedUpdate()
    {
        setLocalGravity(); //重力をAddForceでかけるメソッドを呼ぶ
    }

    private void setLocalGravity()
    {
        Vector3 NewVec;
        NewVec = CheckNormal();
        rBody.AddForce(-NewVec, ForceMode.Acceleration);
    }

    RaycastHit CheckPolygonToRayCast()
    {
        RaycastHit hit;
        Physics.Raycast(Bottom.position, -transform.up, out hit, float.PositiveInfinity);
        return hit;
    }

    Vector3 CheckNormal()
    {
        RaycastHit hit;
        hit=CheckPolygonToRayCast();
        return hit.normal;
    }
}
