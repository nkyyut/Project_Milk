using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTest : MonoBehaviour
{
    [SerializeField] GameObject _particleObj;

    void Start()
    {
        
    }

    void Update()
    {

    }

    public void Ring_Effect(List<Vector3> vec)
    {
        for(int i=0; i<vec.Count; i++)
        {
            Instantiate(_particleObj, vec[i] , this.transform.rotation);
            
        }
    }

}
