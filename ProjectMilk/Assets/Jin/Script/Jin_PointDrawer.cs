//玉那覇臣  4/11
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Jin_Const;
using System;

public class Jin_PointDrawer : MonoBehaviour
{

    //切断面に適用するマテリアル
    public Material capMaterial;

    [SerializeField]
    GameObject Line;

    //切断するオブジェクト
    GameObject MARUTA;

    public GameObject FrontMesh;
    public GameObject _AllMeshObject;

    int dotnum;
    bool IsChangeDirection;
    public bool IsMeshCreate;

    private int AllList_Index;

    [SerializeField]
    public float CutScaleZ = 0.2f;

    [SerializeField]
    private DrawMesh _drawMesh;

    [SerializeField]
    private Footprints _footPrints;

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
    public List<GameObject> _meshList = new List<GameObject>();
    private List<Vector3> _subMesh_vertices = new List<Vector3>();
    //奥の頂点リスト
    public List<Vector3> back_vertices = new List<Vector3>();

    public List<GameObject> _lineList = new List<GameObject>();
    private List<List<GameObject>> All_lineList = new List<List<GameObject>>();
    private List<List<Vector3>> All_verticesList = new List<List<Vector3>>();

    private List<Vector3> _target_vertices = new List<Vector3>();
    private List<Vector3> _localvertices = new List<Vector3>();

    private List<Vector3> _AllVec = new List<Vector3>();

    public Footprints GetFootprints()
    {
        return _footPrints;
    }

    public GameObject GetFrontMesh()
    {
        return FrontMesh;
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
        IsMeshCreate = false;
        _sqrThreshold = _threshold * _threshold;
    }

    private void Update()
    {
        if(_AllMeshObject)
            if(_AllMeshObject.transform.position.y <= -5)
                Destroy(_AllMeshObject);

        //if (Input.GetMouseButton(0))
        //{
        //    //_lineList = new List<GameObject>();
        //    TryRaycast();
        //}

        if (Input.GetMouseButtonUp(0))
        {

            //MeshFilter filter = MARUTA.GetComponent<MeshFilter>();
            //_target_vertices.AddRange(filter.mesh.vertices);

            //int _target_verticesNum = _target_vertices.Count;

            //int[] triangles = filter.mesh.triangles;

            //_target_vertices.AddRange(_localvertices);
            //filter.mesh.SetVertices(_target_vertices);

            //int _target_trianglesNum = filter.mesh.triangles.Count();


            ////点から一番近い
            //int[] MinTriangle = new int[_localvertices.Count];
            //float min = 0;
            //for (int i = 0; i < _localvertices.Count; i++)
            //{
            //    min = 99;
            //    for (int k = 0; k < _target_verticesNum - 1; k++)
            //    {
            //        //float dis = (_localvertices[i] - _target_vertices[k]).sqrMagnitude;
            //        float dis = Vector3.Distance(_localvertices[i], _target_vertices[k]);

            //        if (dis < min)
            //        {
            //            min = dis;
            //            MinTriangle[i] = k;
            //        }
            //    }
            //}
            //Debug.Log(min);
            //for(int i=0; i<MinTriangle.Length; i++)
            //    Debug.Log(MinTriangle[i]);

            //for (int i = 0; i < MinTriangle.Length; i++)
            //{
            //    //int num = Array.IndexOf(triangles, MinTriangle[i]);
            //    //Debug.Log(num);
            //    //if (num % 3 == 0)
            //    //{
            //    //    triangles[num + 1] = _target_verticesNum + i;
            //    //    triangles[num + 2] = _target_verticesNum + i + 1;
            //    //    Debug.Log("並べ替え");
            //    //}
            //    //else if (num % 3 == 1)
            //    //{
            //    //    triangles[num - 1] = _target_verticesNum + i;
            //    //    triangles[num + 1] = _target_verticesNum + i + 1;
            //    //    Debug.Log("並べ替え2");
            //    //}
            //    //else if (num % 3 == 2)
            //    //{
            //    //    triangles[num - 1] = _target_verticesNum + i;
            //    //    triangles[num - 2] = _target_verticesNum + i + 1;
            //    //}
            //}

            //int j = 0;

            //Array.Resize(ref triangles, triangles.Length + (_localvertices.Count * 3));
            //for (int i = 0; i < (_localvertices.Count - 1) * 3; i += 3)
            //{
            //    triangles[_target_trianglesNum + i] = MinTriangle[j];
            //    //Debug.Log(_target_vertices[MinTriangle]);
            //    //Debug.Log(triangles[_target_verticesNum]);

            //    triangles[_target_trianglesNum + 1 + i] = _target_verticesNum + j;
            //    //Debug.Log(_target_vertices[_target_verticesNum]);
            //    //Debug.Log(triangles[_target_verticesNum + 1]);

            //    triangles[_target_trianglesNum + 2 + i] = _target_verticesNum + 1 + j;
            //    //Debug.Log(_target_vertices[_target_verticesNum + 1]);
            //    //Debug.Log(triangles[_target_verticesNum + 2]);
            //    j++;
            //}
            //for (int i = 0; i < _localvertices.Count; i++)
            //{
            //    Debug.Log(triangles[_target_trianglesNum + i]);
            //}
            //for (int i = 0; i < _target_verticesNum + _vertices.Count; i++)
            //{
            //    Debug.Log(filter.mesh.vertices[i]);
            //}

            //_target_vertices[35] = Vector3.zero;
            //_target_vertices[36] = Vector3.zero;
            //_target_vertices[_target_verticesNum - 1] = Vector3.zero;
            //_target_vertices[15] = Vector3.zero;
            //filter.mesh.SetVertices(_target_vertices);
            //Debug.Log(triangles.Count());
            //Debug.Log(triangles[triangles.Count() - 1]);
            //Debug.Log(center);
            //Debug.Log(_target_vertices[_target_vertices.Count - 1]);
            //Debug.Log(triangles[_target_verticesNum - 1 + (_vertices.Count - 1)]);
            //Debug.Log(triangles[_target_verticesNum + 1]);

            //filter.mesh.SetTriangles(triangles, 0);

            //Destroy(MARUTA.GetComponent<MeshCollider>());
            //MARUTA.AddComponent<MeshCollider>();

            //Debug.Log(_target_vertices.Count);

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
            //Initialize();
            //Clear();
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

    public void MeshCuting()
    {
        float max = 0;
        Vector3 CutPoint;

        for (int i = 0; i < _vertices.Count; i++)
        {
            float dis = _vertices.First().z - _vertices[i].z;

            if (Mathf.Abs(dis) > max)
            {
                max = Mathf.Abs(dis);
                CutPoint = _vertices[i];
            }
        }
        Debug.Log(max);
        if (max > 0.5f)
        {
            Debug.Log("斬る");
        }
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

        //for (int i = 0; i < back_vertices.Count; i++)
        //{
        //    //奥のMeshObjectを作るための頂点を打つ
        //    CreateDot(back_vertices[i]);
        //}

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
        ////go.transform.position += go.transform.forward * -0.05f;
        go.AddComponent<MeshCollider>();
        go.GetComponent<MeshCollider>().convex = true;
        go.GetComponent<MeshCollider>().isTrigger = true;
        go.AddComponent<DropEnemy>();
        FrontMesh = go;
        _meshList.Add(go);
        go.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        //Debug.Log(_vertices.Count);
        //Debug.Log(back_vertices.Count);
        //_vertices.Clear();
        //_vertices.AddRange(back_vertices);
        //奥のオブジェクト生成
        //GameObject go2 = _drawMesh.CreateMesh(back_vertices);
        //go2.GetComponent<MeshRenderer>().material = _material;
        //go2.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        //go2.AddComponent<MeshCollider>();
        //go2.AddComponent<DropEnemy>();
        //_meshList.Add(go2);

        ////手前と奥の間にメッシュオブジェクトを作成
        //GameObject meshob = new GameObject("MeshObject", typeof(MeshFilter), typeof(MeshRenderer));
        //mesh.RecalculateNormals();//法線の再設定
        //meshob.GetComponent<MeshRenderer>().material = _material;
        ////meshob.transform.position += meshob.transform.forward * -0.05f;
        //meshob.AddComponent<MeshCollider>();
        ////meshob.GetComponent<MeshCollider>().convex = true;
        //meshob.AddComponent<DropEnemy>();
        //MeshFilter filter = meshob.GetComponent<MeshFilter>();
        //filter.mesh = mesh;
        //_meshList.Add(meshob);

        //１つにまとめる
        GameObject AllMeshObject = new GameObject("AllMeshObject");
        go.transform.parent = AllMeshObject.transform;
        //go2.transform.parent = AllMeshObject.transform;
        //meshob.transform.parent = AllMeshObject.transform;
        AllMeshObject.AddComponent<Jin_DropMover>();
        AllMeshObject.GetComponent<Jin_DropMover>().SetPieceState_DROP();

        AllMeshObject.tag = ("DropBlock");
        _AllMeshObject = AllMeshObject;

        //メッシュを表示するため
        //go.gameObject.AddComponent<MeshInfo>();
        //go2.gameObject.AddComponent<MeshInfo>();
        //meshob.gameObject.AddComponent<MeshInfo>();
        //gogo.gameObject.AddComponent<MeshInfo>();

        //前作ったメッシュとつながってしまうためクリア
        _subMesh_vertices.Clear();
        IsChangeDirection = false;
        //_vertices.Clear();
        //back_dotList.Clear();
    }

    private GameObject CreateCircleMesh(List<Vector3> vertices)
    {
        Mesh mesh = new Mesh();
        Vector3 center = Vector3.zero;
        int[] meshTriangle = new int[(vertices.Count - 1) * 3];

        mesh.SetVertices(vertices);

        //頂点の平均をとって中心点を作る
        foreach (Vector3 point in vertices)
        {
            center += point;
        }
        center = center / vertices.Count;
        vertices.Add(center);

        int k = 0;
        for (int i = 0; i < (vertices.Count - 1) * 3; i += 3)
        {
            if (i == (vertices.Count - 2) * 3)
            {
                Debug.Log("最後");
                Debug.Log(k);
                meshTriangle[i] = k;
                meshTriangle[i + 1] = 0;
                meshTriangle[i + 2] = vertices.Count - 1;
                Debug.Log("最後2");
            }
            else
            {
                Debug.Log("hu");
                Debug.Log(i);
                meshTriangle[i] = k;
                meshTriangle[i + 1] = k + 1;
                meshTriangle[i + 2] = vertices.Count - 1;
            }
            k++;
        }

        mesh.SetTriangles(meshTriangle, 0);
        GameObject meshob = new GameObject("MeshObject", typeof(MeshFilter), typeof(MeshRenderer));
        meshob.GetComponent<MeshFilter>().mesh = mesh;

        return meshob;
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
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;
        //if (Physics.Raycast(ray, out hit, float.MaxValue))
        //{
        //    //var aim = hit.point - hit.collider.gameObject.transform.position;
        //    //var look = Quaternion.LookRotation(aim, Vector3.up);

        //    //奥の頂点
        //    Vector3 pos = hit.point;
        //    //transform.InverseTransformDirection(pos);
        //    pos.z += CutScaleZ;
        //    //transform.TransformDirection(pos);

        //    //最初の頂点
        //    if (_vertices.Count == 0)
        //    {
        //        //手前の頂点
        //        AddVertex(hit.point);
        //        //奥の頂点
        //        back_vertices.Add(pos);
        //        return;
        //    }

        //    if (_samplingVertices.Count == 0)
        //    {
        //        _samplingVertices.Add(hit.point);
        //        return;
        //    }

        //    float distance = (AveragePoint - hit.point).sqrMagnitude;
        //    if (distance >= _sqrThreshold)
        //    {
        //        AddVertex(hit.point);
        //        //奥の頂点リスト追加(点を打つのはまだ)
        //        back_vertices.Add(pos);

        //        LineCreate();

        //    }
        //    else
        //    {
        //        //仮の点
        //        _samplingVertices.Add(hit.point);
        //        //_samplingVertices.Add(pos);
        //    }
        //}
    }
    /// <summary>
    /// 囲むための判定用に頂点間にCube(線)を生成
    /// </summary>   
    public void LineCreate()
    {
        if (_vertices.Count > 1)
        {
            List<Vector3> myPoint = new List<Vector3>();
        
            myPoint.Add(_vertices[_vertices.Count - 2]);
            myPoint.Add(_vertices[_vertices.Count - 1]);

            GameObject obj = Instantiate(Line, transform.position, transform.rotation) as GameObject;
            obj.transform.position = (myPoint[0] + myPoint[1]) / 2;
            obj.transform.right = (myPoint[1] - myPoint[0]).normalized;
            obj.transform.localScale = new Vector3((myPoint[1] - myPoint[0]).magnitude, 0.005f, 0.005f);
            obj.tag = "LastLine";
            obj.layer = LayerMask.NameToLayer("Ignore Raycast");
            _lineList.Add(obj);

            

            if (_lineList.Count > 1)
            {
                //if(!_lineList[_lineList.Count - 2].GetComponent<HitPoint>().IsMeshCreate)
                //    obj.AddComponent<HitPoint>();
                Destroy(_lineList[_lineList.Count - 2].GetComponent<HitPoint>());
                _lineList[_lineList.Count - 2].tag = "Line";
            }

            obj.GetComponent<BoxCollider>().isTrigger = true;
            obj.AddComponent<Rigidbody>();
            obj.GetComponent<Rigidbody>().isKinematic = true;

            obj.AddComponent<HitPoint>();
        }

    }

    /// <summary>
    /// 頂点リストに追加
    /// </summary>
    /// <param name="point"></param>
    public void AddVertex(Vector3 point)
    {
        //CreateDot(point);
        _vertices.Add(point);
        //_localvertices.Add(MARUTA.transform.InverseTransformPoint(point));
        //_samplingVertices.Clear();
    }

    public void AddBackVertex(Vector3 backpoint)
    {
        //CreateDot(point);
        back_vertices.Add(backpoint);
        //_localvertices.Add(MARUTA.transform.InverseTransformPoint(point));
        //_samplingVertices.Clear();
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
            //dot.transform.rotation = Quaternion.AngleAxis(180, new Vector3(0, 0, 1));
            //Vector3 p = MainCamera.transform.position;
            //p.y = dot.transform.position.y;
            //dot.transform.LookAt(p);

        }

        dot.transform.localScale = Vector3.one * _dotSize;
        dot.transform.position = position;
        dot.GetComponent<MeshRenderer>().material = _dotMat;
        Destroy(dot.GetComponent<Collider>());
        if (!IsChangeDirection)
            _dotList.Add(dot);
        else
        {
            //Vector3 pos = dot.transform.localPosition;
            //pos.z += 1.5f;
            //transform.TransformDirection(pos);
            //dot.transform.position = pos;
            back_dotList.Add(dot);
        }

        return dot;
    }

    public void Clear()
    {
        for (int i = 0; i < _dotList.Count; i++)
        {
            Destroy(_dotList[i]);
        }
        _dotList.Clear();

        //for (int i = 0; i < _meshList.Count; i++)
        //{
        //    Destroy(_meshList[i]);
        //}
        //_meshList.Clear();

        for(int i=0; i<_lineList.Count; i++)
        {
            Destroy(_lineList[i]);
        }
        _lineList.Clear();

        _vertices.Clear();
        back_vertices.Clear();
    }
}

