using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetVector : MonoBehaviour {

    public GameObject Camera;
    public GameObject Astar;
    Quaternion InitRotate;
    Vector3 TargetPoint;
    float RotateSpeed=5.0f;

	// Use this for initialization
	void Start () {
        InitRotate = Astar.transform.rotation;
    }
	
	// Update is called once per frame
	void Update () {
        Astar.transform.rotation = Quaternion.Slerp(Astar.transform.rotation, Camera.transform.rotation, Time.deltaTime * RotateSpeed);
    }
}
