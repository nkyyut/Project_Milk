using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rad : MonoBehaviour {
    public GameObject Player;
    float pc_npc_Distance; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        pc_npc_Distance=Vector3.Distance(Player.transform.position,this.gameObject.transform.position);
       
        if (pc_npc_Distance <= 2.0f)
        {
        }
	}
}
