using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnihitodeMover : MonoBehaviour {

    private RaycastHit hit;

	// Use this for initialization
	void Start () {
        if (Physics.Raycast(transform.position, transform.forward*-1, out hit))
        {

            transform.position = hit.point;

        }
    }
	
	// Update is called once per frame
	//void Update () {
		
	//}
}
