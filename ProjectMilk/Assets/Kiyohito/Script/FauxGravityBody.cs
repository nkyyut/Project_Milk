using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravityBody : MonoBehaviour {

    public FauxGravityAttracter Attracter;
    public Transform Bottom;
    private GameObject MyGameObject;
    // Use this for initialization
    void Start()
    {
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        this.GetComponent<Rigidbody>().useGravity = false;
        MyGameObject = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 NormalVec=CheckNormal();
        Attracter.Attract(MyGameObject,NormalVec);
    }
    RaycastHit CheckPolygonToRayCast()
    {
        RaycastHit hit;
        if(Physics.Raycast(Bottom.position, -transform.up, out hit, float.PositiveInfinity));
        return hit;
    }

    Vector3 CheckNormal()
    {
        RaycastHit hit;
        hit = CheckPolygonToRayCast();
        return hit.normal;
    }
}
