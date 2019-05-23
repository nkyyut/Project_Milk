using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutManager : MonoBehaviour
{
    //切断面に適用するマテリアル
    public Material capMaterial;

    GameObject MARUTA;

    private void Start()
    {
        MARUTA = GameObject.Find("MARUTA");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                GameObject[] pieces = MeshCut.Cut(hit.collider.gameObject, this.transform.position, transform.right, capMaterial);

                if (!pieces[1].GetComponent<Rigidbody>())
                    pieces[1].AddComponent<Rigidbody>();

                Destroy(pieces[1], 1);

                GameObject.Destroy(MARUTA.GetComponent<MeshCollider>());

                MARUTA.gameObject.AddComponent<MeshCollider>();
                //MARUTA.gameObject.AddComponent<MeshCollider>().convex = true;

            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 5.0f);
        Gizmos.DrawLine(transform.position + transform.up * 0.5f, transform.position + transform.up * 0.5f + transform.forward * 5.0f);
        Gizmos.DrawLine(transform.position + -transform.up * 0.5f, transform.position + -transform.up * 0.5f + transform.forward * 5.0f);

        Gizmos.DrawLine(transform.position, transform.position + transform.up * 0.5f);
        Gizmos.DrawLine(transform.position, transform.position + -transform.up * 0.5f);
    }

}


