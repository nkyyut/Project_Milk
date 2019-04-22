//玉那覇臣  4/11
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Jin_Const;

namespace BLINDED_AM_ME
{

    public class PointDrawer : MonoBehaviour
    {

        //切断面に適用するマテリアル
        public Material capMaterial;

        [SerializeField]
        GameObject Line;

        //切断するオブジェクト
        GameObject MARUTA;

        int dotnum;
        bool IsChangeDirection;
        public bool IsMeshCreate = false;

        private int AllList_Index;

        [SerializeField]
        public float CutScaleZ;

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
        private List<GameObject> back_dotList = new List<GameObject>();
        public List<Vector3> _vertices = new List<Vector3>();
        private List<GameObject> _meshList = new List<GameObject>();
        private List<Vector3> _subMesh_vertices = new List<Vector3>();
        //奥の頂点リスト
        public List<Vector3> back_vertices = new List<Vector3>();

        public List<GameObject> _lineList = new List<GameObject>();
        private List<List<GameObject>> All_lineList = new List<List<GameObject>>();
        private List<List<Vector3>> All_verticesList = new List<List<Vector3>>();

        private List<Vector3> _target_vertices = new List<Vector3>();


        private void Start()
        {
            CutScaleZ = 0.2f;
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
                //_lineList = new List<GameObject>();
                TryRaycast();
            }

            if (Input.GetMouseButtonUp(0))
            {

                MeshFilter filter = MARUTA.GetComponent<MeshFilter>();
                //Mesh _targetMesh = MARUTA.GetComponent<Mesh>();
                _target_vertices.AddRange(filter.mesh.vertices);

                Vector3 center = Vector3.zero;

                foreach (Vector3 point in _vertices)
                {
                    center += point;
                }
                center = center / _vertices.Count;

                _vertices.Add(center);

                int _target_verticesNum = _target_vertices.Count;

                for (int i = 0; i < _vertices.Count; i++)
                {
                    _target_vertices.Add(_vertices[i]);
                }

                filter.mesh.SetVertices(_target_vertices);

                int[] triangles = filter.mesh.triangles;

                //for (int i = 0; i < _vertices.Count; i += 3)
                //{
                triangles[_target_verticesNum] = (_target_verticesNum - 1) + (_vertices.Count - 1);
                Debug.Log(triangles[_target_verticesNum]);

                triangles[_target_verticesNum + 1] = _target_verticesNum;
                Debug.Log(triangles[_target_verticesNum + 1]);

                triangles[_target_verticesNum + 2] = _target_verticesNum + 1;
                Debug.Log(triangles[_target_verticesNum + 2]);
                //}

                Debug.Log(center);
                Debug.Log(_target_vertices[_target_vertices.Count - 1]);
                Debug.Log(triangles[(_target_verticesNum - 1) + (_vertices.Count - 1)]);
                //Debug.Log(triangles[_target_verticesNum + 1]);

                filter.mesh.SetTriangles(triangles, 0);

                Destroy(MARUTA.GetComponent<MeshCollider>());
                MARUTA.AddComponent<MeshCollider>();

                Debug.Log(_target_vertices.Count);

                //List<GameObject> Sample_lineList = _lineList;
                //List<Vector3> Sample_verticesList = _vertices;

                //All_lineList.Add(Sample_lineList);
                //All_verticesList.Add(Sample_verticesList);

                //for(int i = 0; i < _vertices.Count; i++)
                //{
                //    //Vector3[][] vec = new Vector3[AllList_Index][i];


                //}

                //if (IsMeshCreate)
                //    MeshCreate();

                //Debug.Log(All_lineList[0].Count);

                //Debug.Log(All_lineList[0]);
                //Debug.Log(All_lineList[1]);
                //Debug.Log(All_verticesList[0]);
                //Debug.Log(All_verticesList[1]);
                Initialize();
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                Clear();
            }
        }

        public void Initialize()
        {
            _vertices.Clear();
            back_vertices.Clear();

            _dotList.Clear();
            _lineList.Clear();
        }
        /// <summary>
        /// Meshの生成
        /// </summary>
        public void MeshCreate()
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

            //手前のオブジェクト生成
            GameObject go = _drawMesh.CreateMesh(_vertices);
            go.GetComponent<MeshRenderer>().material = _material;
            go.transform.position += go.transform.forward * -0.001f;
            Vector3 pos = go.transform.localPosition;
            pos.z -= 0.1f;
            go.transform.localPosition = pos;
            _meshList.Add(go);
            //go.AddComponent<Rigidbody>();

            //奥のオブジェクト生成
            GameObject go2 = _drawMesh.CreateMesh(back_vertices);
            go2.GetComponent<MeshRenderer>().material = _material;
            go2.transform.position += go2.transform.forward * -0.001f;
            Vector3 pos2 = go2.transform.localPosition;
            pos2.z -= 0.1f;
            go2.transform.localPosition = pos2;
            //go2.AddComponent<Rigidbody>();
            _meshList.Add(go2);

            //手前と奥の間にメッシュオブジェクトを作成
            GameObject meshob = new GameObject("MeshObject", typeof(MeshFilter), typeof(MeshRenderer));
            mesh.RecalculateNormals();//法線の再設定
            meshob.GetComponent<MeshRenderer>().material = _material;
            Vector3 pos3 = meshob.transform.localPosition;
            pos3.z -= 0.1f;
            meshob.transform.localPosition = pos3;
            MeshFilter filter = meshob.GetComponent<MeshFilter>();
            filter.mesh = mesh;
            _meshList.Add(meshob);

            //メッシュを表示するため
            go.gameObject.AddComponent<MeshInfo>();
            go2.gameObject.AddComponent<MeshInfo>();
            meshob.gameObject.AddComponent<MeshInfo>();

            //前作ったメッシュとつながってしまうためクリア
            _subMesh_vertices.Clear();
            IsChangeDirection = false;
            //_vertices.Clear();
            //back_dotList.Clear();
        }
        /// <summary>
        /// 三角頂点の順番決め
        /// </summary>       
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
        /// クリックした位置に点を打ち、ドラッグで一定間隔に点を打っていく
        /// </summary>
        private void TryRaycast()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, float.MaxValue))
            {
                //奥の頂点
                Vector3 pos = hit.point;
                pos.z += CutScaleZ;

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

                }
                else
                {
                    //仮の点
                    _samplingVertices.Add(hit.point);
                    //_samplingVertices.Add(pos);
                }
            }
        }
        /// <summary>
        /// 囲むための判定用に頂点間にCube(線)を生成
        /// </summary>   
        private void LineCreate()
        {
            List<Vector3> myPoint = new List<Vector3>();
            myPoint.Add(_dotList[_dotList.Count - 2].transform.position);
            myPoint.Add(_dotList[_dotList.Count - 1].transform.position);

            GameObject obj = Instantiate(Line, transform.position, transform.rotation) as GameObject;
            obj.transform.position = (myPoint[0] + myPoint[1]) / 2;
            obj.transform.right = (myPoint[1] - myPoint[0]).normalized;
            obj.transform.localScale = new Vector3((myPoint[1] - myPoint[0]).magnitude, _dotSize, _dotSize);
            obj.tag = "LastLine";
            _lineList.Add(obj);
            if (_lineList.Count > 1)
            {
                Destroy(_lineList[_lineList.Count - 2].GetComponent<HitPoint>());
                _lineList[_lineList.Count - 2].tag = "Line";
            }

            obj.GetComponent<BoxCollider>().isTrigger = true;
            obj.AddComponent<Rigidbody>();
            obj.GetComponent<Rigidbody>().isKinematic = true;

            obj.AddComponent<HitPoint>();
        }

        /// <summary>
        /// 頂点リストに追加
        /// </summary>
        /// <param name="point"></param>
        private void AddVertex(Vector3 point)
        {
            CreateDot(point);
            _vertices.Add(point);
            _samplingVertices.Clear();
        }

        /// <summary>
        /// 点（頂点）を生成
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
            if (!IsChangeDirection)
                _dotList.Add(dot);
            else
                back_dotList.Add(dot);

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
