using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteTriangle : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000.0f))
            {
                deleteTri(hit.triangleIndex);
                Debug.Log(hit.triangleIndex);
            }
        }
    }

    void deleteTri(int index)
    {
        Destroy(this.gameObject.GetComponent<MeshCollider>());
        Mesh mesh = this.transform.GetComponent<MeshFilter>().mesh;
        int[] oldTriangle = mesh.triangles;
        int[] newTriangle = new int[mesh.triangles.Length - 3];

        int i = 0;
        int j = 0;
        while (j < mesh.triangles.Length)
        {
            if (j != index * 3)
            {
                newTriangle[i++] = oldTriangle[j++];
                newTriangle[i++] = oldTriangle[j++];
                newTriangle[i++] = oldTriangle[j++];
            }
            else
            {
                j += 3;
            }
        }
        transform.GetComponent<MeshFilter>().mesh.triangles = newTriangle;
        this.gameObject.AddComponent<MeshCollider>();
    }
}
