using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Comic : MonoBehaviour {
    [SerializeField] Sprite[] Page;
    [SerializeField] GameObject Plane;
    float NowPage;
    
    enum COMIC_STATE{ 
        IDLE,
        TURN_PAGE,
        BACK_PAGE,
        INPUT_RECEPTION
    }COMIC_STATE NowComicState;

	// Use this for initialization
	void Start () {
        NowPage = 0;
	}
	
	// Update is called once per frame
	void Update () {
        Switching();
	}

    void Switching()
    {
        switch (NowComicState)
        {
            case COMIC_STATE.IDLE:
                break;
            case COMIC_STATE.TURN_PAGE:
                TurnPage();
                break;
            case COMIC_STATE.BACK_PAGE:
                BackPage();
                break;
            case COMIC_STATE.INPUT_RECEPTION:
                InputReception();
                break;

        }
    }

    void ChangeSprite(Sprite NextSprite)
    {

    }

    void TurnPage()
    {

    }

    void BackPage()
    {

    }

    void InputReception()
    {

    }
}
