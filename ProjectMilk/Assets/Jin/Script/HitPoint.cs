using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HitPoint : MonoBehaviour
{
    private GameObject _pointDrawer;
    private Jin_PointDrawer _pointDrawer_script;
    private GameObject Player;
    private Footprints footscript;
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
        Player = GameObject.FindGameObjectWithTag("Player");
        footscript = Player.GetComponent<Footprints>();
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider c)
    {
        if (!IsMeshCreate)
        {
            //colList.Add(c.gameObject);
            //Debug.Log(c.gameObject);
            CreatePoint(c);
            IsMeshCreate = true;

        }


    }

    void CreatePoint(Collider c)
    {
        float _dotsize = 0.005f;

        if (c.gameObject.tag == "Line")
        {
            //if (c != _pointDrawer_script._lineList[_pointDrawer_script._lineList.Count - 2].GetComponent<BoxCollider>())
            //{
                Debug.Log("囲む");

                //重なったところに点を打つ
                Vector3 position = c.ClosestPoint(transform.position);

                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.localScale = Vector3.one * _dotsize;

                sphere.transform.position = position;
                sphere.GetComponent<SphereCollider>().enabled = false;
                sphere.GetComponent<MeshRenderer>().enabled = false;
                _pointDrawer_script._vertices[_pointDrawer_script._vertices.Count - 1] = sphere.transform.position;

                //いらない頂点を削除
                int RemoveIndex = _pointDrawer_script._lineList.IndexOf(c.gameObject);//交差した線のインデックスを取得
                //削除した点の避難先
                List<Vector3> vec = new List<Vector3>();
                List<Vector3> back_vec = new List<Vector3>();
                List<GameObject> dot = new List<GameObject>();

                for (int i = RemoveIndex; 0 <= i; i--)//取得したインデックス以前の頂点を削除
                {

                    _pointDrawer_script._vertices.RemoveAt(i);
                    //vec.Add(_pointDrawer_script._vertices[i]);
                    _pointDrawer_script.back_vertices.RemoveAt(i);
                    //back_vec.Add(_pointDrawer_script.back_vertices[i]);
                    Destroy(footscript._dotList[i]);
                }
                //_pointDrawer_script.MeshCuting();
                
                _pointDrawer_script.MeshCreate();
                _pointDrawer_script.Clear();
                //footscript.DotClear();
                footscript.Clear();

            //}
        }
    }
}

