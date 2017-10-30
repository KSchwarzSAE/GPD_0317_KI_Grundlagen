using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPoint : MonoBehaviour, IComparable<PathPoint>
{

    public PathPoint[] Reachable;

    [HideInInspector]
    // Kosten um diesen Punkt zu erreichen
    public float g;

    [HideInInspector]
    // Geschätzen Kosten bis zum Ziel
    public float h;

    [HideInInspector]
    // c = g + h
    public float c;

    [HideInInspector]
    // Vorgängerpunkt hierhin
    public PathPoint prior;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 0.5f);

        foreach (var reachable in Reachable)
        {
            Gizmos.DrawLine(transform.position, reachable.transform.position);
        }
    }

    public int CompareTo(PathPoint other)
    {
        if (other.c > c)
            return -1;

        if (other.c < c)
            return 1;

        return 0;
    }

}
