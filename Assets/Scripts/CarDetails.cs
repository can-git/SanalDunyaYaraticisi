using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CarDetails : MonoBehaviour
{
    JsonVehicleDatas _jsonvehicle;
    JsonVehicleMotionDatas _jsonMotions;

    public JsonVehicleDatas getCarDetails()
    {
        _jsonvehicle = new JsonVehicleDatas();

        _jsonvehicle.ID = gameObject.GetComponent<ChangeColor>().getColorID();
        _jsonvehicle.VectorDetails = getMotionTDatas();

        return _jsonvehicle;
    }
    public List<JsonVehicleMotionDatas> getMotionDatas()
    {
        List<JsonVehicleMotionDatas> motionVectorList = new List<JsonVehicleMotionDatas>();
        var velocityList = GetComponentsInChildren<Velocity>();
        for (int i = 0; i < velocityList.Length; i++)
        {
            if (velocityList[i].IsInView())
            {
                _jsonMotions = new JsonVehicleMotionDatas();
                _jsonMotions.Vector = velocityList[i].getVelocity();
                _jsonMotions.Position = new Vector2(Camera.main.WorldToScreenPoint(velocityList[i].transform.position).x, (Screen.height - Camera.main.WorldToScreenPoint(velocityList[i].transform.position).y));
                motionVectorList.Add(_jsonMotions);
            }
        }
        return motionVectorList;
    }
    public List<JsonVehicleMotionTDatas> getMotionTDatas()
    {
        //MeshFilter[] filters = GetComponentsInChildren<MeshFilter>();
        MeshFilter filters = GetComponentInChildren<MeshFilter>();
        List<JsonVehicleMotionTDatas> motionTList = new List<JsonVehicleMotionTDatas>();
        int num = 0;
        Edge[] edges = GetMeshEdges(filters.sharedMesh);
        print(transform.GetChild(0).position + edges[0].v1);
        for (int i = 0; i < filters.sharedMesh.triangles.Length; i += 3)
        {
            JsonVehicleMotionTDatas motionTDatas = new JsonVehicleMotionTDatas();
            JsonVehicleTDatas triangleDatas = new JsonVehicleTDatas();
            triangleDatas.v0 = filters.sharedMesh.vertices[filters.sharedMesh.triangles[i + 0]];
            triangleDatas.v1 = filters.sharedMesh.vertices[filters.sharedMesh.triangles[i + 1]];
            triangleDatas.v2 = filters.sharedMesh.vertices[filters.sharedMesh.triangles[i + 2]];
            num++;
            motionTDatas.TriangleID = num;
            triangleDatas.v0 = new Vector3(Camera.main.WorldToScreenPoint(transform.GetChild(0).TransformPoint(filters.sharedMesh.vertices[filters.sharedMesh.triangles[i + 0]])).x, Screen.height - (Camera.main.WorldToScreenPoint(transform.GetChild(0).TransformPoint(filters.sharedMesh.vertices[filters.sharedMesh.triangles[i + 0]])).y), Camera.main.WorldToScreenPoint(transform.GetChild(0).TransformPoint(filters.sharedMesh.vertices[filters.sharedMesh.triangles[i + 0]])).z);
            triangleDatas.v1 = new Vector3(Camera.main.WorldToScreenPoint(transform.GetChild(0).TransformPoint(filters.sharedMesh.vertices[filters.sharedMesh.triangles[i + 1]])).x, Screen.height - (Camera.main.WorldToScreenPoint(transform.GetChild(0).TransformPoint(filters.sharedMesh.vertices[filters.sharedMesh.triangles[i + 1]])).y), Camera.main.WorldToScreenPoint(transform.GetChild(0).TransformPoint(filters.sharedMesh.vertices[filters.sharedMesh.triangles[i + 1]])).z);
            triangleDatas.v2 = new Vector3(Camera.main.WorldToScreenPoint(transform.GetChild(0).TransformPoint(filters.sharedMesh.vertices[filters.sharedMesh.triangles[i + 2]])).x, Screen.height - (Camera.main.WorldToScreenPoint(transform.GetChild(0).TransformPoint(filters.sharedMesh.vertices[filters.sharedMesh.triangles[i + 2]])).y), Camera.main.WorldToScreenPoint(transform.GetChild(0).TransformPoint(filters.sharedMesh.vertices[filters.sharedMesh.triangles[i + 2]])).z);
            motionTDatas.TriangleDetails = triangleDatas;
            motionTList.Add(motionTDatas);
        }
        //for (int i = 0; i < filters.sharedMesh.triangles.Length; i++)
        //{
        //    Debug.DrawLine(transform.GetChild(0).TransformPoint(filters.sharedMesh.vertices[filters.sharedMesh.triangles[i + 0]]), transform.GetChild(0).TransformPoint(filters.sharedMesh.vertices[filters.sharedMesh.triangles[i + 1]]), Color.red, 0);
        //}
        return motionTList;
    }
    public struct Edge
    {
        public Vector3 v1;
        public Vector3 v2;

        public Edge(Vector3 v1, Vector3 v2)
        {
            if (v1.x < v2.x || (v1.x == v2.x && (v1.y < v2.y || (v1.y == v2.y && v1.z <= v2.z))))
            {
                this.v1 = v1;
                this.v2 = v2;
            }
            else
            {
                this.v1 = v2;
                this.v2 = v1;
            }
        }
    }
    private Edge[] GetMeshEdges(Mesh mesh)
    {
        HashSet<Edge> edges = new HashSet<Edge>();

        for (int i = 0; i < mesh.triangles.Length; i += 3)
        {
            var v1 = mesh.vertices[mesh.triangles[i]];
            var v2 = mesh.vertices[mesh.triangles[i + 1]];
            var v3 = mesh.vertices[mesh.triangles[i + 2]];
            edges.Add(new Edge(v1, v2));
            edges.Add(new Edge(v1, v3));
            edges.Add(new Edge(v2, v3));
        }

        return edges.ToArray();
    }
}

[System.Serializable]
public class JsonVehicleDatas
{
    public int ID;
    public List<JsonVehicleMotionTDatas> VectorDetails;
}

[System.Serializable]
public class JsonVehicleMotionDatas
{
    public Vector2 Position;
    public Vector3 Vector;
}
[System.Serializable]
public class JsonVehicleTDatas
{
    public Vector3 v0;
    public Vector3 v1;
    public Vector3 v2;
}
[System.Serializable]
public class JsonVehicleMotionTDatas
{
    public int TriangleID;
    public JsonVehicleTDatas TriangleDetails;
}
