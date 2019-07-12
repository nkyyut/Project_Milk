using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleControl : MonoBehaviour {

    [SerializeField] ParticleSystem particle;
    private Object[] objectBox = new Object[100];
    public bool Flg;
    [SerializeField] Transform parent;

    void Start () {
        Flg = false;
	}
	
	
	void Update () {
        if (Flg == true)
        {
            //objectBox.positoin = ;
            for (int i = 0; i < 100; i++)
                if (objectBox[i] == null)
                {
                    objectBox[i] = Instantiate<Object>(particle, parent);
                   
                    i = 100;
                }
        }
	}
}
