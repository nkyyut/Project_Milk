using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoScript : MonoBehaviour {

    [SerializeField]VideoClip[] clipsBox ;
    VideoPlayer videoplayer;
    [SerializeField]GameObject manager;
    private int movieNum;

	void Start () {
        movieNum = 0;
         
        videoplayer = GetComponent<VideoPlayer>();
        videoplayer.clip = clipsBox[movieNum];
        videoplayer.isLooping = true;
        transform.position = new Vector3(Screen.width - GetComponent<RectTransform>().sizeDelta.x * transform.localScale.x/2, Screen.height/2);
	}
	
	void Update () {


        HelpScript.HELP_LIST listv;
        listv = manager.GetComponent<HelpScript>().list;
        Debug.Log((int)listv);

        if (videoplayer.clip == clipsBox[(int)listv])
        {
            
            if (videoplayer.isPlaying == false) videoplayer.Play();
            
        }
        else
        {
            videoplayer.Stop();
            videoplayer.clip = clipsBox[(int)listv];
            videoplayer.Play();

        }
	}
}
