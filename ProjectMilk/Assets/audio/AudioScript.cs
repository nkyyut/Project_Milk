//かずき

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour {

    public bool audioPlayflg = false;
    public bool audioStopflg = false;
    AudioSource audiosorce;
    [SerializeField] AudioClip audioclip;

	void Start () {
        audiosorce = GetComponent<AudioSource>();
	}
	
	
	void Update () {

		if(audioPlayflg == true && audioStopflg == false)
        {
            audiosorce.clip = audioclip;
            audiosorce.Play();
            audioPlayflg = false;
        }

        if(audioStopflg == true)
        {
            audiosorce.Stop();
            audioStopflg = false;
        }
    }
}
