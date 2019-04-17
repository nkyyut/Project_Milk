using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DurableValueManager : MonoBehaviour {
    public GameObject[] CoralPartsArray;
    float[] InitEachCoralVolumeArray;
    float[] EachCoralVolumeArray;
    float InitCoralVolume;
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
        ChangeString(100);
        ChangeColor(TextChangeColorArray[0]);
    }
	
	// Update is called once per frame
	void Update () {
	}


    /*いろいろ初期化*/
    void Initialize()
    {
        InitEachCoralVolumeArray = new float[CoralPartsArray.Length];
        EachCoralVolumeArray = new float[CoralPartsArray.Length];
        InitCoralVolume = 0;
        NowDurableValue = KiyohitoConst.Const.DurableValueMax;

        ChangeColorPoint = 1;
        ChangeColorValue = KiyohitoConst.Const.DurableValueMax / TextChangeColorArray.Length;
        DurableImg = DurableIMG.gameObject.GetComponent<Image>();
        DurableTxt = DurableTXT.gameObject.GetComponent<Text>();

        for (int i = 0; i < CoralPartsArray.Length; i++)
        {

            float MeshVolume = CalculateMeshVolume(CoralPartsArray[i]);
            InitEachCoralVolumeArray[i] = MeshVolume;
            EachCoralVolumeArray[i] = MeshVolume;
            InitCoralVolume += MeshVolume;
        }
    }

    /*メッシュの体積を算出*/
    float CalculateMeshVolume(GameObject Parts)
    {
        if (Parts == null)
        {
            Debug.Log("err");
            return 0;

        }

        float MeshVolume=0;
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
            MeshVolume += Vector3.Dot(p1, Vector3.Cross(p2, p3)) / 6.0f;
        }
        Vector3 scale = Parts.transform.lossyScale;
        MeshVolume = MeshVolume * scale.x * scale.y * scale.z;
        return Mathf.Abs(MeshVolume);
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

    public void EntryDurable(GameObject ShavedCoral)
    {
        CalculateMeshVolume(ShavedCoral);
    }
}
