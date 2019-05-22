using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skip : MonoBehaviour {
    GameObject TurnPage;
    public GameObject GameStart;
	// Use this for initialization
	void Start () {
        TurnPage = GameObject.Find("TurnPage");
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SkipPage()
    {
        TurnPage.GetComponent<TurnPage>().NowPage = TurnPage.GetComponent<TurnPage>().MaxPage-1;
        TurnPage.GetComponent<TurnPage>().TurnPageFunc(TurnPage.GetComponent<TurnPage>().NowPage);
        GameStart.SetActive(true);
    }
}
