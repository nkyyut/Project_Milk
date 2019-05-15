using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DefaultExecutionOrder(-1)]
public class CoralStatus : MonoBehaviour {
    //public DurableValueManager DurableValueManager;
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
        MyMeshArea = Area;
        InitMyMeshArea = Area;
    }

    //void CheckNowVolume()
    //{
    //    float Volume;
    //    Volume =CalculateMeshArea(this.gameObject);
    //    //Debug.Log("Now");

    //}

    /*メッシュの面積を算出*/
    public float CalculateMeshArea(GameObject Parts)
    {
        if (Parts == null)
        {
            Debug.Log("err");
            return 0;

        }

        float MeshArea = 0.0000000f;
        MeshFilter MeshFilter = Parts.GetComponent<MeshFilter>()/*CoralPartsArray[i].GetComponent<MeshFilter>()*/;
        if (MeshFilter == null) return 0;
        Mesh Mesh = MeshFilter.sharedMesh;



        Vector3[] vertices = Mesh.vertices;
        int[] triangles = Mesh.triangles;


        for (int i = 0; i < triangles.Length; i += 3)
        {
            Vector3 p1 = vertices[triangles[i + 0]];
            Vector3 p2 = vertices[triangles[i + 1]];
            Vector3 p3 = vertices[triangles[i + 2]];

            Vector3 VectorAB;
            Vector3 VectorAC;
            Vector3 ForeignProduct_AB_AC;

            VectorAB = p2 - p1;
            VectorAC = p3 - p1;
            float Area_ABC=0.00000f;
            ForeignProduct_AB_AC = Vector3.Cross(VectorAB, VectorAC);
            Vector3 scale = Parts.transform.lossyScale;
            ForeignProduct_AB_AC=new Vector3(ForeignProduct_AB_AC.x * scale.x, ForeignProduct_AB_AC.y * scale.y, ForeignProduct_AB_AC.z * scale.z);

            Area_ABC = ForeignProduct_AB_AC.magnitude / 2;

            MeshArea += Area_ABC;

        }
        return Mathf.Abs(MeshArea);
    }



    public float GetArea() {
        //Debug.Log("MyMeshArea"+MyMeshArea);
        return MyMeshArea;
    }
    //public void SetDurableValue(float NewValue) { MyMeshVolume = NewValue; }
}
