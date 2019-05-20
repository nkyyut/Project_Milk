using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScript : MonoBehaviour {

    [SerializeField] RawImage manta;
    [SerializeField] Text winner;
    [SerializeField] Text result;
    [SerializeField] Text score;

    RawImage imageBox;
    Text[] textBox = new Text[3];
    int[] countdown = { 0, 0, 0, 0 };
    int[] scorebox = { 0, 0, 0, 0 };

    [SerializeField] Image parentImage;

    public int Score;
    
    private int point;
    private float count;


    public bool fadeflg;
    public bool scoreflg;

    public enum TYPE_RESULT
    {
        MANTA,

        TOP,
        MIDDLE,
        lOW,

        WINNER,
        LOSER,

        SCORE,
    }
    public TYPE_RESULT resultType;


	void Start () {
        count = 0;
        
	}


    void Update() {

        switch (resultType) {
            case TYPE_RESULT.MANTA:
                if (imageBox == null)
                {
                    imageBox = Instantiate(manta);
                    imageBox.transform.SetParent(parentImage.transform, false);
                }
                Vector3 Box = imageBox.rectTransform.position;
                imageBox.rectTransform.position = Vector3.Lerp(imageBox.transform.position, new Vector3(Screen.width / 2, Screen.height / 2), 2 * Time.deltaTime);
                
                if (imageBox.rectTransform.position.normalized - Box.normalized == Vector3.zero)
                    resultType = TYPE_RESULT.WINNER;
                break;

            case TYPE_RESULT.WINNER:
                if (textBox[0] == null)
                {
                    textBox[0] = Instantiate(winner);
                    textBox[0].transform.SetParent(parentImage.transform, false);
                    textBox[0].rectTransform.position = new Vector3(Screen.width / 2, Screen.height * 0.9f);
                    textBox[0].text = "勝ち";
                    fadeflg = false;
                }
                if(fadeflg == true)
                    resultType = TYPE_RESULT.TOP;
                break;

            case TYPE_RESULT.TOP:
                if (textBox[1] == null)
                {
                    textBox[1] = Instantiate(result);
                    textBox[1].transform.SetParent(parentImage.transform, false);
                    textBox[1].text = "さいこうだ!!";
                }
                    Vector3 Box2 = textBox[1].rectTransform.position;
                    textBox[1].rectTransform.position = Vector3.Lerp(textBox[1].transform.position, new Vector3(Screen.width / 2, Screen.height *0.7f), 2 * Time.deltaTime);
                    if (textBox[1].rectTransform.position.normalized - Box2.normalized == Vector3.zero)
                    {
                        textBox[1].text += "\n-------------------";
                    resultType = TYPE_RESULT.SCORE;
                    }
                    
                
                break;


            case TYPE_RESULT.MIDDLE:
                
                break;

            case TYPE_RESULT.SCORE:
                if (textBox[2] == null)
                {
                    textBox[2] = Instantiate(score);
                    textBox[2].transform.SetParent(parentImage.transform, false);
                    textBox[2].rectTransform.position = new Vector3(Screen.width / 2, Screen.height *0.3f);
                    textBox[2].text = "とくてん…"+ countdown[3] + countdown[2] + countdown[1] + countdown[0];

                    scorebox[3] = Score / 1000;
                    scorebox[2] = Score % 1000 / 100;
                    scorebox[1] = Score % 1000 % 100 / 10;
                    scorebox[0] = Score % 1000 % 100 % 10 / 1;
                    fadeflg = false;
                }
                textBox[2].text = "とくてん…" + countdown[3] + countdown[2] + countdown[1] + countdown[0];
                if (fadeflg == true)
                {
                    textBox[2].text = "とくてん…" + countdown[3] + countdown[2] + countdown[1] + countdown[0];


                    if (textBox[2] != null)
                    {
                        if (count < 1)
                        {
                            countdown[0] = Random.Range(0, 9);
                            countdown[1] = Random.Range(0, 9);
                            countdown[2] = Random.Range(0, 9);
                            countdown[3] = Random.Range(0, 9);
                            count += Time.deltaTime;


                        }
                        else if(count > 1 && count < 2)
                        {
                            countdown[0] = scorebox[0];
                            count += Time.deltaTime;
                            countdown[1] = Random.Range(0, 9);
                            countdown[2] = Random.Range(0, 9);
                            countdown[3] = Random.Range(0, 9);
                        }
                        else if(count > 2 && count < 3)
                        {
                            countdown[1] = scorebox[1];
                            count += Time.deltaTime;
                            countdown[2] = Random.Range(0, 9);
                            countdown[3] = Random.Range(0, 9);
                        }
                        else if(count > 3 && count < 4)
                        {
                            countdown[2] = scorebox[2];
                            count += Time.deltaTime;
                            countdown[3] = Random.Range(0, 9);
                        }
                        else if(count > 4 && count < 5)
                        {
                            countdown[3] = scorebox[3];
                            Debug.Log("OK4");
                        }



                        Debug.Log(count);

                    }
                }
                break;
           }
    }
}
