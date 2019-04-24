using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleControl : MonoBehaviour {

    [SerializeField] ParticleSystem particle;
    private Object objectBox;
    public bool Flg;


	void Start () {
        Flg = false;
	}
	
	
	void Update () {
        if (Flg == true && objectBox == null)
        {
            objectBox = Instantiate<Object>(particle);

        }
        GameObject.Destroy(objectBox);
	}
}
