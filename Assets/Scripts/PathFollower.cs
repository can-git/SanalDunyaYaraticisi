using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    public Transform nodes;
    private GameObject[] PathNode;
    [SerializeField] float moveSpeed = 2f;
    int waypointIndex = 0;

    void Awake()
    {
        PathNode = new GameObject[nodes.childCount];
        for (int i = 0; i < nodes.childCount; i++)
        {
            PathNode[i] = nodes.GetChild(i).gameObject;
        }
    }
    private void Start()
    {
        transform.position = PathNode[waypointIndex].transform.position;
    }
    void Update()
    {
        Move();
    }
    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, PathNode[waypointIndex].transform.position, moveSpeed * Time.deltaTime);
        if (transform.position == PathNode[waypointIndex].transform.position)
        {
            waypointIndex += 1;
        }
        if (waypointIndex == PathNode.Length)
        {
            //FindObjectOfType<ColorSetter>().deleteColorID(this.GetComponent<ChangeColor>().colorID);
            Destroy(gameObject);
        }
    }
}
