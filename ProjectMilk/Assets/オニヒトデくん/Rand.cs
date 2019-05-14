using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rand : MonoBehaviour {
    public Animator anim;

    public void SetState()
    {
        anim.SetInteger("STATE_IDLE", SetRand());
    }

    private int SetRand()
    {
        int rand = 0;

        rand = Random.Range(0, 2);
        return rand;
    }
}
