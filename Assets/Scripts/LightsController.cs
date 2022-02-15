using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsController : MonoBehaviour
{
    float currentTime;
    DayAndNightControl dayController;
    

    private void Awake()
    {
        dayController = FindObjectOfType<DayAndNightControl>();
    }

    void Update()
    {
        if (dayController.lightsOpen)
        {
            currentTime = 1f;
        }
        else
        {
            currentTime = 0.5f;
        }
        //currentTime = dayController.getCurrentTime();
        UpdateEnvLights();
    }

    void UpdateEnvLights()
    {
        if (currentTime <= 0.3 || 0.9f <= currentTime)
        {
            foreach (Light item in this.transform.GetComponentsInChildren<Light>())
            {
                item.enabled = true;
            }
        }
        else
        {
            foreach (Light item in this.transform.GetComponentsInChildren<Light>())
            {
                item.enabled = false;
            }
        }
    }
}
