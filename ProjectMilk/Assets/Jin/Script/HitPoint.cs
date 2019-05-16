using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HitPoint : MonoBehaviour
{
    private GameObject _pointDrawer;
    private Jin_PointDrawer _pointDrawer_script;
    private bool IsMeshCreate = false;
    private List<GameObject> colList = new List<GameObject>();

    bool GetIsMeshCreate()
    {
        return IsMeshCreate;
    }

    void Start()
    {
        _pointDrawer = GameObject.Find("PointDrawer");
        _pointDrawer_script = _pointDrawer.GetComponent<Jin_PointDrawer>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider c)
    {
        if (!IsMeshCreate)
        {
            //colList.Add(c.gameObject);
            Debug.Log(c.gameObject);
            CreatePoint(c);
            IsMeshCreate = true;

        }


    }

    void CreatePoint(Collider c)
    {
        float _dotsize = 0.005f;
        if (_pointDrawer_script._lineList.Count > 1)
        {
            if (c.gameObject.tag == "Line")
            {
                //Debug.Log("線");
                if (c != _pointDrawer_script._lineList[_pointDrawer_script._lineList.Count - 2].GetComponent<BoxCollider>())
                {
                    Debug.Log("囲む");
                    //_pointDrawer_script.IsMeshCreate = true;

                    //重なったところに点を打つ
                    Vector3 position = c.ClosestPoint(transform.position);

                    GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    sphere.transform.localScale = Vector3.one * _dotsize;

                    sphere.transform.position = position;
                    sphere.GetComponent<SphereCollider>().enabled = false;
                    sphere.GetComponent<MeshRenderer>().enabled = false;
                    _pointDrawer_script._vertices[_pointDrawer_script._vertices.Count - 1] = sphere.transform.position;
                    //position.z += _pointDrawer_script.CutScaleZ;
                    _pointDrawer_script.back_vertices[_pointDrawer_script.back_vertices.Count - 1] = sphere.transform.position + sphere.transform.forward * -0.1f;
                    //Debug.Log(_pointDrawer_script.back_vertices[_pointDrawer_script.back_vertices.Count - 1]);

                    //いらない頂点を削除
                    int RemoveIndex = _pointDrawer_script._lineList.IndexOf(c.gameObject);//交差した線のインデックスを取得

                    //削除した点の避難先
                    List<Vector3> vec = new List<Vector3>();
                    List<Vector3> back_vec = new List<Vector3>();

                    //_pointDrawer_script._vertices.Clear();
                    //_pointDrawer_script._lineList.Clear();

                    for (int i = RemoveIndex; 0 <= i; i--)//取得したインデックス以前の頂点を削除
                    {
                        //_pointDrawer_script._vertices[i - RemoveIndex] =_pointDrawer_script._vertices[i];
                        //_pointDrawer_script._vertices.Add(_pointDrawer_script._vertices[i]);
                        //_pointDrawer_script.back_vertices.Add(_pointDrawer_script._vertices[i]);

                        _pointDrawer_script._vertices.RemoveAt(i);
                        vec.Add(_pointDrawer_script._vertices[i]);
                        _pointDrawer_script.back_vertices.RemoveAt(i);
                        back_vec.Add(_pointDrawer_script.back_vertices[i]);

                    }
                    //for(int i=RemoveIndex; i < _pointDrawer_script._lineList.Count - RemoveIndex; i++)
                    //{
                    //    _pointDrawer_script._lineList.Add(_pointDrawer_script._lineList[i]);
                    //}

                    //float distance = (_pointDrawer_script._vertices[0] - _pointDrawer_script._vertices[5]).sqrMagnitude;

                    //Vector3 Lerp = Vector3.Lerp(_pointDrawer_script._vertices[0], _pointDrawer_script._vertices[5] , 0.5f);

                    //GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    //obj.transform.localScale = Vector3.one * 0.05f;
                    //obj.transform.position = Lerp;

                    //_pointDrawer_script.MeshCuting();
                    //Debug.Log("HitPoint");

                    _pointDrawer_script.MeshCreate();
                    _pointDrawer_script.Clear();
                    _pointDrawer_script.GetFootprints().Clear();


                }
            }
        }
    }
}

