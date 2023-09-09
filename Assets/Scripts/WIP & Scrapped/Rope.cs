using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public GameObject ropePrefab;

    private Transform camera;
    
    #region Joint
    private RopeController rc;

    private HingeJoint joint;
    private GameObject connectedRope;
    #endregion
    

    private void Start()
    {
        camera = GameObject.FindWithTag("Camera").transform;
        rc = GetComponentInParent<RopeController>();
    }

    // Create a new rope segment by instantiating it
    public void CreateNextRopeSegment()
    {
        // Check for empty space in front of it
        if (Physics.Raycast(transform.up, camera.forward, rc.ropeCheckRange))
        {
            
            GameObject nextRopeSegment = Instantiate(ropePrefab, transform.up, transform.localRotation);
            
            // Update ropes list
            rc.ropes.Add((nextRopeSegment.transform));
            
            // Joint magic
            joint = gameObject.AddComponent<HingeJoint>();
            connectedRope = nextRopeSegment;
            joint.anchor = connectedRope.transform.position;
        }
    }
}
