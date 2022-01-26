using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

public class CarDetails : MonoBehaviour
{
    JsonVehicleDatas _jsonvehicle;
    JsonVehicleMotionDatas _jsonMotions;
    Rect r;


    public JsonVehicleDatas getCarDetails()
    {
        _jsonvehicle = new JsonVehicleDatas();
        _jsonvehicle.ID = gameObject.GetComponent<ChangeColor>().getColorID();
        _jsonvehicle.TYPE = gameObject.GetComponent<ChangeColor>().getTypeID();
        //_jsonvehicle.BboxDetails = getBboxDetails();
        _jsonvehicle.VectorDetails = getMotionTDatas();

        return _jsonvehicle;
    }

    public bool isInCamera()
    {
        MeshFilter filter = GetComponentInChildren<MeshFilter>();
        bool durum = false;
        for (int i = 0; i < filter.sharedMesh.triangles.Length; i += 1)
        {
            Vector2 check;

            check = World2ScreenPoint(filter.transform.TransformPoint(filter.sharedMesh.vertices[filter.sharedMesh.triangles[i]]));
            if (check.x > 1920 || check.x < 0 || check.y < 0 || check.y > 1080)
            {
                durum = false;
            }
            else
            {
                durum = true;
                break;
            }
        }
        return durum;
    }
    public bool ObjectInCamera()
    {
        Vector3[] bboxs3 = GetComponentInChildren<MeshFilter>().mesh.vertices;
        List<Vector2> bboxs = new List<Vector2>();
        foreach (Vector3 item in bboxs3)
        {
            bboxs.Add(WorldToGUIPoint(transform.TransformPoint(item)));
        }
        float x1 = float.MaxValue, y1 = float.MaxValue, x2 = 0.0f, y2 = 0.0f;

        foreach (Vector2 temp in bboxs)
        {
            if (temp.x >= 0 && temp.x <= (float)Screen.width && temp.y >= 0 && temp.y <= (float)Screen.height)
            {
                if (temp.x < x1)
                    x1 = temp.x;
                if (temp.x > x2)
                    x2 = temp.x;
                if (temp.y < y1)
                    y1 = temp.y;
                if (temp.y > y2)
                    y2 = temp.y;
            }
        }
        float x = x1;
        float y = y1;
        float width = x2 - x1;
        float height = y2 - y1;

        if (x >= 0 && x <= (float)Screen.width && y >= 0 && y <= (float)Screen.height && width >= 20 && height >= 20)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public Rect GUI2dRectWithObject()
    {
        Vector3[] bboxs3 = GetComponentInChildren<MeshFilter>().mesh.vertices;
        List<Vector2> bboxs = new List<Vector2>();
        foreach (Vector3 item in bboxs3)
        {
            bboxs.Add(WorldToGUIPoint(transform.TransformPoint(item)));
        }
        float x1 = float.MaxValue, y1 = float.MaxValue, x2 = 0.0f, y2 = 0.0f;

        foreach (Vector2 temp in bboxs)
        {
            if (temp.x >= 0 && temp.x <= (float)Screen.width && temp.y >= 0 && temp.y <= (float)Screen.height)
            {
                if (temp.x < x1)
                    x1 = temp.x;
                if (temp.x > x2)
                    x2 = temp.x;
                if (temp.y < y1)
                    y1 = temp.y;
                if (temp.y > y2)
                    y2 = temp.y;
            }
        }
        float x = x1;
        float y = y1;
        float width = x2 - x1;
        float height = y2 - y1;

        if (x >= 0 && x <= (float)Screen.width && y >= 0 && y <= (float)Screen.height && width >= 20 && height >= 20)
        {
            return new Rect(x, y, width, height);
        }
        else
        {
            return new Rect(-1, -1, -1, -1);
        }
    }
    public Vector2 WorldToGUIPoint(Vector3 world)
    {
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(world);
        screenPoint.y = (float)Screen.height - screenPoint.y;
        return screenPoint;
    }

    public JsonVehicleBbox getBboxDetails()
    {

        JsonVehicleBbox bbox = new JsonVehicleBbox();
        Rect r = GUI2dRectWithObject();
        bbox.x = Mathf.RoundToInt(r.x);
        bbox.y = Mathf.RoundToInt(r.y);
        bbox.width = Mathf.RoundToInt(r.width);
        bbox.height = Mathf.RoundToInt(r.height);

        return bbox;
    }

    public List<JsonVehicleMotionTDatas> getMotionTDatas()
    {
        MeshFilter filter = GetComponentInChildren<MeshFilter>();

        List<JsonVehicleMotionTDatas> motionTList = new List<JsonVehicleMotionTDatas>();
        int num = 0;

        if (isInCamera())
        {
            for (int i = 0; i < filter.sharedMesh.triangles.Length; i += 3)
            {
                JsonVehicleMotionTDatas motionTDatas = new JsonVehicleMotionTDatas();
                JsonVehicleTDatas triangleDatas = new JsonVehicleTDatas();

                motionTDatas.TriangleID = num;
                num++;

                triangleDatas.v0 = World2ScreenPoint(filter.transform.TransformPoint(filter.sharedMesh.vertices[filter.sharedMesh.triangles[i + 0]]));

                triangleDatas.v1 = World2ScreenPoint(filter.transform.TransformPoint(filter.sharedMesh.vertices[filter.sharedMesh.triangles[i + 1]]));

                triangleDatas.v2 = World2ScreenPoint(filter.transform.TransformPoint(filter.sharedMesh.vertices[filter.sharedMesh.triangles[i + 2]]));

                motionTDatas.TriangleDetails = triangleDatas;
                motionTList.Add(motionTDatas);

            }
        }

        return motionTList;
    }
    public Vector2 World2ScreenPoint(Vector3 world)
    {
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(world);
        screenPoint.y = (float)Screen.height - screenPoint.y;
        return screenPoint;
    }
}

[System.Serializable]
public class JsonVehicleDatas
{
    public int ID;
    public int TYPE;
    //public JsonVehicleBbox BboxDetails;
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
