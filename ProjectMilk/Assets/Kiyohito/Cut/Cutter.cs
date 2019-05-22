using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Meshを切断するクラス
public class Cutter : MonoBehaviour
{

    public CreateMesh _createMesh; // CreateMeshクラス
    public CreatePlane _createPlane; // CreatePlaneクラス

    public Vector3 _pos1; // planeとmeshの交点その1
    public Vector3 _pos2; // planeとmeshの交点その2


    void Start()
    {
        _createMesh.Create();
        _createPlane.Create();
        Cut();
    }

    public void Cut()
    {
        var plane = _createPlane._plane; // getter作ってあげる

        var group1PosList = new List<Vector3>(); // グループ1頂点リスト
        var group2PosList = new List<Vector3>(); // グループ2頂点リスト

        CheckPlaneSide(plane, group1PosList, group2PosList); // 1.グループ分け

        CalcCrossPoint(plane, group1PosList, group2PosList); // 2.planeとの交点を求める

        // 3.両方のグループともに交点を入れる
        group1PosList.Add(_pos1);
        group1PosList.Add(_pos2);
        group2PosList.Add(_pos1);
        group2PosList.Add(_pos2);

        // 4.2つのグループに分けたオブジェクトを作成する
        CreateCutObj(group1PosList);
        CreateCutObj(group2PosList);

        _createMesh.gameObject.SetActive(false); // 5.元となるオブジェクトを非表示にする
    }

    // planeのどちらにあるかを計算して振り分ける
    private void CheckPlaneSide(Plane plane, List<Vector3> group1, List<Vector3> group2)
    {
        var vertices = _createMesh._vertices;
        foreach (var v in vertices)
        {
            // どちらかのグループに振り分ける
            if (plane.GetSide(v))
            {
                group1.Add(v);
            }
            else
            {
                group2.Add(v);
            }
        }
    }

    // planeとmeshの交点を求める
    private void CalcCrossPoint(Plane plane, List<Vector3> group1, List<Vector3> group2)
    {
        float distance = 0;
        Vector3 basePos; // 計算する基準となる頂点
        Vector3 tmpPos1; // 基準点以外の頂点1
        Vector3 tmpPos2; // 基準点以外の頂点2

        // 少ない方からplaneに対して交差するpointを聞く
        if (group2.Count < group1.Count)
        {
            basePos = group2[0];
            tmpPos1 = group1[0];
            tmpPos2 = group1[1];
        }
        else
        {
            basePos = group1[0];
            tmpPos1 = group2[0];
            tmpPos2 = group2[1];
        }

        // 少ない所から多い片方の頂点に向かってrayを飛ばす。
        Ray ray1 = new Ray(basePos, (tmpPos1 - basePos).normalized);
        // planeと交差する距離を求める
        plane.Raycast(ray1, out distance);
        // ray1がその距離を進んだ位置を取得(ここが交点になる)
        _pos1 = ray1.GetPoint(distance);

        // 同じようにもう片方も計算
        Ray ray2 = new Ray(basePos, (tmpPos2 - basePos).normalized);
        plane.Raycast(ray2, out distance);
        _pos2 = ray2.GetPoint(distance);
    }

    // cutしたmeshを作る
    private void CreateCutObj(List<Vector3> pos)
    {
        var obj = new GameObject("cut obj", typeof(MeshFilter), typeof(MeshRenderer));

        var mesh = new Mesh();
        var triangles = new List<int>();

        var triIdx = 0;
        var triIdx0 = 0;
        var triIdx1 = 0;
        var triIdx2 = 0;
        var cross = Vector3.zero;
        var inner = 0.0f;

        for (int i = 0; i < pos.Count; i += 3)
        {
            triIdx0 = triIdx;
            triIdx1 = triIdx0 + 1;
            triIdx2 = triIdx0 + 2;

            cross = Vector3.Cross(pos[triIdx2] - pos[triIdx0], pos[triIdx1] - pos[triIdx0]);
            inner = Vector3.Dot(cross, Camera.main.transform.forward);

            // 逆向いている場合は反転させる
            if (inner < 0)
            {
                triIdx0 = triIdx2;
                triIdx2 = triIdx;
            }

            triangles.Add(triIdx0);
            triangles.Add(triIdx1);
            triangles.Add(triIdx2);
            triIdx++;
        }

        mesh.SetVertices(pos);
        mesh.triangles = triangles.ToArray();

        obj.GetComponent<MeshFilter>().mesh = mesh;
    }

}