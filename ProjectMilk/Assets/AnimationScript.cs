﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour {
    
    Animator rool;
    public GameObject Player;
    bool gravityFlgR = false;
    bool Flg = true;
   
    void Start () {
        rool = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        
        float InputH = Input.GetAxisRaw("Horizontal");
        float InputV = Input.GetAxisRaw("Vertical");

        if (InputH != 0 || InputV != 0 )
        {
            if (gravityFlgR == false)
                rool.SetBool("Move", true);
            else
            {
                rool.SetBool("Move", false);
                rool.SetBool("Flg", true);
            }
            rool.SetBool("End", false);
        }
        else
        {
            if (gravityFlgR == false)
                rool.SetBool("Move", false);
            else
            {
                rool.SetBool("Move", true);
                rool.SetBool("Flg", false);
            }
            rool.SetBool("End", true);
        }
        //if (Player.GetComponent<PlayerControl>().gravityFlg == true)
        //    gravityFlgR = true;
        //if(gravityFlgR == true)
        //{
        //    if (Flg == true)
        //    {
        //       // transform.Rotate(0f, -90f, 90f);
        //        Flg = false;
        //    }

        //}
    }

    
}
