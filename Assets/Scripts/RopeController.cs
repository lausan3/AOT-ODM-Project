using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RopeController : MonoBehaviour
{
     [HideInInspector] 
     public List<Transform> ropes;

     public float ropeCheckRange = 0.5f;

     [Serialize] private Transform rootRope;

     [Header("Hinge Strength Mod")]
     public float springForce = 4.5f;
     public float spring = 7f;

     private void Start()
     {
          rootRope = transform.GetChild(2);
     }

     // Increase ropes by calling last rope segment's CreateNextRopeSegment() function.
     public void IncreaseRopeSegments()
     {
          Debug.Log("Called IncreaseRopeSegments()");
          if (ropes.Count > 0)
          {
               Rope lastSegment = ropes[ropes.Count - 1].GetComponent<Rope>();
               lastSegment.CreateNextRopeSegment();
          }
          else
          {
               rootRope.GetComponent<Rope>().CreateNextRopeSegment();
          }
     }
     
     // Remove last rope segment by destroying it.
     public void DecreaseRopeSegments() 
     {
          if (ropes.Count > 0)
          {
               Destroy(ropes[ropes.Count - 1]);
          }
     }
}