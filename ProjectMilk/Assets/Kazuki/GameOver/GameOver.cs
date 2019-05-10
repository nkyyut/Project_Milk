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

    int starey = 1;
    int endy = 50;
    List<int> numbersy = new List<int>();

    
    bool Keyflg = false;
    float speed = 1f;
    float time = 0;

	void Start () {
      for(int i = start; i <= end; i++)
        {
            numbers.Add(i);
        }

        for (int i = start; i <= end; i++)
        {
            numbersy.Add(i);
        }

        int BoxCount = 0;
        for(int i = 1; i <= 5; i++)
        {
            for (int y = 1; y <= 10; y++)
                generateBox[BoxCount++] = new Vector3(y * 40f - 10, i * 40 , 0);
        }

    }


    void Update()
    {

       
        if(Input.anyKey)
            for(int i = 0; i < numbers.Count; i++)
            {
                int index = Random.Range(0, numbers.Count);
                int ransu = numbers[index];
                generatePos.position = generateBox[ransu];
                Debug.Log(generatePos.position);
                hitodeBox[ransu] = Instantiate(hitode, generatePos);
                hitodeBox[ransu].transform.SetParent(parentPos, true);
                numbers.Remove(ransu);
                Keyflg = true;

            }

        if ((time += Time.deltaTime * speed) >= 0.75f && Keyflg == false)
            if (numbers.Count > 0)
            {

                int index = Random.Range(0, numbers.Count);
                int ransu = numbers[index];

                int indexy = Random.Range(0, numbersy.Count);
                int ransuy = numbersy[indexy];



                generatePos.position = generateBox[ransu];
                Debug.Log(generatePos.position);
                hitodeBox[ransu] = Instantiate(hitode, generatePos);
                hitodeBox[ransu].transform.SetParent(parentPos, true);



                numbers.Remove(ransu);
                numbersy.Remove(ransu);

                time = 0;
                speed *= 1.3f;
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
