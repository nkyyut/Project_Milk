using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackFade : MonoBehaviour {

    [SerializeField] float fadeSpeed;
    GameObject manager;
    float alfa ;
    


	void Start () {
        alfa = 0;
        manager = GameObject.Find("ResultManager");
    }
	
	
	void Update () {
        
        if(GetComponent<Image>() != null)
            GetComponent<Image>().color = new Color(0,0,0, alfa);

        else if (GetComponent<Text>() != null)
            GetComponent<Text>().color = new Color(0,0,0, alfa);


        if (alfa < 0.6f)
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
