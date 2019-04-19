using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGravity : MonoBehaviour {
    [SerializeField] private Vector3 localGravity;
    private Rigidbody rBody;
    private RaycastHit hit;

    // Use this for initialization
    private void Start()
    {
        rBody = this.GetComponent<Rigidbody>();
        rBody.useGravity = false; //最初にrigidBodyの重力を使わなくする
    }

    private void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            rBody.AddForce(new Vector3(0, 0.1f, 0), ForceMode.Impulse);
            setLocalGravity();
        }
     
    }

    private void setLocalGravity()
    {
        localGravity = new Vector3(hit.point.x, hit.point.y, hit.point.z);

        rBody.AddForce(localGravity, ForceMode.Acceleration);
    }
}
