using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingSound : MonoBehaviour {

    [SerializeField] AudioClip _se_SmallRing;
    [SerializeField] AudioClip _se_BigRing;

    private AudioSource _audioSource;

	void Start () {
		_audioSource = this.gameObject.GetComponent<AudioSource>();
	}

	void Update () {
		
	}

    public void SE_MeshCreate()
    {
        _audioSource.PlayOneShot(_se_BigRing);
    }

}
