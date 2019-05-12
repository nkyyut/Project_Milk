//玉那覇臣  4/11
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Jin_Const;
using System;

public class PointDrawer : MonoBehaviour
{

    //切断面に適用するマテリアル
    public Material capMaterial;

    [SerializeField]
    GameObject Line;

    //切断するオブジェクト
    GameObject MARUTA;

    GameObject FrontMesh;

    int dotnum;
    bool IsChangeDirection;
    public bool IsMeshCreate = false;

    [SerializeField]
    private GameObject _planeMesh;

    private int AllList_Index;

    [SerializeField]
    public float CutScaleZ = 0.2f;

    [SerializeField]
    private DrawMesh _drawMesh;
    [SerializeField]
    private SourcePM _sourcePM;

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
            _target_vertices.AddRange(filter.mesh.vertices);

            int _target_verticesNum = _target_vertices.Count;

            int[] triangles = filter.mesh.triangles;

            _target_vertices.AddRange(_localvertices);
            filter.mesh.SetVertices(_target_vertices);

            int _target_trianglesNum = filter.mesh.triangles.Count();


            //点から一番近い
            int[] MinTriangle = new int[_localvertices.Count];
            float min = 0;
            for (int i = 0; i < _localvertices.Count; i++)
            {
                min = 99;
                for (int k = 0; k < _target_verticesNum - 1; k++)
                {
                    //float dis = (_localvertices[i] - _target_vertices[k]).sqrMagnitude;
                    float dis = Vector3.Distance(_localvertices[i], _target_vertices[k]);

                    if (dis < min)
                    {
                        min = dis;
                        MinTriangle[i] = k;
                    }
                }
            }
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

            int j = 0;

            Array.Resize(ref triangles, triangles.Length + (_localvertices.Count * 3));
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

            filter.mesh.SetTriangles(triangles, 0);

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
        go.transform.position += go.transform.forward * -0.08f;
        //go.transform.position += go.transform.forward * -0.08f;
        _meshList.Add(go);
        go.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

        List<Vector3> _newVec = new List<Vector3>();
        List<Vector3> _newVector0 = new List<Vector3>();
        List<Vector3> _newVector1 = new List<Vector3>();
        List<Vector3> _newVector2 = new List<Vector3>();
        List<Vector3> _newVector3 = new List<Vector3>();
        List<Vector3> _newVector4 = new List<Vector3>();
        List<Vector3> _newVector5 = new List<Vector3>();
        List<Vector3> _newVector6 = new List<Vector3>();
        List<Vector3> _newVector7 = new List<Vector3>();
        List<Vector3> _newVector8 = new List<Vector3>();
        List<Vector3> _newVector9 = new List<Vector3>();

        List<List<Vector3>> AllVectorList = new List<List<Vector3>>();

        AllVectorList.Add(_newVector0);
        AllVectorList.Add(_newVector1);
        AllVectorList.Add(_newVector2);
        AllVectorList.Add(_newVector3);
        AllVectorList.Add(_newVector4);
        AllVectorList.Add(_newVector5);
        AllVectorList.Add(_newVector6);
        AllVectorList.Add(_newVector7);
        AllVectorList.Add(_newVector8);
        AllVectorList.Add(_newVector9);

        for (int k = 0; k < _vertices.Count / 2; k++)
        {
            for (float j = 0.05f; j < 1.0f; j += 0.1f)
            {
                j = j * 100;
                j = Mathf.Floor(j) / 100;

                int Iswitch = (int)(j * 10);

                switch (Iswitch)
                {
                    case 0:
                        AllVectorList[Iswitch].Add(Vector3.Lerp(_vertices[k], _vertices[_vertices.Count / 2 + k], j));
                        break;
                    case 1:
                        AllVectorList[Iswitch].Add(Vector3.Lerp(_vertices[k], _vertices[_vertices.Count / 2 + k], j));
                        break;
                    case 2:
                        AllVectorList[Iswitch].Add(Vector3.Lerp(_vertices[k], _vertices[_vertices.Count / 2 + k], j));
                        break;
                    case 3:
                        AllVectorList[Iswitch].Add(Vector3.Lerp(_vertices[k], _vertices[_vertices.Count / 2 + k], j));
                        break;
                    case 4:
                        AllVectorList[Iswitch].Add(Vector3.Lerp(_vertices[k], _vertices[_vertices.Count / 2 + k], j));
                        break;
                    case 5:
                        AllVectorList[Iswitch].Add(Vector3.Lerp(_vertices[k], _vertices[_vertices.Count / 2 + k], j));
                        break;
                    case 6:
                        AllVectorList[Iswitch].Add(Vector3.Lerp(_vertices[k], _vertices[_vertices.Count / 2 + k], j));
                        break;
                    case 7:
                        AllVectorList[Iswitch].Add(Vector3.Lerp(_vertices[k], _vertices[_vertices.Count / 2 + k], j));
                        break;
                    case 8:
                        AllVectorList[Iswitch].Add(Vector3.Lerp(_vertices[k], _vertices[_vertices.Count / 2 + k], j));
                        break;
                    case 9:
                        AllVectorList[Iswitch].Add(Vector3.Lerp(_vertices[k], _vertices[_vertices.Count / 2 + k], j));
                        break;
                }

                //if (j == 0.05f)
                //    _newVector0.Add(Vector3.Lerp(_vertices[k], _vertices[_vertices.Count / 2 + k], j));

                //if (j == 0.15f)
                //    _newVector1.Add(Vector3.Lerp(_vertices[k], _vertices[_vertices.Count / 2 + k], j));

                //if (j == 0.25f)
                //    _newVector2.Add(Vector3.Lerp(_vertices[k], _vertices[_vertices.Count / 2 + k], j));

                //if (j == 0.35f)
                //    _newVector3.Add(Vector3.Lerp(_vertices[k], _vertices[_vertices.Count / 2 + k], j));

                //if (j == 0.45f)
                //    _newVector4.Add(Vector3.Lerp(_vertices[k], _vertices[_vertices.Count / 2 + k], j));

                //if (j == 0.55f)
                //    _newVector5.Add(Vector3.Lerp(_vertices[k], _vertices[_vertices.Count / 2 + k], j));

                //if (j == 0.65f)
                //    _newVector6.Add(Vector3.Lerp(_vertices[k], _vertices[_vertices.Count / 2 + k], j));

                //if (j == 0.75f)
                //    _newVector7.Add(Vector3.Lerp(_vertices[k], _vertices[_vertices.Count / 2 + k], j));

                //if (j == 0.85f)
                //    _newVector8.Add(Vector3.Lerp(_vertices[k], _vertices[_vertices.Count / 2 + k], j));

                //if (j == 0.95f)
                //    _newVector9.Add(Vector3.Lerp(_vertices[k], _vertices[_vertices.Count / 2 + k], j));

            }
        }
        //_AllVec.AddRange(_vertices);
        //_AllVec.AddRange(_newVector);
        List<Vector3> tmpVertices0 = new List<Vector3>();
        List<Vector3> tmpVertices1 = new List<Vector3>();
        List<Vector3> tmpVertices2 = new List<Vector3>();
        List<Vector3> tmpVertices3 = new List<Vector3>();
        List<Vector3> tmpVertices4 = new List<Vector3>();
        List<List<Vector3>> AlltmpList = new List<List<Vector3>>();
        

        AlltmpList.Add(tmpVertices0);
        AlltmpList.Add(tmpVertices1);
        AlltmpList.Add(tmpVertices2);
        AlltmpList.Add(tmpVertices3);
        AlltmpList.Add(tmpVertices4);

        AlltmpList[0].AddRange(AllVectorList[0]);
        AlltmpList[0].AddRange(AllVectorList[9]);

        AlltmpList[1].AddRange(AllVectorList[1]);
        AlltmpList[1].AddRange(AllVectorList[8]);

        AlltmpList[2].AddRange(AllVectorList[2]);
        AlltmpList[2].AddRange(AllVectorList[7]);

        AlltmpList[3].AddRange(AllVectorList[3]);
        AlltmpList[3].AddRange(AllVectorList[6]);

        AlltmpList[4].AddRange(AllVectorList[4]);
        AlltmpList[4].AddRange(AllVectorList[5]);

        for (int k = 0; k < AlltmpList.Count; k++)
        {
            List<Vector3> newVertices = new List<Vector3>();

            for (int i = 0; i < AlltmpList[k].Count; i++)
            {

                Vector3 pos = AlltmpList[k][i];
                pos.z -= 100;
                AlltmpList[k][i] = pos;

                //Debug.Log(tmpVertices[i]);

                RaycastHit hit;
                if (Physics.Raycast(AlltmpList[k][i], Vector3.forward, out hit))
                {
                    Vector3 newVert = AlltmpList[k][i];
                    newVert.z = hit.point.z;
                    newVert.z -= 0.08f;
                    newVertices.Add(newVert);

                    //GameObject dot = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    //dot.transform.localScale = Vector3.one * 0.05f;
                    //dot.transform.position = newVertices.Last();
                    //dot.transform.position += dot.transform.forward * -0.1f;
                    //dot.layer = LayerMask.NameToLayer("Ignore Raycast");
                    //Debug.Log(newVertices.Last());
                }
                else
                {
                    Vector3 pos2 = AlltmpList[k][i];
                    pos2.z = 0;
                    newVertices[i] = pos2;
                }

            }
            GameObject gogo = _drawMesh.CreateMesh(newVertices);
            gogo.GetComponent<MeshRenderer>().material = _material;
        }

        //GameObject gogo = CreateCircleMesh(newVertices);
        //GameObject gogo = _drawMesh.CreateMesh(newVertices);
        //gogo.GetComponent<MeshRenderer>().material = _material;
        //_drawMesh.CreateMesh(AlltmpList[0]);

        //for (int i = 0; i < tmpVertices1.Count; i++)
        //{
        //    Vector3 pos = tmpVertices1[i];
        //    pos.z -= 100;
        //    tmpVertices1[i] = pos;

        //    //Debug.Log(tmpVertices[i]);

        //    RaycastHit hit;
        //    if (Physics.Raycast(tmpVertices1[i], Vector3.forward, out hit))
        //    {
        //        Vector3 newVert = tmpVertices1[i];
        //        newVert.z = hit.point.z;
        //        newVertices.Add(newVert);

        //        GameObject dot = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //        dot.transform.localScale = Vector3.one * 0.05f;
        //        dot.transform.position = newVertices.Last();
        //        dot.layer = LayerMask.NameToLayer("Ignore Raycast");
        //        Debug.Log(newVertices.Last());
        //    }
        //    else
        //    {
        //        Vector3 pos2 = tmpVertices1[i];
        //        pos2.z = 0;
        //        newVertices[i] = pos2;
        //    }
        //    i++;
        //}


        //GameObject gogo = CreateCircleMesh(newVertices);
        //GameObject gogo = _drawMesh.CreateMesh(newVertices);
        //gogo.GetComponent<MeshRenderer>().material = _material;

        //List<Vector3> newVertices2 = new List<Vector3>();
        //newVertices2.AddRange(_newVector2);
        //for (int i = 0; i < newVertices2.Count; i++)
        //{
        //    Vector3 pos = newVertices2[i];
        //    pos.z -= 100;
        //    newVertices2[i] = pos;

        //    RaycastHit hit;
        //    if (Physics.Raycast(newVertices2[i], Vector3.forward, out hit))
        //    {
        //        Vector3 newVert = newVertices2[i];
        //        newVert.z = hit.point.z;
        //        newVertices2.Add(newVert);
        //    }
        //    else
        //    {
        //        Vector3 pos2 = newVertices2[i];
        //        pos2.z = 0;
        //        newVertices2[i] = pos2;
        //    }
        //    i++;
        //}

        //GameObject gogo2 = CreateCircleMesh(newVertices2);
        //gogo2.GetComponent<MeshRenderer>().material = _material;

        //List<Vector3> newVertices3 = new List<Vector3>();
        //newVertices3.AddRange(_newVector3);
        //for (int i = 0; i < newVertices3.Count; i++)
        //{
        //    Vector3 pos = newVertices3[i];
        //    pos.z -= 100;
        //    newVertices3[i] = pos;

        //    RaycastHit hit;
        //    if (Physics.Raycast(newVertices3[i], Vector3.forward, out hit))
        //    {
        //        Vector3 newVert = newVertices3[i];
        //        newVert.z = hit.point.z;
        //        newVertices3.Add(newVert);
        //    }
        //    else
        //    {
        //        Vector3 pos2 = newVertices3[i];
        //        pos2.z = 0;
        //        newVertices3[i] = pos2;
        //    }
        //    i++;
        //}

        //GameObject gogo3 = CreateCircleMesh(newVertices3);
        //gogo3.GetComponent<MeshRenderer>().material = _material;


        //奥のオブジェクト生成
        //GameObject go2 = _drawMesh.CreateMesh(back_vertices);
        //go2.GetComponent<MeshRenderer>().material = _material;
        //go2.transform.position += go2.transform.forward * -0.001f;
        //go2.transform.position += go2.transform.forward * -0.1f;
        //_meshList.Add(go2);

        //手前と奥の間にメッシュオブジェクトを作成
        //GameObject meshob = new GameObject("MeshObject", typeof(MeshFilter), typeof(MeshRenderer));
        //mesh.RecalculateNormals();//法線の再設定
        //meshob.GetComponent<MeshRenderer>().material = _material;
        //meshob.transform.position += go2.transform.forward * -0.1f;
        //MeshFilter filter = meshob.GetComponent<MeshFilter>();
        //filter.mesh = mesh;
        //_meshList.Add(meshob);

        //GameObject TextureObj = _drawMesh.CreateMesh(_vertices);
        //TextureObj.GetComponent<MeshRenderer>().material = _material;
        //go.transform.position += go.transform.forward * -0.001f;

        //メッシュを表示するため
        //go.gameObject.AddComponent<MeshInfo>();
        //go2.gameObject.AddComponent<MeshInfo>();
        //meshob.gameObject.AddComponent<MeshInfo>();
        //gogo.gameObject.AddComponent<MeshInfo>();

        //_planeMesh.SetActive(true);

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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, float.MaxValue))
        {
            //var aim = hit.point - hit.collider.gameObject.transform.position;
            //var look = Quaternion.LookRotation(aim, Vector3.up);

            //奥の頂点
            Vector3 pos = hit.point;
            //transform.InverseTransformDirection(pos);
            pos.z += CutScaleZ;
            //transform.TransformDirection(pos);

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
        //_localvertices.Add(MARUTA.transform.InverseTransformPoint(point));
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

        for (int i = 0; i < _meshList.Count; i++)
        {
            Destroy(_meshList[i]);
        }
        _meshList.Clear();
    }
}

