using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ButtonScript : MonoBehaviour {
    public int Boxnum;
    //public GameObject manager;


    public void OnClick()
    {
        Debug.Log("On");
        
        GameObject manager = GameObject.Find("HelpManager");

        for (int i = 0; i < 4; i++)
            if (manager.GetComponent<HelpScript>().Hbutton[i].transform.position == this.transform.position)
                Boxnum = i;
            else Debug.Log("false");

        manager.GetComponent<HelpScript>().list = (HelpScript.HELP_LIST)Enum.ToObject(typeof(HelpScript.HELP_LIST), Boxnum);
    }
		
	
}
