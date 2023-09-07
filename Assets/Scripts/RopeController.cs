using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RopeController : MonoBehaviour
{
     [HideInInspector] 
     public List<Transform> ropes;

     [Header("Hinge Strength Mod")]
     public float springForce = 4.5f;
     public float spring = 7f;
     
     void IncreaseRopeSegments()
     {
          Rope lastSegment = ropes[ropes.Count - 1].GetComponent<Rope>();
          lastSegment.CreateNextRopeSegment();
     }
     
     // Remove last rope segment by destroying it.
     void DecreaseRopeSegments()
     {
          Destroy(ropes[ropes.Count - 1]);
     }
}