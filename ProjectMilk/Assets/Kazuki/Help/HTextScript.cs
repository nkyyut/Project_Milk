using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HTextScript : MonoBehaviour {

    Text textBox;
    private int textNum;
    [SerializeField] GameObject manager;
    [Multiline] [SerializeField] string[] textString = new string[2];

	void Start () {
        textNum = 0;
        textBox = GetComponent<Text>();
        textBox.transform.position = new Vector3(Screen.width * 0.1f, Screen.height * 0.7f);
        textBox.text = textString[textNum];
    }
	
	
	void Update () {

        HelpScript.HELP_LIST listt;
        listt = manager.GetComponent<HelpScript>().list;


        if (textNum != (int)listt)
        {
            textBox.text = textString[(int)listt];
            textNum = (int)listt;
        }

    }
}
