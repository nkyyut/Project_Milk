﻿using System.Collections;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Onihitode")
        {
            other.gameObject.transform.parent = this.transform.gameObject.transform;
            other.gameObject.GetComponent<SphereCollider>().enabled = false;
            other.gameObject.transform.position += other.transform.forward * -0.005f;

            other.gameObject.GetComponent<EnemyRouteMover>().SetNowEnemyState_IDLE();
            other.gameObject.GetComponent<EnemyGravityBody>().SetGravitySwitch();
        }
    }
}
