using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAngle : MonoBehaviour {
    public GameObject Astar;
    public GameObject Camera;
    Vector3 Vec1;
    Vector3 Vec2;
    float Angle;
    // Use this for initialization
    void Start () {
        



    }
	
	// Update is called once per frame
	void Update () {
        Vec1 = Camera.transform.position - Astar.transform.position;
        Vec2 = Astar.transform.up;
        Angle = Vector3.Angle(Vec1, Vec2);
        Debug.Log(Angle);
	}
}
