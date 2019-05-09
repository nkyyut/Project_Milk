using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimCon : MonoBehaviour {

    [SerializeField] Animator anim;

    private void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            anim.SetInteger("state", 1);
        }
        else
        {
            anim.SetInteger("state", 0);
        }

        if (Input.GetMouseButtonDown(1))
        {
            anim.SetInteger("ModeState", 1);
        }
        if (Input.GetMouseButtonUp(1))
        {
                anim.SetInteger("ModeState", 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            anim.SetTrigger("Hit");
        }
    }
}
