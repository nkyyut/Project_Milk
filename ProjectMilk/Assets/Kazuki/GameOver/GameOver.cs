using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{

    [SerializeField] RawImage hitode;
    [SerializeField] Transform generatePos;
    [SerializeField] Transform parentPos;
    private RawImage[] hitodeBox = new RawImage[100];
    private Vector3[] generateBox = new Vector3[100];
    float ScreenSizeRation;

    int start = 1;
    int end = 60;
    List<int> numbers = new List<int>();

    private bool Doonce = false;
    private bool generateFlg = false;

    public bool finish { get { return generateFlg; } }


    bool Keyflg = false;
    bool State = false;
    float speed = 1f;
    float time = 0;

    private Audio_GameOver _audioGameOver;

    void Start()
    {
        ScreenSizeRation = Screen.height / (float)KiyohitoConst.Const.FoundationScreenSize;

        //State = false;
        for (int i = start; i <= end; i++)
        {
            numbers.Add(i);
        }

        int BoxCount = 0;
        for (int i = 1; i <= 5; i++)
        {
            for (int y = 1; y <= 10; y++)
                generateBox[BoxCount++] = new Vector3(Screen.width * y / 10 - 60, Screen.height * i / 5 - 50/*y * 40f - 10, i * 40 , 0*/);
        }

        _audioGameOver = this.gameObject.GetComponent<Audio_GameOver>();
    }


    void Update()
    {
        if (State == true)
        {
            if (Input.anyKey)
                for (int i = 0; i < numbers.Count;)
                {
                    _audioGameOver.SE_OnihitodeBan();

                    int index = Random.Range(0, numbers.Count);
                    int ransu = numbers[index];
                    generatePos.position = generateBox[ransu];
                    // Debug.Log(generatePos.position);
                    hitodeBox[ransu] = Instantiate(hitode, generatePos);
                    hitodeBox[ransu].transform.SetParent(parentPos, true);

                    //hitodeBox[ransu].transform.localScale *= ScreenSizeRation;
                    numbers.Remove(ransu);
                    Keyflg = true;
                    Debug.Log(numbers.Count);
                }

            if ((time += Time.deltaTime * speed) >= 0.75f && Keyflg == false)
                if (numbers.Count > 0)
                {
                    _audioGameOver.SE_OnihitodeBan();

                    //Debug.Log(Screen.height);
                    int index = Random.Range(0, numbers.Count);
                    int ransu = numbers[index];

                    generatePos.position = generateBox[ransu];
                    // Debug.Log(generatePos.position);
                    hitodeBox[ransu] = Instantiate(hitode, generatePos);
                    hitodeBox[ransu].transform.SetParent(parentPos, true);
                    // hitodeBox[ransu].transform.localScale *= 0.5f;

                    numbers.Remove(ransu);

                    time = 0;
                    speed *= 1.5f;
                }

            if (numbers.Count == 0)
            {
                if (!Doonce)
                {
                    _audioGameOver.MainBGM_Stop();
                    _audioGameOver.BGM_GameOver();
                    generateFlg = true;
                    Doonce = true;
                }
            }

        }

        //if (flg == true)
        //{
        //    for (int i = 0; i < 10; i++)
        //    {
        //        //float random = Random.Range(-150, 200);
        //        float randomy = Random.Range(0 , 3);
        //        generatePos.position = new Vector3(i * 50,100+randomy*5, 0);
        //        hitodeBox[i] = Instantiate(hitode, generatePos);
        //        hitodeBox[i].transform.SetParent(parentPos, true);
        //    }
        //    flg = false;
        //}
    }

    public void SetGameOver() { State = true; }

}
