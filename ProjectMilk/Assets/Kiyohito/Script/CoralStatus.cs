using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DefaultExecutionOrder(-1)]//タスク優先度を上げる
public class CoralStatus : MonoBehaviour {
    float MyMeshArea;
    float InitMyMeshArea;
	// Use this for initialization
	void Start () {
        Initialize();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void Initialize()
    {
        float Area;
        Area=CalculateMeshArea(this.gameObject);
        Debug.Log(this.gameObject.name+Area);
        MyMeshArea = Area;
        InitMyMeshArea = Area;
    }

    void CheckNowArea()
    {
        float Volume;
        Volume =CalculateMeshArea(this.gameObject);

    }

    /*メッシュの面積を算出*/
    public float CalculateMeshArea(GameObject Parts)
    {
        if (Parts == null)
        {
            Debug.Log("err");
            return 0;

        }
        //表面積を格納する変数の初期化
        float MeshArea = 0.0000000f;
        MeshFilter MeshFilter = Parts.GetComponent<MeshFilter>();
        if (MeshFilter == null) return 0;
        Mesh Mesh = MeshFilter.sharedMesh;


        //頂点情報の格納
        Vector3[] vertices = Mesh.vertices;
        int[] triangles = Mesh.triangles;


        for (int i = 0; i < triangles.Length; i += 3)
        {
            Vector3 p1 = vertices[triangles[i + 0]];
            Vector3 p2 = vertices[triangles[i + 1]];
            Vector3 p3 = vertices[triangles[i + 2]];
            //
            Vector3 VectorAB;
            Vector3 VectorAC;
            Vector3 ForeignProduct_AB_AC;


            //p1とp2をABベクトルとする
            VectorAB = p2 - p1;
            //p1とp3をACベクトルとする
            VectorAC = p3 - p1;

            //三点で形成される三角形の面積を格納する変数
            float Area_ABC=0.00000f;
            //ABベクトルとACベクトルの外積を算出
            ForeignProduct_AB_AC = Vector3.Cross(VectorAB, VectorAC);
            //面積を算出
            Area_ABC = ForeignProduct_AB_AC.magnitude / 2;

            MeshArea += Area_ABC;

        }
        Debug.Log("MeshArea" + MeshArea);
        return Mathf.Abs(MeshArea);
    }



    public float GetArea() { return MyMeshArea; }
}
