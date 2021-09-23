using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectSetter : MonoBehaviour
{
    GameObject objToSpawn;
    List<Vector3> list;
    Vector3[] filter;

    public MeshFilter[] meshFilters;

    void Start()
    {
        if (meshFilters.Length == 0)
        {
            meshFilters = GetComponentsInChildren<MeshFilter>();
        }
        PreparePointWithAll();
    }

    void PreparePointWithAll()
    {
        foreach (var meshFilter in meshFilters)
        {
            filter = meshFilter.mesh.vertices;

            Vector3[] list = filter.Distinct().ToArray();
            foreach (var item in list)
            {
                objToSpawn = new GameObject("Vertice Of " + this.GetComponent<ChangeColor>().colorID);
                objToSpawn.AddComponent<Velocity>();
                //objToSpawn.AddComponent<SpriteRenderer>();
                objToSpawn.AddComponent<SphereCollider>();
                objToSpawn.GetComponent<SphereCollider>().radius = 0.01f;
                objToSpawn.transform.parent = meshFilter.transform;
                objToSpawn.transform.position = meshFilter.transform.position + item;
            }
        }
    }
}
