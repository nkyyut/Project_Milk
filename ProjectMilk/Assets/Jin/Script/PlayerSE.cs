using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSE : MonoBehaviour {

    [SerializeField] private AudioClip _se_CreateLine;

    private AudioSource _audioSource;

	// Use this for initialization
	void Start () {
		_audioSource = this.gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SE_LineCreate()
    {
        _audioSource.PlayOneShot(_se_CreateLine);
    }
}
