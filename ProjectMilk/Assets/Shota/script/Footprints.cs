using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Footprints : MonoBehaviour
{
    [SerializeField] int DotMaxValue;
    [SerializeField] int LineMaxValue;
    [SerializeField] GameObject footpoints; // 子
    [SerializeField] GameObject FootPoint; // 親
    [SerializeField] GameObject pointDrawer;
    Jin_PointDrawer _pointDrawerSc;

    PlayerSE _playerSE;

    public Material blueMa;

    RaycastHit LogHit;

    public float PointRange = 1;

    private Vector3 OldRotation;
    private Vector3 OldNormal;

    public List<GameObject> _dotList = new List<GameObject>();
    private List<List<GameObject>> All_dotList = new List<List<GameObject>>();

    private bool IsButtonUp = false;

    // キリトリモードのフラグ
    protected bool isDrawing = false;

    //  頂点比較用リスト
    private List<Vector3> rendererPositions = new List<Vector3>();

    // 線引きに使う頂点数
    private int VertNum = 0;

    // キリトリモードへ移行するボタン（ここでは右クリック）
    private const int DRAW_BUTTON = 1;

    public void Clear()
    {
        isDrawing = false;
        // 各種変数を初期化
        rendererPositions.Clear();
        VertNum = 0;
        _pointDrawerSc.Clear();
        foreach (GameObject dot in _dotList)
            Destroy(dot);
        _dotList.Clear();
    }

    public void DotClear()
    {
        foreach (GameObject dot in _dotList)
            Destroy(dot);
        _dotList.Clear();
    }

    private void Start()
    {
        _pointDrawerSc = pointDrawer.GetComponent<Jin_PointDrawer>();
        _playerSE = this.gameObject.GetComponent<PlayerSE>();
        DotMaxValue = 40;
        LineMaxValue = 5;
    }

    void Update()
    {
        Debug.Log("tennsuu"+_dotList.Count);
        if (Input.GetAxis("RT_Botton") == -1)
        {
            Debug.Log("Innn");
            
            if (!isDrawing)
                CreateLineRoot();

                isDrawing = true;
           
            IsButtonUp = true;
        }
        if (Input.GetAxis("RT_Botton") == 0)
        {
            
            if (IsButtonUp)
            {                    
                _playerSE.SE_LineCreate();
                foreach (GameObject line in _pointDrawerSc._lineList)
                {
                    line.AddComponent<AgainLinePosition>();
                    line.tag = "OldLine";
                    line.GetComponent<MeshRenderer>().enabled = true;
                }
                List<GameObject> list = new List<GameObject>();
                List<GameObject> dot = new List<GameObject>();

                list.AddRange(_pointDrawerSc._lineList);
                dot.AddRange(_dotList);

                foreach (GameObject Triggerlist in _pointDrawerSc._lineList)
                    Triggerlist.GetComponent<BoxCollider>().isTrigger = false;

                _pointDrawerSc.All_lineList.Add(list);
                All_dotList.Add(dot);

                if (_pointDrawerSc.All_lineList.Count > LineMaxValue)
                {
                    for (int i = 0; i < _pointDrawerSc.All_lineList[0].Count; i++)
                    {
                        Destroy(_pointDrawerSc.All_lineList[0][i]);
                        Destroy(All_dotList[0][i]);
                    }
                    foreach (GameObject _dot in All_dotList[0])
                        Destroy(_dot);

                    _pointDrawerSc.All_lineList.Remove(_pointDrawerSc.All_lineList[0]);
                    All_dotList.Remove(All_dotList[0]);
                }

                isDrawing = false;
                VertNum = 0;
                //Clear();
                rendererPositions.Clear();
                _dotList.Clear();
                _pointDrawerSc._lineList.Clear();
                _pointDrawerSc._vertices.Clear();
                _pointDrawerSc.back_vertices.Clear();
                IsButtonUp = false;

            }

        }
        if (isDrawing&&_dotList.Count< DotMaxValue)
        {
            SetLinePoint(CheckPoint());
        }

        //Debug.Log(Input.GetAxis("RT_Botton"));
    }

    RaycastHit CheckPolygonToRayCast()
    {
        RaycastHit hit;
        Physics.Raycast(gameObject.transform.position, -transform.up, out hit, float.PositiveInfinity);
        //if (Physics.Raycast(gameObject.transform.position, -transform.up, out hit, float.PositiveInfinity))
        //{
        //    if (hit.collider.transform.tag == "Coral")
        //    {
        //        //LogHit = hit;
        //        return hit;
        //    }
        //    //else return LogHit;
        //}
        //else
        //{
        //    return LogHit;
        //}
        return hit;
    }

    Vector3 CheckPoint()
    {
        RaycastHit hit;
        hit = CheckPolygonToRayCast();
        return hit.point;
    }

    public Vector3 CheckNormal()
    {
        RaycastHit hit;
        hit = CheckPolygonToRayCast();
        return hit.normal;
    }

    void SetLinePoint(Vector3 pos)
    {
        if (isDrawing)
        {
            float x = Mathf.Abs(rendererPositions[VertNum - 1].x - pos.x);
            float y = Mathf.Abs(rendererPositions[VertNum - 1].y - pos.y);
            float z = Mathf.Abs(rendererPositions[VertNum - 1].z - pos.z);
            // 各軸一定量以上移動確認後、頂点設定
            if (x > PointRange || y > PointRange || z > PointRange)
            {
                // その地点に頂点を打っていなければ
                if (!rendererPositions.Contains(pos))
                //if(CheckNormal() != OldNormal)
                {
                    
                    VertNum++;
                    GameObject fp = Instantiate(footpoints, FootPoint.transform);
                    //fp.transform.rotation = gameObject.transform.rotation;
                    fp.transform.position = pos;
                    _pointDrawerSc.AddVertex(pos);
                    rendererPositions.Add(pos);
                    Quaternion TargetRotation = Quaternion.FromToRotation(fp.gameObject.transform.up, CheckNormal()) * fp.gameObject.transform.rotation;
                    fp.transform.rotation = Quaternion.Slerp(fp.transform.rotation, TargetRotation, 10);
                    GameObject fp2 = Instantiate(footpoints, FootPoint.transform);
                    Vector3 vec = CheckNormal();
                    fp2.transform.position = fp.transform.position + fp.transform.up * -0.1f;
                    fp2.GetComponent<MeshRenderer>().material = blueMa;
                    _pointDrawerSc.AddBackVertex(fp2.transform.position);
                    Destroy(fp2);
                    _pointDrawerSc.LineCreate();
                    fp.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                    _dotList.Add(fp);

                    OldNormal = CheckNormal();
                }
            }
        }
    }

    void CreateLineRoot()
    {
        //新規生成
        OldNormal = CheckNormal();
        OldRotation = this.gameObject.transform.rotation.eulerAngles;
        GameObject fp = Instantiate(footpoints, FootPoint.transform);
        fp.transform.position = CheckPoint();
        //fp.transform.rotation = this.gameObject.transform.rotation;
        Quaternion TargetRotation = Quaternion.FromToRotation(fp.gameObject.transform.up, CheckNormal()) * fp.gameObject.transform.rotation;
        fp.transform.rotation = Quaternion.Slerp(fp.transform.rotation, TargetRotation, 10);
        _pointDrawerSc.AddVertex(fp.transform.position);
        GameObject fp2 = Instantiate(footpoints, FootPoint.transform);
        Vector3 vec = CheckNormal();
        fp2.transform.position = fp.transform.position + fp.transform.up * -0.05f;
        fp2.GetComponent<MeshRenderer>().material = blueMa;
        _pointDrawerSc.AddBackVertex(fp2.transform.position);
        Destroy(fp2);
        fp.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        _dotList.Add(fp);
        rendererPositions.Add(CheckPoint());
        VertNum++;
    }

}
