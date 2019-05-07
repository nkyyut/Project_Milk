//與儀清仁　2019年/4/17
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravityBody : MonoBehaviour {

    public FauxGravityAttracter Attracter;
    RaycastHit LogHit;
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
        if (Input.GetAxis("Horizontal") + Input.GetAxis("Vertical") != 0)
        {
            Vector3 NormalVec = CheckNormal();
            Attracter.Attract(MyGameObject, NormalVec);
        }
    }
    RaycastHit CheckPolygonToRayCast()
    {
        RaycastHit hit;
        if(Physics.Raycast(Bottom.position, -transform.up, out hit, float.PositiveInfinity))
        {
            if (hit.collider.transform.tag == "Coral")
            {
                Debug.DrawRay(Bottom.position, -transform.up * 10, Color.red, 0.1f);
                Debug.Log(hit.collider.transform.tag);
                LogHit = hit;
                return hit;
            }
            else return LogHit;
        }
        else
        {
            return LogHit;
        }

    }

    Vector3 CheckNormal()
    {
        RaycastHit hit;
        hit = CheckPolygonToRayCast();
        return hit.normal;
    }
}
