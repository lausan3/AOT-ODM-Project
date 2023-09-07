using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Grappling : MonoBehaviour
{
    [Header("Hooking")]
    public float hookRange = 100f;
    public LayerMask hookable;
    public Transform hookPoint, camera, player;
    public RawImage crosshair;
    private RopeController rope;
    private Vector3 grapplePoint;
    private bool grappling;

    [Header("Keybinds")] 
    public KeyCode grappleKey;
    
    // Start is called before the first frame update
    void Start()
    {
        grappling = false;
        
        hookPoint = transform.GetChild(1).GetComponent<Transform>();
        camera = GameObject.FindWithTag("Camera").transform;
        player = GameObject.FindWithTag("Player").transform;
        rope = GetComponent<RopeController>();
    }
    
    void FixedUpdate()
    {
        DrawRope();
        
        // Change color of crosshair to red when object is within range
        if (Physics.Raycast(camera.position, camera.forward, hookRange))
        {
            crosshair.color = Color.red;
        } else
        {
            crosshair.color = Color.black;
        }
        
        // Toggle grapple.
        if (Input.GetKeyDown(grappleKey) && !grappling)
        {
            StartCoroutine(Grapple(0.2f));
        }
    }

    // This code is inspired by Dani's YouTube tutorial on grapple guns.
    // TODO: Currently can grapple with same key multiple times.
    IEnumerator Grapple(float waitTime)
    {
        grappling = true;
        
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, hookRange))
        {
            rope.IncreaseRopeSegments();
            
            // grapplePoint = hit.point;
            
            // Grapple joint
            // joint = player.gameObject.AddComponent<SpringJoint>();
            // joint.autoConfigureConnectedAnchor = false;
            // joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);
            
            // Joint modification code:
            // joint.spring = jointSpringForce;
            // joint.damper = jointDamperForce;
            // joint.massScale = jointMassScale;
        }
        
        yield return new WaitForSeconds(waitTime);
        
        grappling = false;
    }

    // Also inspired by Dani's YouTube tutorial on grapple guns.
    void DrawRope()
    {
        // If not grappling, don't draw rope
        // if (!joint) return;

    }
}
