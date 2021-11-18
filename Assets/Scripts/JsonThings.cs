using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using UnityEditor.Recorder;
using UnityEditor.Recorder.Input;

public class JsonThings : MonoBehaviour
{
    List<JsonVehicleDatas> list;
    List<JsonDatas> genelList;
    JsonDatas jsonData;
    int num = 0;
    bool isLoaded = false;
    private RecorderControllerSettings controllerSettings;
    private RecorderController TestRecorderController;

    [Header("Recording Config")]
    public string scene_name = "aa3";
    public string path = "D:\\Dosyalar\\Bi_alametler\\Dataset\\Recordings\\Datasets\\";
    public int frameRate = 30;
    public int Width = 1920;
    public int Height = 1080;
    public int frameStart = 0;
    public int frameEnd = 5;



    private void Awake()
    {
        genelList = new List<JsonDatas>();
        controllerSettings = ScriptableObject.CreateInstance<RecorderControllerSettings>();
        TestRecorderController = new RecorderController(controllerSettings);
        StartRecorder();
    }

    private void Update()
    {
        if (isLoaded)
        {
            print(num);
            if (num == frameEnd)
            {
                StopRecorder();
            }
            WriteJson();
            num++;
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
        //jsonData.CameraVector = gameObject.transform.GetComponent<Velocity>().getVelocity();
        jsonData.Vehicles = getCarDatas();
        genelList.Add(jsonData);
    }

    public List<JsonVehicleDatas> getCarDatas()
    {
        list = new List<JsonVehicleDatas>();
        foreach (var item in FindObjectsOfType<CarDetails>())
        {
            list.Add(item.getCarDetails());
        }
        return list;
    }


    void StartRecorder()
    {
        var imageRecorder = ScriptableObject.CreateInstance<ImageRecorderSettings>();
        imageRecorder.name = "SingleFrameRecorder";
        imageRecorder.Enabled = true;
        imageRecorder.CaptureAlpha = false;
        imageRecorder.RecordMode = RecordMode.SingleFrame;
        imageRecorder.OutputFormat = ImageRecorderSettings.ImageRecorderOutputFormat.PNG;
        imageRecorder.OutputFile = path + scene_name + "\\Masks\\<Frame>";

        imageRecorder.imageInputSettings = new CameraInputSettings
        {
            Source = ImageSource.MainCamera,
            RecordTransparency = true,
            CaptureUI = false,
            FlipFinalOutput = false,
            OutputWidth = Width,
            OutputHeight = Height,
        };


        controllerSettings.SetRecordModeToFrameInterval(frameStart, frameEnd);
        controllerSettings.AddRecorderSettings(imageRecorder);
        controllerSettings.FrameRate = frameRate;

        RecorderOptions.VerboseMode = false;
        TestRecorderController.PrepareRecording();
        TestRecorderController.StartRecording();
    }

    void StopRecorder()
    {
        TestRecorderController.StopRecording();
        File.WriteAllText("D:/Dosyalar/Bi_alametler/Dataset/Recordings/Datasets/" + scene_name + "/VehiclesJsonData" + ".json", JsonConvert.SerializeObject(genelList, Formatting.Indented));
        UnityEditor.EditorApplication.isPlaying = false;
        Debug.Log("Recording is Finished");
        Application.Quit();
    }
}

[System.Serializable]
public class JsonDatas
{
    public int Frame;
    //public Vector3 CameraVector;
    public List<JsonVehicleDatas> Vehicles;
}
