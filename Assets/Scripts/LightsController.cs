﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsController : MonoBehaviour
{
    float currentTime;
    DayAndNightControl dayController;

    void Start()
    {
        dayController = FindObjectOfType<DayAndNightControl>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = dayController.getCurrentTime();
        UpdateEnvLights();
    }

    void UpdateEnvLights()
    {
        if (currentTime <= 0.3 || 0.9f <= currentTime)
        {
            Debug.Log(this.transform.GetComponentInChildren<Light>());
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