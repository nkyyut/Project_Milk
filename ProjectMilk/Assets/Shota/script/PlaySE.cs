using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySE : MonoBehaviour {

    public AudioSource audiosource;

    public void Play(AudioClip ac)
    {
        audiosource.PlayOneShot(ac);
    }
}
