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
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.transform.parent = this.transform.gameObject.transform;

            other.gameObject.transform.position += other.transform.forward * -0.005f;
        }
    }
    
    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("hha");
    //    if (collision.gameObject.tag == "DropBlock")
    //    {
    //        this.transform.gameObject.transform.parent = collision.gameObject.transform;
    //    }
    //}
}
