using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    // IDEA: line renderer renders lines (maybe at fixed lengths of ropesegment length so it looks like a rope) constantly from hookPoint
    // to this hook
    [Header("Rope Renderer")] 
    public int ropeQuality = 20;
    private LineRenderer lr;

    private Vector3 startPoint;
    [HideInInspector] public bool hitSurface = false;

    private void Start()
    {
        startPoint = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        hitSurface = true;
    }

    private void DrawRope()
    {
        lr.positionCount = ropeQuality;
        Vector3 hookPos = transform.position;
        float segmentLength = Vector3.Distance(startPoint, hookPos) / ropeQuality;
        
        for (int i = 0; i < ropeQuality; i++)
        {
            Vector3 lastSegmentPoint = lr.GetPosition(i);
            // Next segment position is the position of one segment length away from last segment
            Vector3 segmentPoint = new Vector3();
            lr.SetPosition(i, segmentPoint);
        }
    }
}
