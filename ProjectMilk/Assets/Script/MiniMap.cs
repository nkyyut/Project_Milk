//かずき

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{

    public Transform PlayerTransform;
    [SerializeField] float offset;


    void Start()
    {
        transform.position = PlayerTransform.up * offset;
        transform.rotation = Quaternion.Euler(90, 0, 0);
    }


    void Update()
    {
        transform.position = PlayerTransform.position + PlayerTransform.up * offset;
        transform.rotation = PlayerTransform.rotation;
        transform.Rotate(90, 0, 0);



    }
}
