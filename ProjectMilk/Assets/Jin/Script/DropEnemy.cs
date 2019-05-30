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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Onihitode")
        {
            Debug.Log("DropEnemy");
            Debug.Log(other.gameObject.name);
            other.gameObject.transform.parent = this.transform.gameObject.transform;
            other.gameObject.GetComponent<SphereCollider>().enabled = false;
            other.gameObject.transform.position += other.transform.forward * -0.005f;

            other.gameObject.GetComponent<EnemyRouteMover>().enabled = false;
            other.gameObject.GetComponent<EnemyGravityBody>().enabled = false;
        }
    }
}
