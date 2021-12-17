using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Spawner : MonoBehaviour
{
    PathCreator pathCreator;
    
    [Header("Set car object from prefabs and its speed independently")]
    public CarInit[] cars;
    private GameObject newCar;

    [Header("")]
    public float secondsBetweenSpawn;

    void Awake()
    {
        pathCreator = this.gameObject.transform.GetComponent<PathCreator>();
    }

    IEnumerator Start()
    {
        createDestroyer();

        foreach (CarInit car in cars)
        {  
            yield return new WaitForSeconds(secondsBetweenSpawn);
            Spawn(car.car, car.speed);
        }
    }
    
    void Spawn(GameObject car, int speed)
    {
        Vector3 spawnPosition = this.transform.position;
        newCar = Instantiate(car);
        newCar.transform.parent = this.gameObject.transform;
        newCar.transform.position = pathCreator.path.GetPoint(0);
        //newCar.GetComponent<CarAI>().setSpeed(speed);
    }
    void createDestroyer()
    {
        GameObject destroyer = new GameObject("Destroyer");
        
        destroyer.transform.position = pathCreator.path.GetPoint(pathCreator.path.NumPoints - 1);
        destroyer.transform.parent = this.gameObject.transform;

        destroyer.AddComponent<BoxCollider>();
        destroyer.GetComponent<BoxCollider>().isTrigger = true;

        destroyer.AddComponent<Rigidbody>();
        destroyer.GetComponent<Rigidbody>().useGravity = false;
    }
}

[System.Serializable]
public class CarInit
{
    public GameObject car;
    public int speed;
}
