using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footprints : MonoBehaviour
{

    [SerializeField] GameObject footpoints; // 子
    [SerializeField] GameObject FootPoint; // 親
    [SerializeField] GameObject pointDrawer;
    Jin_PointDrawer _pointDrawerSc;

    RaycastHit LogHit;

    public float PointRange = 1;

    private Vector3 OldRotation;
    private Vector3 OldNormal;

    Vector3 cameraForward;

    private List<GameObject> _dotList = new List<GameObject>();

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

    private void Start()
    {
        _pointDrawerSc = pointDrawer.GetComponent<Jin_PointDrawer>();
        cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
    }

    void Update()
    {
        if (Input.GetAxis("RT_Botton") == -1)
        {
            if (!isDrawing)
                CreateLineRoot();
            isDrawing = true;
        }
        if (Input.GetAxis("RT_Botton") == 0)
        {
            Clear();
        }
        if (isDrawing)
        {
            SetLinePoint(CheckPoint());
        }

        //Debug.Log(Input.GetAxis("RT_Botton"));
    }

    RaycastHit CheckPolygonToRayCast()
    {
        RaycastHit hit;
        if (Physics.Raycast(gameObject.transform.position, -transform.up, out hit, float.PositiveInfinity))
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

    Vector3 CheckPoint()
    {
        RaycastHit hit;
        hit = CheckPolygonToRayCast();
        return hit.point;
    }

    Vector3 CheckNormal()
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
                // その地点に頂点を打っていなければ
                if (!rendererPositions.Contains(pos))
                //if(CheckNormal() != OldNormal)
                {
                    VertNum++;
                    GameObject fp = Instantiate(footpoints, FootPoint.transform);
                    fp.transform.rotation = gameObject.transform.rotation;
                    fp.transform.position = pos;
                    _pointDrawerSc.AddVertex(pos);
                    rendererPositions.Add(pos);
                    pos += cameraForward * -0.1f;
                    Vector3 back = pos;
                    _pointDrawerSc.AddBackVertex(back);
                    _pointDrawerSc.LineCreate();
                    fp.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                    _dotList.Add(fp);
                    
                    OldNormal = CheckNormal();
                }
        }
    }

    void CreateLineRoot()
    {
        //新規生成
        OldNormal = CheckNormal();
        OldRotation = gameObject.transform.rotation.eulerAngles;
        GameObject fp = Instantiate(footpoints, FootPoint.transform);
        fp.transform.position = CheckPoint();
        fp.transform.rotation = gameObject.transform.rotation;

        _pointDrawerSc.AddVertex(fp.transform.position);
        Vector3 back = fp.transform.forward * -0.1f;
        _pointDrawerSc.AddBackVertex(fp.transform.position);
        fp.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        _dotList.Add(fp);
        rendererPositions.Add(CheckPoint());
        VertNum++;
    }

}
