using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRightForward : MonoBehaviour {

    private void Start()
    {
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if(other.transform.tag == "Coral")
    //        this.transform.position += this.transform.forward * -0.001f;
    //}
}
