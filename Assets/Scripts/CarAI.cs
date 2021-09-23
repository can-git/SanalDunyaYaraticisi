using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using System.Linq;

public class CarAI : MonoBehaviour
{
    PathCreator pathCreator;
    float speed = 5;
    float distanceTravalled;

    void Start()
    {
        if (transform.parent)
            pathCreator = this.transform.parent.gameObject.GetComponent<PathCreator>();
    }

    private void Update()
    {
        if (pathCreator)
        {
            distanceTravalled += speed * Time.deltaTime;
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravalled);
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravalled);
        }

    }

    public void setSpeed(int speed)
    {
        this.speed = speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
