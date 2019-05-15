using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DurableValueManager : MonoBehaviour {
    public GameObject[] CoralPartsArray;
    int RecoveryPoint;
    float InitAreaValue;
    float TotalAreaValue;
    float NowDurableValue;
    public GameObject DurableIMG;
    public GameObject DurableTXT;
    Image DurableImg;
    Text DurableTxt;
    public Sprite[] ImageChangeSpriteArray;
    public Material[] TextChangeColorArray;
    int ChangeColorPoint;
    float ChangeColorValue;

    // Use this for initialization
    void Start () {
        Initialize();
        //ChangeString(100);
        //ChangeColor(TextChangeColorArray[0]);
        SubMeshArea(CoralPartsArray[0]);

    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(TotalAreaValue);
	}


    /*いろいろ初期化*/
    void Initialize()
    {
        RecoveryPoint = 0;
        InitAreaValue = CheckTotalArea();
        TotalAreaValue = InitAreaValue;
        NowDurableValue = KiyohitoConst.Const.DurableValueMax;
        CoralPartsArray = GameObject.FindGameObjectsWithTag("Coral");
        ChangeColorPoint = 1;
        ChangeColorValue = KiyohitoConst.Const.DurableValueMax / TextChangeColorArray.Length;
        DurableImg = DurableIMG.gameObject.GetComponent<Image>();
        DurableTxt = DurableTXT.gameObject.GetComponent<Text>();
    }

    ///*メッシュの体積を算出*/
    //public float CalculateMeshVolume(GameObject Parts)
    //{
    //    if (Parts == null)
    //    {
    //        Debug.Log("err");
    //        return 0;

    //    }

    //    float MeshVolume=0;
    //    MeshFilter MeshFilter = Parts.GetComponent<MeshFilter>()/*CoralPartsArray[i].GetComponent<MeshFilter>()*/;
    //    if (MeshFilter == null) return 0;
    //    Mesh Mesh = MeshFilter.sharedMesh;



    //    Vector3[] vertices = Mesh.vertices;
    //    int[] triangles = Mesh.triangles;


    //    for (int i = 0; i < triangles.Length; i += 3)
    //    {
    //        Vector3 p1 = vertices[triangles[i + 0]];
    //        Vector3 p2 = vertices[triangles[i + 1]];
    //        Vector3 p3 = vertices[triangles[i + 2]];
    //        MeshVolume += Vector3.Dot(p1, Vector3.Cross(p2, p3)) / 6.0f;
    //    }
    //    Vector3 scale = Parts.transform.lossyScale;
    //    MeshVolume = MeshVolume * scale.x * scale.y * scale.z;
    //    return Mathf.Abs(MeshVolume);
    //}

    /*メッシュの面積を算出*/
    public float CalculateMeshArea(GameObject Parts)
    {
        if (Parts == null)
        {
            Debug.Log("err");
            return 0;

        }

        float MeshArea=0;
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
            float Area_ABC = 0;
            ForeignProduct_AB_AC = Vector3.Cross(VectorAB, VectorAC);
            //Vector3 scale = Parts.transform.lossyScale;
            //ForeignProduct_AB_AC = new Vector3(ForeignProduct_AB_AC.x * scale.x, ForeignProduct_AB_AC.y * scale.y, ForeignProduct_AB_AC.z * scale.z);

            Area_ABC = ForeignProduct_AB_AC.magnitude / 2;
            MeshArea += Area_ABC;
        }
        return Mathf.Abs(MeshArea);
    }

    void ChangeSprite(int Index)
    {
        DurableImg.sprite = ImageChangeSpriteArray[Index];
    }

    public void ChangeString(int NewDurableValue)
    {
        DurableTxt.text = NewDurableValue.ToString() + "%";
    }

    void ChangeColor(Material NewColor)
    {
        DurableTxt.color = NewColor.color;
    }

    float CheckTotalArea()
    {
        float TotalArea = 0;

        for (int i = 0; i < CoralPartsArray.Length; i++)
        {
            TotalArea += CoralPartsArray[i].GetComponent<CoralStatus>().GetArea();
        }
        return TotalArea;
    }

    public void SubMeshArea(GameObject SubObject)
    {
        float SubValue = CalculateMeshArea(SubObject);
        TotalAreaValue -= SubValue;
    }

    public void AddRecoveryPoint()
    {
        RecoveryPoint++;
        if (RecoveryPoint>10)
        {
            Recover();
        }
    }

    void Recover()
    {
        TotalAreaValue = InitAreaValue;
    }

}
