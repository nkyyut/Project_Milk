using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoralStatus : MonoBehaviour {
    public DurableValueManager DurableValueManager;
    float MyMeshVolume;
    float InitMyMeshVolum;
	// Use this for initialization
	void Start () {
        Initialize();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void Initialize()
    {
        float Volume;
        Volume=CalculateMeshVolume(this.gameObject);
        MyMeshVolume = Volume;
        InitMyMeshVolum = Volume;
    }

    void CheckNowVolume()
    {
        float Volume;
        Volume =CalculateMeshVolume(this.gameObject);
    }

    /*メッシュの体積を算出*/
    public float CalculateMeshVolume(GameObject Parts)
    {
        if (Parts == null)
        {
            Debug.Log("err");
            return 0;

        }

        float MeshVolume = 0;
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
    float GetVolume() { return MyMeshVolume; }
    //public void SetDurableValue(float NewValue) { MyMeshVolume = NewValue; }
}
