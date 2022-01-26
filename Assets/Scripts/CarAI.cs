using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using System.Linq;
using UnityEditor;

public class CarAI : MonoBehaviour
{
    PathCreator pathCreator;
    float speed = 5;
    float distanceTravalled;
    bool isNormal;

    private void Awake()
    {
        isNormal = GameObject.Find("Config").GetComponent<JsonThings>().isSceneNormal;
    }

    void Start()
    {
        if (isNormal)
        {
            Destroy(GetComponent<CarDetails>());
            Destroy(GetComponent<ChangeColor>());
        }
        else
        {
            Destroy(GetComponent<Transform>().GetChild(1).gameObject);
        }

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
