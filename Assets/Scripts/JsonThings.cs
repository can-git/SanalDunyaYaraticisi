using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using UnityEditor.Recorder;
using UnityEditor.Recorder.Input;
using UnityEngine.SceneManagement;
using System;
using System.Collections;


public class JsonThings : MonoBehaviour
{
    private bool isWindows = true;
    public bool isWriteable = true;

    public bool isSceneNormal = true;
    string currentFolderName;

    List<JsonVehicleDatas> list;

    //List<JsonDatas> genelList;
    //JsonDatas jsonData;

    JsonDictionary jsonData;

    //public int waitForFrame = 0;
    private RecorderControllerSettings controllerSettings;
    private RecorderController TestRecorderController;
    int num = 0;
    [Header("Recording Config")]
    string scene_name;
    private string database = "";
    private string triangleFileName = "";
    public int frameRate = 30;
    public Vector2Int ScreenSize;
    public int recordStart = 0;
    public int recordEnd = 10;

    public Camera camera;

    private void Awake()
    {
        if (SystemInfo.operatingSystem.Contains("Windows"))
        {
            triangleFileName = "\\Triangles";
            database = "C:\\Users\\CAN\\Desktop\\Python Workspace\\HomographyT1001\\Datasets";
            isWindows = true;
        }
        else
        {
            triangleFileName = @"/Triangles";
            database = "/home/can/Desktop/UnityResources/HomographyT1001/Datasets";
            isWindows = false;
        }
        recordEnd = recordEnd + 4;
        Application.targetFrameRate = 30;
        scene_name = SceneManager.GetActiveScene().name;
        if (!isSceneNormal)
        {
            Destroy(GameObject.Find("Day and Night Controller"));
            Destroy(GameObject.Find("Lights Controller"));
            currentFolderName = "Masks";
            //genelList = new List<JsonDatas>();
            jsonData = new JsonDictionary();
        }
        else
        {
            Destroy(GetComponent<ColorSetter>());
            currentFolderName = "Images";
        }
    }
    private void Start()
    {
        createFolders();
        StartRecorder();
        num = recordStart;
        //isLoaded = true;

    }

    private void Update()
    {
        //if (isLoaded)
        CallItEveryTime();
    }

    private void CallItEveryTime()
    {
        if (Time.frameCount >= recordStart + 3 && Time.frameCount <= recordEnd)
        {
            Debug.Log(num);
            if(isWriteable)
                Process();
            num++;
        }
        else if (Time.frameCount > recordEnd + 1)
        {
            End();
        }
    }

    public void Process()
    {
        if (!isSceneNormal)
        {
            jsonData.data.Add(num, getCarDatas());
            //jsonData = new JsonDatas();
            //jsonData.Frame = num;
            ////jsonData.CameraVector = gameObject.transform.GetComponent<Velocity>().getVelocity();
            //jsonData.Vehicles = getCarDatas();
            //genelList.Add(jsonData);
        }
    }

    public List<JsonVehicleDatas> getCarDatas()
    {
        list = new List<JsonVehicleDatas>();
        foreach (var item in FindObjectsOfType<CarDetails>())
        {
            if (item.isVisibleViaCollider(camera, item.gameObject))
            {
                list.Add(item.getCarDetails());
            }
        }
        return list;
    }

    private void createFolders()
    {
        Directory.CreateDirectory(Path.Combine(database, scene_name));
        Directory.CreateDirectory(Path.Combine(database, scene_name, currentFolderName));
    }

    void StartRecorder()
    {
        controllerSettings = ScriptableObject.CreateInstance<RecorderControllerSettings>();
        TestRecorderController = new RecorderController(controllerSettings);
        Debug.Log("Record Started.");
        var imageRecorder = ScriptableObject.CreateInstance<ImageRecorderSettings>();
        imageRecorder.name = scene_name + "_" + currentFolderName;
        imageRecorder.Enabled = true;
        imageRecorder.CaptureAlpha = false;
        imageRecorder.RecordMode = RecordMode.SingleFrame;
        imageRecorder.OutputFormat = ImageRecorderSettings.ImageRecorderOutputFormat.PNG;
        if (isWindows)
            imageRecorder.OutputFile = Path.Combine(database, scene_name) + "\\" + currentFolderName + "\\<Frame>";
        else
            imageRecorder.OutputFile = Path.Combine(database, scene_name) + "/" + currentFolderName + "/<Frame>";
        imageRecorder.imageInputSettings = new CameraInputSettings
        {
            Source = ImageSource.MainCamera,
            RecordTransparency = true,
            CaptureUI = false,
            FlipFinalOutput = false,
            OutputWidth = ScreenSize.x,
            OutputHeight = ScreenSize.y,
        };
        controllerSettings.SetRecordModeToFrameInterval(recordStart, recordEnd - 3);
        controllerSettings.AddRecorderSettings(imageRecorder);
        controllerSettings.FrameRate = frameRate;

        RecorderOptions.VerboseMode = false;
        TestRecorderController.PrepareRecording();
        TestRecorderController.StartRecording();


    }

    void End()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        if(isWriteable){
            if (!isSceneNormal)
        {
            File.WriteAllText(Path.Combine(database, scene_name) + triangleFileName + ".json", JsonConvert.SerializeObject(jsonData.data, Formatting.None));
            Debug.Log(Time.frameCount);
        }
        }
        Debug.Log("Finished");
        Application.Quit();
    }
}

//[System.Serializable]
//public class JsonDatas
//{
//    public int Frame;
//    //public Vector3 CameraVector;
//    public List<JsonVehicleDatas> Vehicles;
//}
public class JsonDictionary
{
    public Dictionary<int, List<JsonVehicleDatas>> data;

    public JsonDictionary()
    {
        data = new Dictionary<int, List<JsonVehicleDatas>>();
    }
}
