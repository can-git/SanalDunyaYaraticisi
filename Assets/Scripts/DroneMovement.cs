using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMovement : MonoBehaviour
{
    PathCreator pathCreator;
    public float speed = 5;
    float distanceTravalled;

    void Start()
    {
        pathCreator = this.transform.parent.gameObject.GetComponent<PathCreator>();
        this.gameObject.transform.position = pathCreator.path.GetPoint(0);
    }

    private void Update()
    {
        distanceTravalled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravalled);
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravalled);
    }
}
