using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class CreateMesh : MonoBehaviour
{
    private MeshFilter _meshFilter;
    private Mesh _mesh;
    public Vector3[] _vertices; // ポリゴンの頂点
    void Start()
    {
        Create();
    }

    public void Create()
    {
        // 最小構成である三点ポリゴンを作る
        _mesh = new Mesh();

        _vertices = new Vector3[]{
            new Vector3 ( 0.0f, 5.0f , 0.0f),
            new Vector3 ( 5.0f, 0.0f , 0.0f),
            new Vector3 (-5.0f, 0.0f , 0.0f),
        };


        var triangles = new int[]{
            0,1,2
        };

        _mesh.vertices = _vertices;
        _mesh.triangles = triangles;

        _meshFilter = GetComponent<MeshFilter>();
        _meshFilter.mesh = _mesh;
    }
}
