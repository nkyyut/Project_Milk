//玉那覇臣  4/11
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jin_Const;

namespace BLINDED_AM_ME
{

    public class PointDrawer : MonoBehaviour
    {

        //切断面に適用するマテリアル
        public Material capMaterial;

        //切断するオブジェクト
        GameObject MARUTA;

        int dotnum;
        bool IsChangeDirection;

        [SerializeField]
        private float CutScale;

        [SerializeField]
        private DrawMesh _drawMesh;

        //打った点のマテリアル
        [SerializeField]
        private Material _dotMat;

        [SerializeField]
        private float _dotSize = 0.05f;

        [SerializeField]
        private Material _material;

        [SerializeField]
        private float _threshold = 0.1f;

        private float _sqrThreshold = 0;

        private List<Vector3> _samplingVertices = new List<Vector3>();

        private List<GameObject> _dotList = new List<GameObject>();
        private List<Vector3> _vertices = new List<Vector3>();
        private List<GameObject> _meshList = new List<GameObject>();
        private List<Vector3> _Mesh_vertices = new List<Vector3>();
        private List<Vector3> _subMesh_vertices = new List<Vector3>();

        //奥の頂点リスト
        private List<Vector3> back_vertices = new List<Vector3>();

        private void Start()
        {
            CutScale = 0.2f;
        }


        /// <summary>
        /// Get average point.
        /// マウスの平均位置を取得
        /// </summary>
        private Vector3 AveragePoint
        {
            get
            {
                Vector3 avg = Vector3.zero;
                for (int i = 0; i < _samplingVertices.Count; i++)
                {
                    avg += _samplingVertices[i];
                }
                avg /= _samplingVertices.Count;

                return avg;
            }
        }

        private void Awake()
        {
            MARUTA = GameObject.Find("MARUTA");
            _sqrThreshold = _threshold * _threshold;
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                TryRaycast();
            }

            if (Input.GetMouseButtonUp(0))
            {
                dotnum = 0;
                IsChangeDirection = true;

                int _verticesNum = _vertices.Count;
                int poriNum = (_vertices.Count + back_vertices.Count) * 3;


                for (int i = 0; i < back_vertices.Count; i++)
                {
                    //奥のMeshObjectを作るための頂点を打つ
                    CreateDot(back_vertices[i]);
                }

                _subMesh_vertices.AddRange(_vertices);
                _subMesh_vertices.AddRange(back_vertices);

                //手前と奥の間にメッシュを作るための準備
                Mesh mesh = new Mesh();
                //頂点群の位置座標
                mesh.vertices = new Vector3[poriNum];
                mesh.SetVertices(_subMesh_vertices);

                //決めた順番をセット
                mesh.SetTriangles(OrderTriangles(), 0);


                //meshob.AddComponent<Rigidbody>();

                //for (int i = 0; i < poriNum; i += 3)
                //{
                //    Vector3 side1 = mesh.vertices[i + 1] - mesh.vertices[i];
                //    Vector3 side2 = mesh.vertices[i + 2] - mesh.vertices[i];
                //    Vector3 normal = Vector3.Cross(side1, side2);
                //    normal = normal.normalized;
                //    if (normal.y < 0)
                //    {
                //        //頂点の順番の入れ替え
                //        //for (int m = 0; m < mesh.subMeshCount; m++)
                //        //{ //ポリゴンのインデックスを取得する
                //        int[] triangles = mesh.GetTriangles(0);
                //        //for (int i = 0; i < triangles.Length; i += 3)//ポリゴンなので３つずつインクリメントしてループ
                //        //{ //２番目と３番目の頂点インデックスを入れ替えて三角形を反転する
                //        int index = triangles[0 + 1];
                //        triangles[0 + 1] = triangles[0 + 2];
                //        triangles[0 + 2] = index;
                //        //} //ポリゴンのインデックスをメッシュに戻す
                //        mesh.SetTriangles(triangles, 0);
                //        //} //法線を再計算する 
                //        mesh.RecalculateNormals();

                //    }

                //}




                //手前のオブジェクト生成
                GameObject go = _drawMesh.CreateMesh(_vertices);
                go.GetComponent<MeshRenderer>().material = _material;
                go.transform.position += go.transform.forward * -0.001f;
                _meshList.Add(go);
                //go.AddComponent<Rigidbody>();

                //奥のオブジェクト生成
                GameObject go2 = _drawMesh.CreateMesh(back_vertices);
                go2.GetComponent<MeshRenderer>().material = _material;
                go2.transform.position += go2.transform.forward * -0.001f;
                //go2.AddComponent<Rigidbody>();
                _meshList.Add(go2);

                //手前と奥の間にメッシュオブジェクトを作成
                GameObject meshob = new GameObject("MeshObject", typeof(MeshFilter), typeof(MeshRenderer));
                mesh.RecalculateNormals();//法線の再設定
                meshob.GetComponent<MeshRenderer>().material = _material;
                MeshFilter filter = meshob.GetComponent<MeshFilter>();
                filter.mesh = mesh;
                _meshList.Add(meshob);

                //メッシュを表示するため
                go.gameObject.AddComponent<MeshInfo>();
                go2.gameObject.AddComponent<MeshInfo>();
                meshob.gameObject.AddComponent<MeshInfo>();

            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                Clear();
            }
        }

        //三角頂点の順番決め
        private int[] OrderTriangles()
        {
            //頂点を結ぶのに必要な数
            int PorigonAraay = _subMesh_vertices.Count * _JinConst.ORDERTRIANGLES_PORIGON;

            //三角頂点（ポリゴン）用のint配列
            int[] meshTriangles = new int[PorigonAraay];

            //頂点を結ぶ順番を決める処理
            int j = 0;
            for (int i = 0; i < PorigonAraay; i += _JinConst.ORDERTRIANGLES_NUM)
            {
                if (!(i == PorigonAraay - _JinConst.ORDERTRIANGLES_NUM))
                {
                    meshTriangles[i] = j;
                    meshTriangles[i + 1] = j + _vertices.Count;
                    meshTriangles[i + 2] = j + 1;
                }
                else
                {
                    meshTriangles[i] = j;
                    meshTriangles[i + 1] = j + _vertices.Count;
                    meshTriangles[i + 2] = 0;
                }
                j++;
            }
            j = 0;
            for (int i = 3; i < PorigonAraay; i += _JinConst.ORDERTRIANGLES_NUM)
            {
                if (!(i == PorigonAraay - _JinConst.ORDERTRIANGLES_PORIGON))
                {
                    meshTriangles[i] = j + 1;
                    meshTriangles[i + 1] = j + _vertices.Count;
                    meshTriangles[i + 2] = j + _vertices.Count + 1;
                }
                else
                {
                    meshTriangles[i] = 0;
                    meshTriangles[i + 1] = j + _vertices.Count;
                    meshTriangles[i + 2] = _vertices.Count;
                }
                j++;
            }

            return meshTriangles;
        }

        /// <summary>
        /// Try raycast to the plane.
        /// </summary>
        private void TryRaycast()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, float.MaxValue))
            {
                //奥の頂点
                Vector3 pos = hit.point;
                pos.z += CutScale;

                //最初の頂点
                if (_vertices.Count == 0)
                {
                    //手前の頂点
                    AddVertex(hit.point);
                    //奥の頂点
                    back_vertices.Add(pos);
                    return;
                }

                if (_samplingVertices.Count == 0)
                {
                    _samplingVertices.Add(hit.point);
                    return;
                }

                float distance = (AveragePoint - hit.point).sqrMagnitude;
                if (distance >= _sqrThreshold)
                {
                    AddVertex(hit.point);
                    //奥の頂点リスト追加(点を打つのはまだ)
                    back_vertices.Add(pos);

                    LineCreate();

                    //GameObject pl = GameObject.CreatePrimitive(PrimitiveType.Plane);

                    //pl.transform.localScale = Vector3.one * distance;
                    //pl.transform.position = _dotList[_dotList.Count - 2].transform.position;
                    //if(_dotList[_dotList.Count - 1].transform.position.x > _dotList[_dotList.Count - 2].transform.position.x)
                    //{
                    //    float dis = (_dotList[_dotList.Count - 1].transform.position - _dotList[_dotList.Count - 2].transform.position).sqrMagnitude;

                    //    Vector3 vec = pl.transform.position;
                    //    vec.x += dis;
                    //    pl.transform.position = vec;
                    //}
                    //else
                    //{
                    //    float dis = (_dotList[_dotList.Count - 2].transform.position - _dotList[_dotList.Count - 1].transform.position).sqrMagnitude;

                    //    Vector3 vec = pl.transform.position;
                    //    vec.x -= dis;
                    //    pl.transform.position = vec;
                    //}

                    //var aim = _dotList[_dotList.Count -1].transform.position - _dotList[_dotList.Count - 2].transform.position;
                    //var look = Quaternion.LookRotation(aim);
                    //pl.transform.localRotation = look;
                }
                else
                {
                    //仮の点
                    _samplingVertices.Add(hit.point);
                    //_samplingVertices.Add(pos);
                }
            }
        }

        private void LineCreate()
        {
            List<Vector3> myPoint = new List<Vector3>();
            myPoint.Add(_dotList[0].transform.position);
            myPoint.Add(_dotList[1].transform.position);

            GameObject newLine = new GameObject("Line");
            LineRenderer lRend = newLine.AddComponent<LineRenderer>();
            lRend.SetVertexCount(2);
            lRend.SetWidth(0.2f, 0.2f);
            Vector3 startVec = myPoint[0];
            Vector3 endVec = myPoint[1];
            lRend.SetPosition(0, startVec);
            lRend.SetPosition(1, endVec);
        }

        private void AddVertex(Vector3 point)
        {
            CreateDot(point);
            _vertices.Add(point);
            _samplingVertices.Clear();
        }

        /// <summary>
        /// Create dot for clicked poisition.
        /// </summary>
        /// <returns>Dot GameObject.</returns>
        private GameObject CreateDot(Vector3 position)
        {
            //Debug.Log("Create dot.");

            GameObject dot = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            //名前付け
            dot.name = "dot" + dotnum;
            dotnum++;
            if (IsChangeDirection)
            {
                dot.transform.rotation = Quaternion.AngleAxis(180, new Vector3(0, 0, 1));
            }


            dot.transform.localScale = Vector3.one * _dotSize;
            dot.transform.position = position;
            dot.GetComponent<MeshRenderer>().material = _dotMat;
            Destroy(dot.GetComponent<Collider>());

            _dotList.Add(dot);

            return dot;
        }

        public void Clear()
        {
            for (int i = 0; i < _dotList.Count; i++)
            {
                Destroy(_dotList[i]);
            }
            _dotList.Clear();

            for (int i = 0; i < _meshList.Count; i++)
            {
                Destroy(_meshList[i]);
            }
            _meshList.Clear();
        }
    }
}
