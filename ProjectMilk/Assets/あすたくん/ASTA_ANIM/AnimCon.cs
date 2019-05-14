using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimCon : MonoBehaviour {

    [SerializeField] Animator anim;
    [SerializeField] Material mat;

    [SerializeField] Color[] color; // 変更前後 0:キリトリ　1:通常 2:変数
    [SerializeField] float ColorSpeed;
    
    private void FixedUpdate()
    {
        anim.SetFloat("X", Input.GetAxis("Horizontal"));
        anim.SetFloat("Z", Input.GetAxis("Vertical"));

        if (Input.GetMouseButtonDown(1))
        {
            anim.SetInteger("ModeState", 1);
        }
        if (Input.GetMouseButtonUp(1))
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

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Onihitode")
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
}
