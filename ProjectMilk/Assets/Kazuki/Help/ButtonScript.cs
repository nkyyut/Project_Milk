using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ButtonScript : MonoBehaviour {
    public int Boxnum;
    [SerializeField] GameObject manager;
   

    

    public void OnClick()
    {
        manager = GameObject.Find("HelpManager");
        Debug.Log("On");
        for (int i = 0; i < 4; i++)
            if (manager.GetComponent<HelpScript>().Hbutton[i].transform.position == this.transform.position)
                Boxnum = i;
            else Debug.Log("false");

        manager.GetComponent<HelpScript>().list = (HelpScript.HELP_LIST)Enum.ToObject(typeof(HelpScript.HELP_LIST), Boxnum);
    }
		
	
}
