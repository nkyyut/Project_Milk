using UnityEngine;
using System.Collections;

public class SourcePM : MonoBehaviour {

    // Use for projected mesh.
    private GameObject m_projectedOject;
    private GameObject _FrontMesh;
    private GameObject _PointDrawer;
    private PointDrawer PointDrawer_Scri;
    bool IsDrawMesh;

    public void IsTrue()
    {
        IsDrawMesh = true;
    }

    // Use this for initialization
    void Start () {
        _PointDrawer = GameObject.Find("PointDrawer");
        PointDrawer_Scri = _PointDrawer.GetComponent<PointDrawer>();
        //_FrontMesh = PointDrawer_Scri.GetFrontMesh();
        CreateProjectedObject();
        GetComponent<MeshRenderer>().enabled = false;
    }


    // Update is called once per frame
    void Update () {
        if (IsDrawMesh)
        {
            _FrontMesh = PointDrawer_Scri.GetFrontMesh();
            this.gameObject.GetComponent<MeshFilter>().mesh = _FrontMesh.GetComponent<MeshFilter>().mesh;
        }
            
        MeshFilter meshFilter   = GetComponent<MeshFilter>();
        Matrix4x4  matrix       = meshFilter.transform.localToWorldMatrix;
        Mesh       mesh         = meshFilter.mesh;
        Vector3[]  verticies    = mesh.vertices;
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
        // projectedMesh.RecalculateNormals();
    }

    // Create a projected mesh base.
    void CreateProjectedObject() {
        m_projectedOject = new GameObject("ProjectedObject");

        MeshRenderer newRenderer = m_projectedOject.AddComponent<MeshRenderer>();
        MeshFilter newMeshFilter = m_projectedOject.AddComponent<MeshFilter>();

        // Copy a mesh to the game object.
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        Mesh       mesh       = meshFilter.mesh;

        newMeshFilter.mesh = mesh;
    }
}