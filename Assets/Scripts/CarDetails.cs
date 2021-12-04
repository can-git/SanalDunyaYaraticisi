using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

public class CarDetails : MonoBehaviour
{
    JsonVehicleDatas _jsonvehicle;
    JsonVehicleMotionDatas _jsonMotions;
    Renderer rend;

    private void Start()
    {
        rend = gameObject.GetComponentInChildren<Renderer>();
    }

    public JsonVehicleDatas getCarDetails()
    {
        _jsonvehicle = new JsonVehicleDatas();
        _jsonvehicle.ID = gameObject.GetComponent<ChangeColor>().getColorID();
        _jsonvehicle.TYPE = gameObject.GetComponent<ChangeColor>().getTypeID();
        _jsonvehicle.BboxDetails = getBboxDetails();
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
    public Rect get_bbox()
    {
        Vector3 cen = this.gameObject.GetComponentInChildren<Renderer>().bounds.center;
        Vector3 ext = this.gameObject.GetComponentInChildren<Renderer>().bounds.extents;
        Vector2[] extentPoints = new Vector2[8]
        {
         HandleUtility.WorldToGUIPoint(new Vector3(cen.x-ext.x, cen.y-ext.y, cen.z-ext.z)),
         HandleUtility.WorldToGUIPoint(new Vector3(cen.x+ext.x, cen.y-ext.y, cen.z-ext.z)),
         HandleUtility.WorldToGUIPoint(new Vector3(cen.x-ext.x, cen.y-ext.y, cen.z+ext.z)),
         HandleUtility.WorldToGUIPoint(new Vector3(cen.x+ext.x, cen.y-ext.y, cen.z+ext.z)),
         HandleUtility.WorldToGUIPoint(new Vector3(cen.x-ext.x, cen.y+ext.y, cen.z-ext.z)),
         HandleUtility.WorldToGUIPoint(new Vector3(cen.x+ext.x, cen.y+ext.y, cen.z-ext.z)),
         HandleUtility.WorldToGUIPoint(new Vector3(cen.x-ext.x, cen.y+ext.y, cen.z+ext.z)),
         HandleUtility.WorldToGUIPoint(new Vector3(cen.x+ext.x, cen.y+ext.y, cen.z+ext.z))
        };
        Vector2 min = extentPoints[0];
        Vector2 max = extentPoints[0];
        foreach (Vector2 v in extentPoints)
        {
            min = Vector2.Min(min, v);
            max = Vector2.Max(max, v);
        }
        return new Rect(min.x, min.y, max.x - min.x, max.y - min.y);
        
    }

    public JsonVehicleBbox getBboxDetails()
    {
        JsonVehicleBbox bbox = new JsonVehicleBbox();
        MeshFilter filter = GetComponentInChildren<MeshFilter>();
        Rect rect = get_bbox();
        //Debug.Log(rend.bounds.size);
        bbox.x = Mathf.RoundToInt(rect.x);
        bbox.y = Mathf.RoundToInt(rect.y);
        bbox.width = Mathf.RoundToInt(rect.width);
        bbox.height = Mathf.RoundToInt(rect.height);
        return bbox;
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
            triangleDatas.v0 = new Vector2(
                Camera.main.WorldToScreenPoint(transform.GetChild(0).TransformPoint(filter.sharedMesh.vertices[filter.sharedMesh.triangles[i + 0]])).x,
                Screen.height - (Camera.main.WorldToScreenPoint(transform.GetChild(0).TransformPoint(filter.sharedMesh.vertices[filter.sharedMesh.triangles[i + 0]])).y));
            //Camera.main.WorldToScreenPoint(transform.GetChild(0).TransformPoint(filter.sharedMesh.vertices[filter.sharedMesh.triangles[i + 0]])).z);

            triangleDatas.v1 = new Vector2(
                Camera.main.WorldToScreenPoint(transform.GetChild(0).TransformPoint(filter.sharedMesh.vertices[filter.sharedMesh.triangles[i + 1]])).x,
                Screen.height - (Camera.main.WorldToScreenPoint(transform.GetChild(0).TransformPoint(filter.sharedMesh.vertices[filter.sharedMesh.triangles[i + 1]])).y));
            //Camera.main.WorldToScreenPoint(transform.GetChild(0).TransformPoint(filter.sharedMesh.vertices[filter.sharedMesh.triangles[i + 1]])).z);

            triangleDatas.v2 = new Vector2(
                Camera.main.WorldToScreenPoint(transform.GetChild(0).TransformPoint(filter.sharedMesh.vertices[filter.sharedMesh.triangles[i + 2]])).x,
                Screen.height - (Camera.main.WorldToScreenPoint(transform.GetChild(0).TransformPoint(filter.sharedMesh.vertices[filter.sharedMesh.triangles[i + 2]])).y));
            //Camera.main.WorldToScreenPoint(transform.GetChild(0).TransformPoint(filter.sharedMesh.vertices[filter.sharedMesh.triangles[i + 2]])).z);

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
    public JsonVehicleBbox BboxDetails;
    public List<JsonVehicleMotionTDatas> VectorDetails;
}

[System.Serializable]
public class JsonVehicleBbox
{
    public int x;
    public int y;
    public int width;
    public int height;
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
    public Vector2 v0;
    public Vector2 v1;
    public Vector2 v2;
}
[System.Serializable]
public class JsonVehicleMotionTDatas
{
    public int TriangleID;
    public JsonVehicleTDatas TriangleDetails;
}
