using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizmo : MonoBehaviour {

    private void OnDrawGizmos()
    {

        var preColor = Gizmos.color;
        var preMatrix = Gizmos.matrix;
        Gizmos.color = Color.blue;
        Gizmos.matrix = transform.localToWorldMatrix;

        // 法線方向にラインを描画
        var mesh = GetComponent<MeshFilter>().sharedMesh;
        for (int i = 0; i < mesh.normals.Length; i++)
        {
            var from = mesh.vertices[i];
            var to = from + mesh.normals[i];
            Gizmos.DrawLine(from, to);
        }

        Gizmos.color = preColor;
        Gizmos.matrix = preMatrix;
    }
}
