using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class ProjectedMesh : MonoBehaviour
{

    // Use for projected mesh.
    private GameObject m_projectedOject;
    private GameObject m_projectedOject2;

    [SerializeField]
    Material _material;
    GameObject Black;
    GameObject _drawmesh;
    DrawMesh _drawmesh_scr;

    bool IsUpdate;

    // Use this for initialization
    void Start()
    {
        Black = GameObject.Find("Sphere");
        _drawmesh = GameObject.Find("DrawMesh");
        _drawmesh_scr = _drawmesh.GetComponent<DrawMesh>();
        CreateProjectedObject();
        //if (IsUpdate)
        TextureMesh();
        //TextureMesh();
        GetComponent<MeshRenderer>().enabled = false;
    }


    // Update is called once per frame
    void Update()
    {
        //MeshFilter meshFilter = GetComponent<MeshFilter>();
        //Matrix4x4 matrix = meshFilter.transform.localToWorldMatrix;
        //Mesh mesh = meshFilter.mesh;
        //Vector3[] verticies = mesh.vertices;
        //int vexReNum = 0;
        //int vexNum = verticies.Length;

        //for (int k = 0; k < verticies.Length / 2; k++)
        //{
        //    for (float j = 0.1f; j < 1.0f; j += 0.1f)
        //    {
        //        vexReNum++;
        //    }
        //}
        //Vector3[] AllVec = new Vector3[vexNum + vexReNum];
        //Vector3[] newVec = new Vector3[vexReNum];
        //int g = 0;
        //for (int k = 0; k < verticies.Length / 2; k++)
        //{

        //    for (float j = 0.1f; j < 1.0f; j += 0.1f)
        //    {
        //        newVec[g] = Vector3.Lerp(verticies[k], verticies[verticies.Length / 2 + k], j);
        //        g++;
        //    }
        //}

        //Array.Copy(verticies, AllVec, verticies.Length);
        //Array.Copy(newVec, 0, AllVec, verticies.Length, newVec.Length);

        ////Debug.Log(verticies.Length);
        ////Debug.Log(newVec.Length);
        ////Debug.Log(AllVec.Length);

        //Array.Resize(ref verticies, AllVec.Length);
        //verticies = AllVec;

        ////for (int h = 0; h < verticies.Length; h++)
        ////{
        ////    GameObject dot = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        ////    dot.transform.position = verticies[h];
        ////    dot.transform.localScale = Vector3.one * 0.025f;
        ////    dot.GetComponent<MeshRenderer>().material = Black.GetComponent<MeshRenderer>().material;
        ////}

        //Vector3[] newVerticies = new Vector3[verticies.Length];

        //int i = 0;
        //foreach (Vector3 vertex in verticies)
        //{
        //    Vector3 vec = matrix.MultiplyPoint(vertex);
        //    vec.y = 1000;
        //    RaycastHit hit;
        //    if (Physics.Raycast(vec, Vector3.down, out hit))
        //    {
        //        Vector3 newVert = vec;
        //        newVert.y = hit.point.y;
        //        newVerticies[i] = newVert;
        //    }
        //    else
        //    {
        //        vec.y = 0;
        //        newVerticies[i] = vec;
        //    }

        //    i++;
        //}

        //Mesh projectedMesh = m_projectedOject.GetComponent<MeshFilter>().mesh;

        //projectedMesh.vertices = newVerticies;
        //projectedMesh.RecalculateNormals();

        //m_projectedOject.GetComponent<MeshFilter>().mesh.vertices = projectedMesh.vertices;

        //m_projectedOject.AddComponent<MeshInfo>();
        //Debug.Log(newVerticies.Length);
        //List<Vector3> vecmesh = new List<Vector3>();
        ////vecmesh.AddRange(newVerticies);
        ////_drawmesh_scr.CreateMesh(vecmesh);

    }

    // Create a projected mesh base.
    void CreateProjectedObject()
    {
        m_projectedOject = new GameObject("ProjectedObject");

        MeshRenderer newRenderer = m_projectedOject.AddComponent<MeshRenderer>();
        MeshFilter newMeshFilter = m_projectedOject.AddComponent<MeshFilter>();
        m_projectedOject.GetComponent<MeshRenderer>().material = _material;

        // Copy a mesh to the game object.
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        Mesh mesh = meshFilter.mesh;

        newMeshFilter.mesh = mesh;
    }

    void TextureMesh()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        Matrix4x4 matrix = meshFilter.transform.localToWorldMatrix;
        Mesh mesh = meshFilter.mesh;
        Vector3[] verticies = mesh.vertices;
        int vexReNum = 0;
        int vexNum = verticies.Length;

        for (int k = 0; k < verticies.Length / 2; k++)
        {
            for (float j = 0.1f; j < 1.0f; j += 0.1f)
            {
                vexReNum++;
            }
        }
        Vector3[] AllVec = new Vector3[vexNum + vexReNum];
        Vector3[] newVec = new Vector3[vexReNum];
        int g = 0;
        for (int k = 0; k < verticies.Length / 2; k++)
        {

            for (float j = 0.1f; j < 1.0f; j += 0.1f)
            {
                newVec[g] = Vector3.Lerp(verticies[k], verticies[verticies.Length / 2 + k], j);
                g++;
            }
        }

        Array.Copy(verticies, AllVec, verticies.Length);
        Array.Copy(newVec, 0, AllVec, verticies.Length, newVec.Length);

        //Debug.Log(verticies.Length);
        //Debug.Log(newVec.Length);
        //Debug.Log(AllVec.Length);

        Array.Resize(ref verticies, AllVec.Length);
        verticies = AllVec;


        Vector3[] newVerticies = new Vector3[verticies.Length];

        int i = 0;
        foreach (Vector3 vertex in verticies)
        {
            Vector3 vec = matrix.MultiplyPoint(vertex);
            vec.y = 1000;
            RaycastHit hit;
            if (Physics.Raycast(vec, Vector3.down, out hit))
            {
                Vector3 newVert = vec;
                newVert.y = hit.point.y;
                newVerticies[i] = newVert;
                //Debug.Log("頂点再計算");
            }
            else
            {
                vec.y = 0;
                newVerticies[i] = vec;
            }

            i++;
        }
       //for (int h = 0; h < newVerticies.Length; h++)
       // {
       //     GameObject dot = GameObject.CreatePrimitive(PrimitiveType.Sphere);

       //     dot.transform.position = newVerticies[h];
       //     dot.transform.localScale = Vector3.one * 0.025f;
       //     dot.GetComponent<MeshRenderer>().material = Black.GetComponent<MeshRenderer>().material;
       // }


        Mesh projectedMesh = m_projectedOject.GetComponent<MeshFilter>().mesh;

        projectedMesh.vertices = newVerticies;
        projectedMesh.RecalculateNormals();

        //m_projectedOject2 = new GameObject("ProjectedObject2");

        //MeshRenderer newRenderer = m_projectedOject2.AddComponent<MeshRenderer>();
        //MeshFilter newMeshFilter = m_projectedOject2.AddComponent<MeshFilter>();
        //newMeshFilter.mesh = projectedMesh;

        //m_projectedOject.transform.position += m_projectedOject.transform.up * 0.05f;

        //m_projectedOject.GetComponent<MeshFilter>().mesh.vertices = projectedMesh.vertices;

        //m_projectedOject.AddComponent<MeshInfo>();
        //Debug.Log(newVerticies.Length);

        //重い
        //List<Vector3> vecmesh = new List<Vector3>();
        //vecmesh.AddRange(newVerticies);
        //_drawmesh_scr.CreateMesh(vecmesh);
    }

    //private void Lerpdot()
    //{
    //    MeshFilter meshFilter = GetComponent<MeshFilter>();
    //    Matrix4x4 matrix = meshFilter.transform.localToWorldMatrix;
    //    Mesh mesh = meshFilter.mesh;
    //    Vector3[] verticies = mesh.vertices;
    //    int vexReNum = 0;
    //    int vexNum = verticies.Length;

    //    for (int k = 0; k < verticies.Length / 2; k++)
    //    {
    //        for (float j = 0.1f; j < 1.0f; j += 0.1f)
    //        {
    //            vexReNum++;
    //        }
    //    }
    //    Vector3[] AllVec = new Vector3[vexNum + vexReNum];
    //    Vector3[] newVec = new Vector3[vexReNum];
    //    int g = 0;
    //    for (int k = 0; k < verticies.Length / 2; k++)
    //    {

    //        for (float j = 0.1f; j < 1.0f; j += 0.1f)
    //        {
    //            newVec[g] = Vector3.Lerp(verticies[k], verticies[verticies.Length / 2 + k], j);
    //            g++;
    //        }
    //    }

    //    Array.Copy(verticies, AllVec, verticies.Length);
    //    Array.Copy(newVec, 0, AllVec, verticies.Length, newVec.Length);

    //    //Debug.Log(verticies.Length);
    //    //Debug.Log(newVec.Length);
    //    //Debug.Log(AllVec.Length);

    //    Array.Resize(ref verticies, AllVec.Length);
    //    verticies = AllVec;
    //}
}