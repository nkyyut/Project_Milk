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

    int dotnum;
    bool IsChangeDirection;
    public bool IsAllMeshCreate;
    RaycastHit LogHit;

    private Vector3 MeshForawd;

    [SerializeField]
    public float CutScaleZ = 0.1f;

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
    private Material _maskMaterial;

    [SerializeField]
    private float _threshold = 0.1f;

    [SerializeField] GameObject footpoints; // 子
    [SerializeField] GameObject FootPoint_Parent; // 親

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
    public List<List<GameObject>> All_lineList = new List<List<GameObject>>();
    private List<List<Vector3>> All_verticesList = new List<List<Vector3>>();

    private List<Vector3> _localvertices = new List<Vector3>();

    public List<GameObject> _allMeshObject = new List<GameObject>();

    public Footprints GetFootprints()
    {
        return _footPrints;
    }

    public GameObject GetFrontMesh()
    {
        return FrontMesh;
    }

    public Vector3 MeshObjectForwad()
    {
        return MeshForawd;
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

    private Vector3 CenterPoint(List<Vector3> vert)
    {
        Vector3 center = Vector3.zero;

        //頂点の平均をとって中心点を作る
        foreach (Vector3 point in vert)
        {
            center += point;
        }
        center = center / vert.Count;

        return center;
    }

    RaycastHit CheckPolygonToRayCast()
    {
        RaycastHit hit;
        if (Physics.Raycast(CenterPoint(_vertices), transform.up, out hit, float.PositiveInfinity))
        {
            if (hit.collider.transform.tag == "Coral")
            {
                LogHit = hit;
                return hit;
            }
            else return LogHit;
        }
        else
        {
            return LogHit;
        }
    }

    private void Awake()
    {
        MARUTA = GameObject.Find("edasan1");
        IsAllMeshCreate = false;
        _sqrThreshold = _threshold * _threshold;
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    Clear();
        //}
    }

    public void Initialize()
    {
        _vertices.Clear();
        back_vertices.Clear();

        _dotList.Clear();
        _lineList.Clear();
    }

    //public void MeshCuting()
    //{
    //    float max = 0;
    //    Vector3 CutPoint;

    //    for (int i = 0; i < _vertices.Count; i++)
    //    {
    //        float dis = _vertices.First().z - _vertices[i].z;

    //        if (Mathf.Abs(dis) > max)
    //        {
    //            max = Mathf.Abs(dis);
    //            CutPoint = _vertices[i];
    //        }
    //    }
    //    Debug.Log(max);
    //    if (max > 0.5f)
    //    {
    //        Debug.Log("斬る");
    //    }
    //}

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

        //真ん中に頂点追加
        GameObject fp = Instantiate(footpoints, FootPoint_Parent.transform);
        fp.name = "CenterPoint";
        fp.transform.position = CenterPoint(_vertices);
        Quaternion TargetRotation = Quaternion.FromToRotation(fp.gameObject.transform.forward, _footPrints.CheckNormal()) * fp.gameObject.transform.rotation;
        fp.transform.rotation = Quaternion.Slerp(fp.transform.rotation, TargetRotation, 10);
        MeshForawd = fp.transform.forward;

        GameObject[] _dots = new GameObject[_footPrints._dotList.Count];
        for (int i = 0; i < _footPrints._dotList.Count; i++)
        {
            _footPrints._dotList[i].transform.parent = fp.transform;
            _dots[i] = _footPrints._dotList[i];
        }

        for (int i = 0; i < _vertices.Count; i++)
        {
            _vertices[i] += MeshForawd * 0.1f;
        }

        Destroy(fp.gameObject);

        //手前のオブジェクト生成
        GameObject go = _drawMesh.CreateMesh(_vertices);
        Mesh gomesh = go.GetComponent<MeshFilter>().mesh;
        go.GetComponent<MeshFilter>().mesh.SetTriangles(gomesh.triangles.Reverse().ToArray(), 0);
        go.GetComponent<MeshRenderer>().material = _material;
        //go.transform.position += MeshForwad * -0.05f;
        go.AddComponent<MeshCollider>();
        go.GetComponent<MeshCollider>().convex = true;
        go.GetComponent<MeshCollider>().isTrigger = true;
        go.AddComponent<DropEnemy>();
        go.AddComponent<Subtractor>();
        go.GetComponent<Subtractor>().maskMaterial = _maskMaterial;
        FrontMesh = go;
        go.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        _meshList.Add(go);

        //奥のオブジェクト生成
        GameObject go2 = _drawMesh.CreateMesh(back_vertices);
        go2.GetComponent<MeshRenderer>().material = _material;
        go2.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        go2.AddComponent<MeshCollider>();
        //Mesh go2mesh = go2.GetComponent<MeshFilter>().mesh;
        //go2.GetComponent<MeshFilter>().mesh.SetTriangles(go2mesh.triangles.Reverse().ToArray(), 0);
        //go2.GetComponent<MeshCollider>().convex = true;
        go2.AddComponent<DropEnemy>();
        go2.AddComponent<Subtractor>();
        go2.GetComponent<Subtractor>().maskMaterial = _maskMaterial;
        _meshList.Add(go2);


        _subMesh_vertices.AddRange(_vertices);
        _subMesh_vertices.AddRange(back_vertices);

        //手前と奥の間にメッシュを作るための準備
        Mesh mesh = new Mesh();
        //頂点群の位置座標
        mesh.vertices = new Vector3[poriNum];
        mesh.SetVertices(_subMesh_vertices);

        //決めた順番をセット
        mesh.SetTriangles(OrderTriangles(), 0);

        //手前と奥の間にメッシュオブジェクトを作成
        GameObject meshob = new GameObject("MeshObject", typeof(MeshFilter), typeof(MeshRenderer));
        //mesh.RecalculateNormals();//法線の再設定
        meshob.GetComponent<MeshRenderer>().material = _material;
        meshob.AddComponent<DropEnemy>();
        MeshFilter filter = meshob.GetComponent<MeshFilter>();
        filter.mesh = mesh;
        meshob.AddComponent<MeshCollider>();
        //meshob.GetComponent<MeshCollider>().convex = true;
        meshob.AddComponent<Subtractor>();
        meshob.GetComponent<Subtractor>().maskMaterial = _maskMaterial;
        _meshList.Add(meshob);



        GameObject AllMeshObject = new GameObject("AllMeshObject", typeof(MeshFilter), typeof(MeshRenderer));
        go.transform.parent = AllMeshObject.transform;
        go2.transform.parent = AllMeshObject.transform;
        meshob.transform.parent = AllMeshObject.transform;
        //オブジェクトを一つに結合
        MeshFilter[] meshFilters = AllMeshObject.GetComponentsInChildren<MeshFilter>();
        CombineInstance[] _combine = new CombineInstance[meshFilters.Length];
        int h = 0;
        while (h < meshFilters.Length)
        {
            _combine[h].mesh = meshFilters[h].sharedMesh;
            _combine[h].transform = meshFilters[h].transform.localToWorldMatrix;
            meshFilters[h].gameObject.SetActive(false);
            h++;
        }
        AllMeshObject.transform.GetComponent<MeshFilter>().mesh = new Mesh();
        AllMeshObject.transform.GetComponent<MeshFilter>().mesh.CombineMeshes(_combine);
        AllMeshObject.transform.gameObject.SetActive(true);

        AllMeshObject.AddComponent<MeshCollider>();
        AllMeshObject.GetComponent<MeshRenderer>().material = _material;
        AllMeshObject.AddComponent<Subtractor>();
        AllMeshObject.GetComponent<Subtractor>().maskMaterial = _maskMaterial;

        //AllMeshObject.transform.position += MeshForwad * 0.05f;
        //AllMeshObject.AddComponent<Jin_DropMover>();
        //AllMeshObject.GetComponent<Jin_DropMover>().SetPieceState_DROP();
        AllMeshObject.tag = ("DropBlock");
        Mesh allmesh = AllMeshObject.GetComponent<MeshFilter>().mesh;
        if (0 < getArea(_dots))
            AllMeshObject.GetComponent<MeshFilter>().mesh.SetTriangles(allmesh.triangles.Reverse().ToArray(), 0);
        _allMeshObject.Add(AllMeshObject);
        AllMeshObject.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");


        GameObject DropMeshObject = new GameObject("DropMeshObject");
        GameObject front = Instantiate(go);
        GameObject back = Instantiate(go2);
        GameObject between = Instantiate(meshob);

        Destroy(back.GetComponent<MeshCollider>());
        Destroy(between.GetComponent<MeshCollider>());

        front.SetActive(true);
        back.SetActive(true);
        between.SetActive(true);

        front.transform.parent = DropMeshObject.transform;
        back.transform.parent = DropMeshObject.transform;
        between.transform.parent = DropMeshObject.transform;

        front.GetComponent<MeshRenderer>().material = _dotMat;
        back.GetComponent<MeshRenderer>().material = _dotMat;
        between.GetComponent<MeshRenderer>().material = _dotMat;

        DropMeshObject.AddComponent<Jin_DropMover>();
        DropMeshObject.GetComponent<Jin_DropMover>().SetPieceState_DROP();

        DropMeshObject.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

        IsAllMeshCreate = true;
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

    //多角形の右回り左回りを判定
    private float getArea(GameObject[] vec)
    {
        float S = 0;

        CenterPoint(_vertices);

        for (int i = 0; i < vec.Length; i++)
        {
            Vector3 a = vec[i].transform.localPosition;
            Vector3 b = (i < vec.Length - 1) ? vec[i + 1].transform.localPosition : vec[0].transform.localPosition;
            S += a.x * b.y - a.y * b.x;
        }
        return S / 2;
    }

    //メッシュオブジェクトが平面かどうか調べる処理
    //private Vector3[] FindDistanceVertex(Vector3[] vec)
    //{

    //}

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
                meshTriangle[i] = k;
                meshTriangle[i + 1] = 0;
                meshTriangle[i + 2] = vertices.Count - 1;
            }
            else
            {
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
                meshTriangles[i + 1] = j + 1;
                meshTriangles[i + 2] = j + _vertices.Count;
            }
            else
            {
                meshTriangles[i] = j;
                meshTriangles[i + 1] = 0;
                meshTriangles[i + 2] = j + _vertices.Count;
            }
            j++;
        }
        j = 0;
        for (int i = 3; i < PorigonAraay; i += _JinConst.ORDERTRIANGLES_NUM)
        {
            if (!(i == PorigonAraay - _JinConst.ORDERTRIANGLES_PORIGON))
            {
                meshTriangles[i] = j + 1;
                meshTriangles[i + 1] = j + _vertices.Count + 1;
                meshTriangles[i + 2] = j + _vertices.Count;
            }
            else
            {
                meshTriangles[i] = 0;
                meshTriangles[i + 1] = _vertices.Count;
                meshTriangles[i + 2] = j + _vertices.Count;
            }
            j++;
        }

        return meshTriangles;
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

            GameObject Lineobj = Instantiate(Line, transform.position, transform.rotation) as GameObject;
            Lineobj.transform.position = (myPoint[0] + myPoint[1]) / 2;
            if (myPoint[0].x < myPoint[1].x)
                MeshForawd = Lineobj.transform.right = (myPoint[1] - myPoint[0]).normalized;
            else
                MeshForawd = Lineobj.transform.right = (myPoint[0] - myPoint[1]).normalized;

            Lineobj.transform.localScale = new Vector3((myPoint[1] - myPoint[0]).magnitude, 0.005f, 0.005f);
            Lineobj.tag = "LastLine";
            Lineobj.layer = LayerMask.NameToLayer("Ignore Raycast");
            _lineList.Add(Lineobj);
            //All_lineList[AllList_Index].Add(Lineobj);
            if (_lineList.Count > 1)
            {
                Destroy(_lineList[_lineList.Count - 2].GetComponent<HitPoint>());
                _lineList[_lineList.Count - 2].tag = "Line";
                _lineList[_lineList.Count - 2].layer = LayerMask.NameToLayer("Coral");
            }
            if (_lineList.Count > 2)
            {
                _lineList[_lineList.Count - 3].layer = LayerMask.NameToLayer("Ignore Raycast");
            }

            Lineobj.GetComponent<BoxCollider>().isTrigger = true;
            Lineobj.AddComponent<Rigidbody>();
            Lineobj.GetComponent<Rigidbody>().isKinematic = true;

            Lineobj.AddComponent<HitPoint>();

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

        for (int i = 0; i < _lineList.Count; i++)
        {
            Destroy(_lineList[i]);
        }
        _lineList.Clear();

        _vertices.Clear();
        back_vertices.Clear();
        _subMesh_vertices.Clear();
    }

    public void LineClear()
    {
        foreach (GameObject line in _lineList)
            Destroy(line);

        _lineList.Clear();
    }
}

