using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackFade : MonoBehaviour {


    // 色が見づらく文字が読み取れなかったため個別に設定できるよう変更しました　5/28與那覇
    // ※追記5/30　文字が半透明は違和感があったためこちらも個別に変更できるようにしました。與那覇

    [SerializeField] float fadeSpeed;
    [SerializeField] float alphaLimit;
    [SerializeField] Color mycolor;
    GameObject manager;
    float alfa ;
    


	void Start () {
        alfa = 0;
        manager = GameObject.Find("ResultManager");
    }
	
	
	void Update () {
        
        if(GetComponent<Image>() != null)
            GetComponent<Image>().color = new Color(mycolor.r,mycolor.g,mycolor.b, alfa);

        else if (GetComponent<Text>() != null)
            GetComponent<Text>().color = new Color(mycolor.r, mycolor.g, mycolor.b, alfa);


        if (alfa < alphaLimit)
            alfa += fadeSpeed;
        else
        {
            Debug.Log(manager.GetComponent<ResultScript>().fadeflg);

            if (manager.GetComponent<ResultScript>().enabled == false)
            manager.GetComponent<ResultScript>().enabled = true;
            if (manager.GetComponent<ResultScript>().fadeflg == false)
                manager.GetComponent<ResultScript>().fadeflg = true;
            Destroy(this);
        }
	}
}
