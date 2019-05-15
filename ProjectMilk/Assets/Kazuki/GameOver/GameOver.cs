using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

    [SerializeField] RawImage hitode;
    [SerializeField] Transform generatePos;
    [SerializeField] Transform parentPos;
    private RawImage[] hitodeBox = new RawImage[100];
    private Vector3[] generateBox = new Vector3[100];


    int start = 1;
    int end = 50;
    List<int> numbers = new List<int>();

    private bool generateFlg = true;
    
    public bool finish { get { return generateFlg; } }
    

    bool Keyflg = false;
    bool State = false;
    float speed = 1f;
    float time = 0;
    


	void Start () {
        State = false;
      for(int i = start; i <= end; i++)
        {
            numbers.Add(i);
        }
      
        int BoxCount = 0;
        for(int i = 1; i <= 5; i++)
        {
            for (int y = 1; y <= 10; y++)
                generateBox[BoxCount++] = new Vector3(y * 40f - 10, i * 40 , 0);
        }
        if (BoxCount >= 50)
            State = true;

    }


    void Update()
    {

        if (State == true)
        {
            if (Input.anyKey)
                for (int i = 0; i < numbers.Count; )
                {
                    int index = Random.Range(0, numbers.Count);
                    int ransu = numbers[index];
                    generatePos.position = generateBox[ransu];
                    // Debug.Log(generatePos.position);
                    hitodeBox[ransu] = Instantiate(hitode, generatePos);
                    hitodeBox[ransu].transform.SetParent(parentPos, true);
                    numbers.Remove(ransu);
                    Keyflg = true;
                    Debug.Log(numbers.Count);
                }

            if ((time += Time.deltaTime * speed) >= 0.75f && Keyflg == false)
                if (numbers.Count > 0)
                {

                    int index = Random.Range(0, numbers.Count);
                    int ransu = numbers[index];

                    generatePos.position = generateBox[ransu];
                    // Debug.Log(generatePos.position);
                    hitodeBox[ransu] = Instantiate(hitode, generatePos);
                    hitodeBox[ransu].transform.SetParent(parentPos, true);


                    numbers.Remove(ransu);

                    time = 0;
                    speed *= 1.5f;
                }

            if (numbers.Count == 0)
                generateFlg = false;

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
    
}
