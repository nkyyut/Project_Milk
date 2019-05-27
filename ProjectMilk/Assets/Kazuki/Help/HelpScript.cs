using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HelpScript : MonoBehaviour {
    
    [SerializeField] Button button;
    public Button[] Hbutton ;
    
    float buttonWidht ;
    [SerializeField] GameObject parent;
    bool activeFlg = true;


    public enum HELP_LIST
    {
        A,
        B,
    }
    public HELP_LIST list;
    

	void Start () {
        list = 0;

        for (int i = 0; i < Hbutton.Length; i++)
        {
            Hbutton[i] = Instantiate<Button>(button);
            Hbutton[i].transform.SetParent(parent.transform);
        }

        buttonWidht = Screen.width / Hbutton.Length;
        Hbutton[0].Select();
        
        activeFlg = false;
        for (int i = 0; i < Hbutton.Length; i++)
        {
            Hbutton[i].GetComponent<RectTransform>().sizeDelta = new Vector2(buttonWidht, Hbutton[i].GetComponent<RectTransform>().sizeDelta.y * 2);
            Hbutton[i].transform.position = new Vector3(buttonWidht / 2 + i * buttonWidht, Screen.height - 15);
        }
    }
	
	
	void Update () {

        if(parent.activeInHierarchy == true && activeFlg == false)
        {
            list = 0;
            Hbutton[(int)list].Select();
            Hbutton[(int)list].GetComponent<ButtonScript>().OnClick();
            activeFlg = true;
            
        }
        if (parent.activeInHierarchy == false && activeFlg == true)
        {
            
            activeFlg = false;
        }
        if(Input.GetKeyDown(KeyCode.JoystickButton5))
        {
            if ((int)list == Hbutton.Length - 1)
                list = 0;
            else
                list++;
            Hbutton[(int)list].Select();
            Hbutton[(int)list].GetComponent<ButtonScript>().OnClick();
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton4))
        {
            if ((int)list == 0)
                list = (HELP_LIST)Enum.ToObject(typeof(HELP_LIST), Hbutton.Length-1);
            else
                list--;
            Hbutton[(int)list].Select();
            Hbutton[(int)list].GetComponent<ButtonScript>().OnClick();
        }


        if(Input.GetKeyDown(KeyCode.C))
        {
            parent.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            parent.SetActive(true);
        }
    }
}
