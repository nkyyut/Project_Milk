using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPage : MonoBehaviour {

    public int MaxPage = 9;
    public GameObject GameStart;
    public Sprite[] Page=new Sprite[9];
    GameObject Comic;
    [SerializeField]
    public int NowPage;
    // Use this for initialization
    void Start () {
        Comic = GameObject.Find("Comic");
        NowPage = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TurnPageFunc(int PageNum)
    {
        Comic.GetComponent<SpriteRenderer>().sprite = Page[PageNum];
    }

    public void AddPage()
    {
        GameObject.Find("DecisionSE").GetComponent<AudioSource>().Play();
        if (NowPage < MaxPage-1)
        {
            NowPage+=1;
            TurnPageFunc(NowPage);
        }
        if(NowPage==MaxPage-1)
        {
            GameStart.SetActive(true);
        }
    }
    public void SubPage()
    {
        GameObject.Find("CancelSE").GetComponent<AudioSource>().Play();
        GameStart.SetActive(false);
        if (NowPage > 0)
        {
            NowPage-=1;
            TurnPageFunc(NowPage);
        }
    }
}
