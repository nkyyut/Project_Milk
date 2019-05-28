using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgainLinePosition : MonoBehaviour {

    private void OnCollisionStay(Collider other)
    {
        //埋まらないように位置を再調整
        if(other.gameObject.tag == "Coral")
        {
            Debug.Log("CoralHit");
            this.gameObject.transform.position += this.transform.forward * 0.001f;
        }
    }
}
