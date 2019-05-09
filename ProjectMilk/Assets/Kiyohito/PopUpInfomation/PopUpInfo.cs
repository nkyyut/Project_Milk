using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PopUpInfo : MonoBehaviour {
    public GameObject Player;
    public GameObject Plane;
    float Distance;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Distance = Vector3.Distance(Player.transform.position, this.gameObject.transform.position);

        if (Distance <= 2.0f)
        {
            PopUp();
        }
        else
        {
            PullDown();
        }
    }
    public void PopUp()
    {
        Color PlaneColor = Plane.GetComponent<SpriteRenderer>().color;
        PlaneColor.a = 255;
        Plane.GetComponent<SpriteRenderer>().color= PlaneColor;
    }
    public void PullDown()
    {
        Color PlaneColor = Plane.GetComponent<SpriteRenderer>().color;
        PlaneColor.a = 0;
        Plane.GetComponent<SpriteRenderer>().color = PlaneColor;
    }
}
