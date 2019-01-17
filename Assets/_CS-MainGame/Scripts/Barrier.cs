using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform tailPoint;
    public Transform headPoint;
    public Collider2D col;
    public float distance()
    {
        return Mathf.Abs(tailPoint.position.y - headPoint.position.y);
    }

    public void turnOffCol()
    {
        col.enabled = false;
    }
}
