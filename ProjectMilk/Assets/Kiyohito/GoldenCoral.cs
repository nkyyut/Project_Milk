using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenCoral : MonoBehaviour {

    DurableValueManager DurableValueManager;

	// Use this for initialization
	void Start () {
        //DurableValueManager = GameObject.Find("DurableValueManager").GetComponent<DurableValueManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            //DurableValueManager.AddRecoveryPoint();
        }
        Destroy(this.gameObject);
    }
}
