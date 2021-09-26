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
        _jsonvehicle.TYPE = gameObject.GetComponent<ChangeColor>().getTypeID();
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
        MeshFilter filter = GetComponentInChildren<MeshFilter>();

        List<JsonVehicleMotionTDatas> motionTList = new List<JsonVehicleMotionTDatas>();
        int num = 0;

        for (int i = 0; i < filter.sharedMesh.triangles.Length; i += 3)
        {
            JsonVehicleMotionTDatas motionTDatas = new JsonVehicleMotionTDatas();
            JsonVehicleTDatas triangleDatas = new JsonVehicleTDatas();
            //triangleDatas.v0 = filter.sharedMesh.vertices[filter.sharedMesh.triangles[i + 0]];
            //triangleDatas.v1 = filter.sharedMesh.vertices[filter.sharedMesh.triangles[i + 1]];
            //triangleDatas.v2 = filter.sharedMesh.vertices[filter.sharedMesh.triangles[i + 2]];

            //Debug.DrawLine(transform.GetChild(0).TransformPoint(triangleDatas.v0), transform.GetChild(0).TransformPoint(triangleDatas.v1), Color.red, 0);

            motionTDatas.TriangleID = num;
            num++;
            triangleDatas.v0 = new Vector3(Camera.main.WorldToScreenPoint(transform.GetChild(0).TransformPoint(filter.sharedMesh.vertices[filter.sharedMesh.triangles[i + 0]])).x, Screen.height - (Camera.main.WorldToScreenPoint(transform.GetChild(0).TransformPoint(filter.sharedMesh.vertices[filter.sharedMesh.triangles[i + 0]])).y), Camera.main.WorldToScreenPoint(transform.GetChild(0).TransformPoint(filter.sharedMesh.vertices[filter.sharedMesh.triangles[i + 0]])).z);
            triangleDatas.v1 = new Vector3(Camera.main.WorldToScreenPoint(transform.GetChild(0).TransformPoint(filter.sharedMesh.vertices[filter.sharedMesh.triangles[i + 1]])).x, Screen.height - (Camera.main.WorldToScreenPoint(transform.GetChild(0).TransformPoint(filter.sharedMesh.vertices[filter.sharedMesh.triangles[i + 1]])).y), Camera.main.WorldToScreenPoint(transform.GetChild(0).TransformPoint(filter.sharedMesh.vertices[filter.sharedMesh.triangles[i + 1]])).z);
            triangleDatas.v2 = new Vector3(Camera.main.WorldToScreenPoint(transform.GetChild(0).TransformPoint(filter.sharedMesh.vertices[filter.sharedMesh.triangles[i + 2]])).x, Screen.height - (Camera.main.WorldToScreenPoint(transform.GetChild(0).TransformPoint(filter.sharedMesh.vertices[filter.sharedMesh.triangles[i + 2]])).y), Camera.main.WorldToScreenPoint(transform.GetChild(0).TransformPoint(filter.sharedMesh.vertices[filter.sharedMesh.triangles[i + 2]])).z);
            //Debug.Log(triangleDatas.v0);
            //Debug.Log(new Vector3(Camera.main.WorldToScreenPoint(transform.GetChild(0).transform.position + triangleDatas.v0).x, Screen.height - (Camera.main.WorldToScreenPoint(transform.GetChild(0).transform.position + triangleDatas.v0).y), Camera.main.WorldToScreenPoint(transform.GetChild(0).transform.position + triangleDatas.v0).z));

            motionTDatas.TriangleDetails = triangleDatas;
            motionTList.Add(motionTDatas);
        }
        return motionTList;
    }
}

[System.Serializable]
public class JsonVehicleDatas
{
    public int ID;
    public int TYPE;
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
