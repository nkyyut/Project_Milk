using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpScript : MonoBehaviour {

    [SerializeField] Button setbutton;
    public Button[] Hbutton = new Button[4];
    int buttonNum = 0;
    float buttonWidht ;
    [SerializeField] GameObject parent;
    bool activeFlg = false;


    public enum HELP_LIST
    {
        A,
        B,
    }
    public HELP_LIST list;




	void Start () {
        list = 0;
        buttonWidht = Screen.width / 4f;
        if (Hbutton[0] != null)
            for (int i = 0; i < 4; i++)
            {
                Destroy(Hbutton[i]);
            }

        
        
	}
	
	
	void Update () {

        if(parent.activeInHierarchy == true && activeFlg == false)
        {
            activeFlg = true;
            for (int i = 0; i < 4; i++)
            {
                Hbutton[i] = Instantiate(setbutton, parent.transform);

                Hbutton[i].GetComponent<RectTransform>().sizeDelta = new Vector2(buttonWidht, Hbutton[i].GetComponent<RectTransform>().sizeDelta.y);
                Hbutton[i].transform.position = new Vector3(buttonWidht / 2 + i * buttonWidht, Screen.height - 15);
            }
        }

        if (parent.activeInHierarchy == false && Hbutton[0] != null && activeFlg == true)
        {
            this.Start();
            activeFlg = false;
        }
            


        //if(Input.GetKeyDown(KeyCode.S))
        //{
        //    if (list < HELP_LIST.B)
        //        list++;
        //    else
        //        list = 0;
        //}

    }
}
