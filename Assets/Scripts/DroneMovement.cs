﻿using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMovement : MonoBehaviour
{
    PathCreator pathCreator;
    public float speed = 5;
    float distanceTravalled;

    private void Awake()
    {
        Application.targetFrameRate = 30;
    }

    void Start()
    {
        pathCreator = this.transform.parent.gameObject.GetComponent<PathCreator>();
        this.gameObject.transform.position = pathCreator.path.GetPoint(0);
    }

    private void Update()
    {
        this.transform.GetChild(0).transform.Rotate(0, 0, .1f);
        distanceTravalled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravalled);
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravalled);
    }
}
