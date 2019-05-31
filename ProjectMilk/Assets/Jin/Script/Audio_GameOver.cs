using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_GameOver : MonoBehaviour {

    [SerializeField] private AudioClip _bgm_gameover;
    [SerializeField] private AudioClip _se_OnihitodeHit;

    [SerializeField] private GameObject _bgmManager;

    private AudioSource _audioSource;

    private AudioSource _BGM_audioSource;

	// Use this for initialization
	void Start () {
		_audioSource = this.GetComponent<AudioSource>();
        _BGM_audioSource = _bgmManager.GetComponent<AudioSource>();
	}

    public void SE_OnihitodeBan()
    {
        _audioSource.PlayOneShot(_se_OnihitodeHit);
    }
	
    public void BGM_GameOver()
    {
        _audioSource.clip = _bgm_gameover;
        _audioSource.PlayOneShot(_bgm_gameover);
        _audioSource.Play();
    }

    public void MainBGM_Stop()
    {
        _BGM_audioSource.Stop();
    }
}
