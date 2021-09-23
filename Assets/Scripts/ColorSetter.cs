using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSetter : MonoBehaviour
{
    [SerializeField] public Material outlineMask;
    [SerializeField] public Material outlineFill;

    private ArrayList colorIDsBus;
    private ArrayList colorIDsCar;
    private ArrayList colorIDsLorry;
    private ArrayList colorIDsTruck;
    private ArrayList colorIDsVan;

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
                index = 0;
                break;
            case "Car":
                index = 1;
                break;
            case "Lorry":
                index = 2;
                break;
            case "Truck":
                index = 3;
                break;
            case "Van":
                index = 4;
                break;
            default:
                index = 5;
                break;
        }
        return index;
    }

    public int newColorID(string carType)
    {
        int index=255;
        switch (carType)
        {
            case "Bus":
                index = 255;
                while (index > 0)
                {
                    if (!colorIDsBus.Contains(index))
                    {
                        colorIDsBus.Add(index);
                        break;
                    }
                    else
                    {
                        index--;
                    }
                }
                break;
            case "Car":
                index = 255;
                while (index > 0)
                {
                    if (!colorIDsCar.Contains(index))
                    {
                        colorIDsCar.Add(index);
                        break;
                    }
                    else
                    {
                        index--;
                    }
                }
                break;
            case "Lorry":
                index = 255;
                while (index > 0)
                {
                    if (!colorIDsLorry.Contains(index))
                    {
                        colorIDsLorry.Add(index);
                        break;
                    }
                    else
                    {
                        index--;
                    }
                }
                break;
            case "Truck":
                index = 255;
                while (index > 0)
                {
                    if (!colorIDsTruck.Contains(index))
                    {
                        colorIDsTruck.Add(index);
                        break;
                    }
                    else
                    {
                        index--;
                    }
                }
                break;
            case "Van":
                index = 255;
                while (index > 0)
                {
                    if (!colorIDsVan.Contains(index))
                    {
                        colorIDsVan.Add(index);
                        break;
                    }
                    else
                    {
                        index--;
                    }
                }
                break;
            default:
                break;
        }
        return index;
    }
}
