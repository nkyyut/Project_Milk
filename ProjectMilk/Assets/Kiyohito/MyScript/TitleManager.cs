using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour {

    public SceneTransition SceneTransition;

    enum TITLE_STATE
    {
        Animation,
        Wait
    }TITLE_STATE TitleState;

	// Use this for initialization
	void Start () {
        TitleState = TITLE_STATE.Wait;
	}
	
	// Update is called once per frame
	void Update () {
        Switch();

    }

    void Switch()
    {
        switch (TitleState)
        {
            case TITLE_STATE.Animation:
                break;
            case TITLE_STATE.Wait:
                Wait();
                break;

        }
    }

    void Wait()
    {
        if(Input.GetKeyUp("joystick button 0"))
        {
            SceneTransition.Transition();
        }
    }
}
