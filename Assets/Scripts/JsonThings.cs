using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class JsonThings : MonoBehaviour
{
    List<JsonVehicleDatas> list;
    List<JsonDatas> genelList;
    JsonDatas jsonData;
    int num = -1;
    bool isLoaded = false;

    private void Awake()
    {

        genelList = new List<JsonDatas>();
    }

    private void Update()
    {
        if (isLoaded)
        {
            num++;
            WriteJson();
        }
    }

    private void OnGUI()
    {
        if (!isLoaded)
            isLoaded = true;
    }

    public void WriteJson()
    {
        jsonData = new JsonDatas();
        jsonData.Frame = num;
        jsonData.CameraVector = gameObject.transform.GetComponent<Velocity>().getVelocity();
        jsonData.Vehicles = getCarDatas();
        genelList.Add(jsonData);
    }

    public List<JsonVehicleDatas> getCarDatas()
    {
        list = new List<JsonVehicleDatas>();
        foreach (var item in FindObjectsOfType<CarDetails>())
        {
            //Debug.Log(item.getMotionDatas().Count);
            //if (item.getMotionDatas().Count > 0)
            //{
            //    list.Add(item.getCarDetails());
            //}
            list.Add(item.getCarDetails());
        }
        return list;
    }

    private void OnApplicationQuit()
    {
        File.WriteAllText(Application.dataPath + "/VehiclesJsonData" + ".json", JsonConvert.SerializeObject(genelList, Formatting.Indented));
    }
}

[System.Serializable]
public class JsonDatas
{
    public int Frame;
    public Vector3 CameraVector;
    public List<JsonVehicleDatas> Vehicles;
}
