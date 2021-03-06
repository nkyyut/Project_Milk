﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimCon : MonoBehaviour {

    [SerializeField] Animator anim;
    [SerializeField] Material mat;

    [SerializeField] Color[] color; // 変更前後 0:キリトリ　1:通常 2:変数
    [SerializeField] float ColorSpeed;

    public bool isEnd;

    private void Start()
    {
        isEnd = false;
    }

    private void FixedUpdate()
    {
        anim.SetFloat("X", Input.GetAxis("Horizontal"));
        anim.SetFloat("Z", Input.GetAxis("Vertical"));

        if (Input.GetAxis("RT_Botton") == -1)
        {
            anim.SetInteger("ModeState", 1);
        }
        if (Input.GetAxis("RT_Botton") == 0)
        {
            anim.SetInteger("ModeState", 0);
        }

        if (anim.GetInteger("ModeState") == 0)
        {
            color[2] = ColorChange(color[2], color[1]);
            color[2].a = 0;
            mat.color = color[2];
        }
        else
        {
            color[2] = ColorChange(color[2], color[0]);
            color[2].a = 0;
            mat.color = color[2];
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Stan") && 
            collision.gameObject.tag == "Onihitode")
        {
            anim.SetTrigger("Hit");
        }
    }

    private Color ColorChange(Color w , Color a)
    {
        if(w.r < a.r)
        {
            w.r += Time.deltaTime * ColorSpeed;
        }
        if(w.r > a.r)
        {
            w.r -= Time.deltaTime * ColorSpeed;
        }
        if (w.g < a.g)
        {
            w.g += Time.deltaTime * ColorSpeed;
        }
        if (w.g > a.g)
        {
            w.g -= Time.deltaTime * ColorSpeed;
        }

        return w;
    }

    public void PlayClearAnim()
    {
        anim.SetBool("isClear", true);
    }

    public void PlayGameOverAnim()
    {
        Debug.Log("In");
        anim.SetBool("isGameOver", true);
    }

    public bool AnimEndCheck()
    {
        return isEnd;
    }
}

// Play○○Animの後AnimEndCheckを呼びtrueが返ってくると終了してる扱いです。