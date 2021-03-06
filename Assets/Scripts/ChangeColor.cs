using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[DisallowMultipleComponent]

public class ChangeColor : MonoBehaviour
{
    private Renderer[] renderers;
    private MeshFilter[] filters;

    public int carTypeID = 0;
    public int colorID = 0;
    private string carType;
    Color color4Default;
    Color color4Outline;

    public Material material;

    void Start()
    {
        if (!GameObject.Find("Config").GetComponent<JsonThings>().isSceneNormal)
        {
            renderers = GetComponentsInChildren<Renderer>();
            carType = gameObject.tag;

            foreach (Renderer rend in renderers)
            {
                var mats = new Material[rend.materials.Length];
                for (int i = 0; i < rend.materials.Length; i++)
                {
                    mats[i] = material;
                }
                rend.materials = mats;
            }

            if (carType == null)
                carType = "Car";

            carTypeID = FindObjectOfType<ColorSetter>().newCarTypeID(carType);
            colorID = FindObjectOfType<ColorSetter>().newColorID(carType);

            color4Default = new Color32(0, (byte)carTypeID, (byte)colorID, 255);
            // color4Outline = new Color32(1, (byte)carTypeID, (byte)colorID, 255);

            foreach (var renderer in renderers)
            {
                foreach (var item in renderer.materials)
                {
                    item.SetColor("_Color", color4Default);
                    // item.SetColor("_OutlineColor", color4Outline);
                }
            }
        }
        
    }
    public int getColorID()
    {
        return colorID;
    }
    public int getTypeID()
    {
        return carTypeID;
    }
}
