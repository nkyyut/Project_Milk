using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropEnemy : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Onihitode")
        {
            Debug.Log("Onihitode");
            other.gameObject.transform.parent = this.transform.gameObject.transform;
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            other.gameObject.transform.position += other.transform.forward * -0.005f;
        }
    }
}
