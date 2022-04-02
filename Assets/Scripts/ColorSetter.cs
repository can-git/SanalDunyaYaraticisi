using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ColorSetter : MonoBehaviour
{
    private ArrayList colorIDsBus;
    private ArrayList colorIDsCar;
    private ArrayList colorIDsLorry;
    private ArrayList colorIDsTruck;
    private ArrayList colorIDsVan;

    public GameObject city;
    public Material material;
    // public Material defaultMaterial;
    public Material treeMaterial;
    public Material[] objects;

    private Terrain terrain;
    
    void Start()
    {
        terrain = city.GetComponentInChildren<Terrain>();
        if (city.GetComponentInChildren<Terrain>() != null)
        {
            foreach (Material item in objects)
            {
                item.color = material.color;
            }

            terrain.materialTemplate = material;
        }
        
        
        foreach (Renderer rend in city.GetComponentsInChildren<Renderer>())
        {
            if (rend)
            {
                Material[] mats = new Material[rend.materials.Length];
                for (int i = 0; i < rend.materials.Length; i++)
                {
                    if(rend.gameObject.CompareTag("Tree"))
                        mats[i] = treeMaterial;
                    else
                        mats[i] = material;
                }
                rend.materials = mats;
            }
        }
    }
    // private void OnApplicationQuit()
    // {
    //     foreach (Material item in objects)
    //     {
    //         item.color = defaultMaterial.color;
    //     }

    // }

    private void OnValidate()
    {
        colorIDsBus = new ArrayList();
        colorIDsCar = new ArrayList();
        colorIDsLorry = new ArrayList();
        colorIDsTruck = new ArrayList();
        colorIDsVan = new ArrayList();
    }
    
    public int newCarTypeID(string carType)
    {
        int index;
        switch (carType)
        {
            case "Bus":
            index = 1;
            break;
            case "Car":
            index = 2;
            break;
            case "Lorry":
            index = 3;
            break;
            case "Truck":
            index = 4;
            break;
            case "Van":
            index = 5;
            break;
            default:
            index = 6;
            break;
        }
        return index;
    }

    public int newColorID(string carType)
    {
        int index = 1;
        switch (carType)
        {
            case "Bus":
            index = 1;
            while (index < 255)
            {
                if (!colorIDsBus.Contains(index))
                {
                    colorIDsBus.Add(index);
                    break;
                }
                else
                {
                    index++;
                }
            }
            break;
            case "Car":
            index = 1;
            while (index < 255)
            {
                if (!colorIDsCar.Contains(index))
                {
                    colorIDsCar.Add(index);
                    break;
                }
                else
                {
                    index++;
                }
            }
            break;
            case "Lorry":
            index = 1;
            while (index < 255)
            {
                if (!colorIDsLorry.Contains(index))
                {
                    colorIDsLorry.Add(index);
                    break;
                }
                else
                {
                    index++;
                }
            }
            break;
            case "Truck":
            index = 1;
            while (index < 255)
            {
                if (!colorIDsTruck.Contains(index))
                {
                    colorIDsTruck.Add(index);
                    break;
                }
                else
                {
                    index++;
                }
            }
            break;
            case "Van":
            index = 1;
            while (index < 255)
            {
                if (!colorIDsVan.Contains(index))
                {
                    colorIDsVan.Add(index);
                    break;
                }
                else
                {
                    index++;
                }
            }
            break;
            default:
            break;
        }
        return index;
    }
}
