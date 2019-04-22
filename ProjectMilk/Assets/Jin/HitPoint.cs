using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace BLINDED_AM_ME
{

    public class HitPoint : MonoBehaviour
    {
        private GameObject _pointDrawer;
        private PointDrawer _pointDrawer_script;
        bool IsMeshCreate = false;

        void Start()
        {
            _pointDrawer = GameObject.Find("PointDrawer");
            _pointDrawer_script = _pointDrawer.GetComponent<PointDrawer>();
        }

        void Update()
        {

        }

        private void OnTriggerEnter(Collider c)
        {
            if (!IsMeshCreate)
            {
                CreatePoint(c);
                IsMeshCreate = true;
            }
                

        }

        void CreatePoint(Collider c)
        {
            float _dotsize = 0.05f;

            if (c.gameObject.tag == "Line")
            {
                Debug.Log("あたった");

                if (c != _pointDrawer_script._lineList[_pointDrawer_script._lineList.Count - 2].GetComponent<BoxCollider>())
                {
                    Debug.Log("魔法カード発動！");

                    _pointDrawer_script.IsMeshCreate = true;

                    //重なったところに点を打つ
                    Vector3 position = c.ClosestPoint(transform.position);

                    GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    sphere.transform.localScale = Vector3.one * _dotsize;

                    sphere.transform.position = position;
                    
                    _pointDrawer_script._vertices[_pointDrawer_script._vertices.Count-1] = sphere.transform.position;
                    position.z += _pointDrawer_script.CutScaleZ;
                    _pointDrawer_script.back_vertices[_pointDrawer_script.back_vertices.Count-1] = position;


                    //いらない頂点を削除
                    int RemoveIndex = _pointDrawer_script._lineList.IndexOf(c.gameObject);//交差した線のインデックスを取得
                    
                    //削除した点の避難先
                    List<Vector3> vec = new List<Vector3>();
                    List<Vector3> back_vec = new List<Vector3>();

                    //_pointDrawer_script._vertices.Clear();
                    //_pointDrawer_script._lineList.Clear();

                    for(int i=RemoveIndex; 0 <= i; i--)//取得したインデックス以前の頂点を削除
                    {
                        //_pointDrawer_script._vertices[i - RemoveIndex] =_pointDrawer_script._vertices[i];
                        //_pointDrawer_script._vertices.Add(_pointDrawer_script._vertices[i]);
                        //_pointDrawer_script.back_vertices.Add(_pointDrawer_script._vertices[i]);

                        _pointDrawer_script._vertices.RemoveAt(i);
                        vec.Add(_pointDrawer_script._vertices[i]);
                        _pointDrawer_script.back_vertices.RemoveAt(i);
                        back_vec.Add(_pointDrawer_script._vertices[i]);
                        
                    }
                    //for(int i=RemoveIndex; i < _pointDrawer_script._lineList.Count - RemoveIndex; i++)
                    //{
                    //    _pointDrawer_script._lineList.Add(_pointDrawer_script._lineList[i]);
                    //}

                    _pointDrawer_script.MeshCreate();

                }
            }

        }
    }
}
