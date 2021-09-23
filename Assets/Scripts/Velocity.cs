using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Velocity : MonoBehaviour
{
    Vector3 pos, velocity;
    void Start()
    {
        pos = transform.position;
    }

    void Update()
    {
        velocity = (transform.position - pos) / Time.deltaTime;
        pos = transform.position;
        //Debug.Log(Camera.main.WorldToScreenPoint(this.transform.position).z);
    }

    public bool IsInView()
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        var objCollider = GetComponent<SphereCollider>();
        if (GeometryUtility.TestPlanesAABB(planes, objCollider.bounds))
        {
            if (Physics.Linecast(Camera.main.transform.position, objCollider.bounds.center, out var hit))
            {
                if (hit.transform.position == this.transform.position)
                {
                    //Debug.DrawLine(Camera.main.transform.position, transform.position, Color.red, 0);
                    //Debug.DrawLine(objCollider.bounds.center, pos-velocity, Color.cyan, 0);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        return false;
    }

    public Vector3 getVelocity()
    {
        return velocity;
    }
}
