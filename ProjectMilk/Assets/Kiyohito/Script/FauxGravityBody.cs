//與儀清仁　2019年/4/17
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravityBody : MonoBehaviour {

    public FauxGravityAttracter Attracter;
    RaycastHit LogHit;
    float Search_Radius;
    public Transform Bottom1,Bottom2;
    private GameObject MyGameObject;
    // Use this for initialization
    void Start()
    {
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        this.GetComponent<Rigidbody>().useGravity = false;
        MyGameObject = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
            Vector3 NormalVec = CheckNormal();
            Attracter.Attract(MyGameObject, NormalVec);
    }


    RaycastHit CheckPolygonToRayCast()
    {
        RaycastHit hit1,hit2;
        //RaycastHit[] hits;

        //hits = Physics.RaycastAll(Bottom.position, -this.transform.up,10.0f);
        //Debug.Log(hits.Length);

        Physics.Raycast(Bottom1.position, -transform.up, out hit1, 10.0f);
        Physics.Raycast(Bottom2.position, -transform.up, out hit2, 10.0f);
        if (hit1.transform.tag == "Coral" && hit2.transform.tag == "Coral")
        {
            if (hit1.distance < hit2.distance)
            {
                LogHit = hit1;
                return hit1;
            }
            else
            {
                LogHit = hit2;
                return hit2;
            }
        }
        else if (hit1.transform.tag == "Coral" && hit2.transform.tag != "Coral")
        {
            return hit1;
        }
        else if (hit1.transform.tag != "Coral" && hit2.transform.tag == "Coral")
        {
            return hit2;
        }
        else return LogHit;


        

        //if (Physics.Raycast(Bottom1.position, -transform.up, out hit, 10.0f))
        //{
        //    if (hit.collider.transform.tag == "Coral")
        //    {
        //        LogHit = hit;
        //        return hit;
        //    }
        //    else return LogHit;
        //}
        //else
        //{
        //    return LogHit;
        //}

    }

    //指定されたタグの中で最も近いものを取得
    GameObject serchTag(GameObject OriginObject, string TagName)
    {
        float TmpDis = 0;           //距離用一時変数
        float NearDis = 0;          //最も近いオブジェクトの距離
        GameObject MostNearObj = null; //オブジェクト

        //タグ指定されたオブジェクトを配列で取得する
        foreach (GameObject Container in GameObject.FindGameObjectsWithTag(TagName))
        {
            //自身と取得したオブジェクトの距離を取得
            TmpDis = Vector3.Distance(Container.transform.position,OriginObject.transform.position);

            //オブジェクトの距離が近いか、距離0であればオブジェクト名を取得
            //一時変数に距離を格納
            if ( NearDis > TmpDis|| NearDis == 0)
            {
                NearDis = TmpDis;
                MostNearObj = Container;
            }

        }
        //最も近かったオブジェクトを返す
        return MostNearObj;
    }




    Vector3 CheckNormal()
    {
        RaycastHit hit;
        hit = CheckPolygonToRayCast();
        return hit.normal;
    }
}
