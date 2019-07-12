//與儀清仁　2019年/4/17
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravityBody : MonoBehaviour {
    public  bool GravitySwitch;
    RaycastHit hit;
    RaycastHit hitLog;
    RaycastHit Previoushit;
    public Transform Bottom1;
    public Transform Bottom2;//Rayの原点
    bool LostFlg;
    Vector3 NowNormal;
    private GameObject MyGameObject;
    public float Gravity;
    // Use this for initialization
    void Start()
    {
        /*リジットボディを固定*/
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        this.GetComponent<Rigidbody>().useGravity = false;
        MyGameObject = this.gameObject;
        LostFlg = true;
        GravitySwitch = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (GravitySwitch)
        {
            Vector3 NormalVec = CheckNormal();
            Attract(MyGameObject, NormalVec);
        }
    }

    /*Rayを飛ばして自分の真下のポリゴンを取得*/
    RaycastHit CheckPolygonToRayCast()
    {

        float DistanceLog=999999999;
        
            for (int i = 0; i < 360; i += 60)
            {
                for (int j = 0; j < 360; j += 60)
                {
                    for (int k = 0; k < 360; k += 60)
                    {
                        if (Physics.Raycast(Bottom1.position, new Vector3(i - 180, j - 180, k - 180), out hit, Mathf.Infinity))
                        {
                            if (hit.collider.transform.tag == "Coral")
                            {
                                if (hit.distance < DistanceLog)
                                {
                                    hitLog = hit;
                                    DistanceLog = hitLog.distance;
                                }
                            }
                        }
                    }
                }
            }
            LostFlg = false;
            return hitLog;

    }

    //指定されたタグの中で最も近いものを取得
    GameObject serchTag(Transform OriginObject, string TagName)
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



    //Rayを飛ばして接点の放線ベクトルを取得
    Vector3 CheckNormal()
    {
        RaycastHit hit;
        hit = CheckPolygonToRayCast();
        NowNormal = hit.normal;
        return hit.normal;
    }

   
    //取得した放線ベクトルの逆向きに重力を与える
    public void Attract(GameObject body, Vector3 NormalVec)
    {
        //Vector3 GravityUp = (body.transform.position - transform.position).normalized;
        Vector3 GravityUp = NormalVec;
        Vector3 BodyUp = body.transform.up;


        body.GetComponent<Rigidbody>().AddForce(GravityUp * Gravity);
        Quaternion TargetRotation = Quaternion.FromToRotation(BodyUp, GravityUp) * body.transform.rotation;
        body.transform.rotation = Quaternion.Slerp(body.transform.rotation, TargetRotation,   10*Time.deltaTime);
    }
    Vector3 GetNowNormal(){return NowNormal;}
    public void SetGravitySwitch() { GravitySwitch = false; }
}
