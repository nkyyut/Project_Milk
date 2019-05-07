using UnityEngine;
using System.Collections;
using System;

public class ProjectedMesh : MonoBehaviour {

    // Use for projected mesh.
    private GameObject m_projectedOject;

    [SerializeField]
    Material _material;

    // Use this for initialization
    void Start () {
        CreateProjectedObject();
        TextureMesh();
        GetComponent<MeshRenderer>().enabled = false;
    }


    // Update is called once per frame
    void Update () {
        //MeshFilter meshFilter   = GetComponent<MeshFilter>();
        //Matrix4x4  matrix       = meshFilter.transform.localToWorldMatrix;
        //Mesh       mesh         = meshFilter.mesh;
        //Vector3[]  verticies    = mesh.vertices;
        

        //for(int k=0; k<verticies.Length / 2; k++)
        //{
        //    for(float j = 0.1f; j<1.0f; j+=0.2f)
        //    {
        //        Vector3 vecpos = Vector3.Lerp(verticies[k], verticies[verticies.Length / 2 + k], j);
        //        //Debug.Log("入る");
        //        //verticies[verticies.Length-1] = vecpos;
        //    }
        //}
        //Vector3[]  newVerticies = new Vector3[verticies.Length];

        //int i = 0;
        //foreach (Vector3 vertex in verticies) {
        //    Vector3 vec = matrix.MultiplyPoint(vertex);
        //    vec.y = 1000;

        //    RaycastHit hit;
        //    if (Physics.Raycast(vec, Vector3.down, out hit)) {
        //        Vector3 newVert = vec;
        //        newVert.y = hit.point.y;
        //        newVerticies[i] = newVert;
        //    }
        //    else {
        //        vec.y = 0;
        //        newVerticies[i] = vec;
        //    }

        //    i++;
        //}

        //Mesh projectedMesh = m_projectedOject.GetComponent<MeshFilter>().mesh;
        
        //projectedMesh.vertices = newVerticies;
        //projectedMesh.RecalculateNormals();
    }

    // Create a projected mesh base.
    void CreateProjectedObject() {
        m_projectedOject = new GameObject("ProjectedObject");

        MeshRenderer newRenderer = m_projectedOject.AddComponent<MeshRenderer>();
        MeshFilter newMeshFilter = m_projectedOject.AddComponent<MeshFilter>();
        m_projectedOject.GetComponent<MeshRenderer>().material = _material;

        // Copy a mesh to the game object.
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        Mesh       mesh       = meshFilter.mesh;

        newMeshFilter.mesh = mesh;
    }

    void TextureMesh()
    {
        MeshFilter meshFilter   = GetComponent<MeshFilter>();
        Matrix4x4  matrix       = meshFilter.transform.localToWorldMatrix;
        Mesh       mesh         = meshFilter.mesh;
        Vector3[]  verticies    = mesh.vertices;
        

        for(int k=0; k<verticies.Length / 2; k++)
        {
            for(float j = 0.1f; j<1.0f; j+=0.2f)
            {
                Vector3 vecpos = Vector3.Lerp(verticies[k], verticies[verticies.Length / 2 + k], j);
                Debug.Log("入る");
                Array.Resize(ref verticies, verticies.Length + 1);
                verticies[verticies.Length - 1] = vecpos;
            }
        }
        Vector3[]  newVerticies = new Vector3[verticies.Length];

        int i = 0;
        foreach (Vector3 vertex in verticies) {
            Vector3 vec = matrix.MultiplyPoint(vertex);
            vec.y = 1000;

            RaycastHit hit;
            if (Physics.Raycast(vec, Vector3.down, out hit)) {
                Vector3 newVert = vec;
                newVert.y = hit.point.y;
                newVerticies[i] = newVert;
            }
            else {
                vec.y = 0;
                newVerticies[i] = vec;
            }

            i++;
        }

        Mesh projectedMesh = m_projectedOject.GetComponent<MeshFilter>().mesh;
        
        projectedMesh.vertices = newVerticies;
        projectedMesh.RecalculateNormals();
    }
}