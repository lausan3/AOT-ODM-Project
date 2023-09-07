using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public GameObject ropePrefab;
    
    #region Joint
    private RopeController rc;

    private HingeJoint joint;
    private GameObject connectedRope;
    #endregion
    

    private void Start()
    {
        joint = GetComponent<HingeJoint>();
        rc = GetComponentInParent<RopeController>();
    }

    // Create a new rope segment by instantiating it
    public void CreateNextRopeSegment()
    {
        GameObject nextRopeSegment = Instantiate(ropePrefab, transform.up, transform.rotation);
        
        // Update ropes list
        rc.ropes.Add((nextRopeSegment.transform));
        
        // Joint magic
        connectedRope = nextRopeSegment;
        joint.anchor = connectedRope.transform.position;
    }
}
