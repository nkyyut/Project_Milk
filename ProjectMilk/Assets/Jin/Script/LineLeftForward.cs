using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineLeftForward : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Coral")
            this.transform.position -= this.transform.forward * -0.001f;
    }

}
