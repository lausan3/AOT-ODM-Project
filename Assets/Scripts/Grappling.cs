using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Grappling : MonoBehaviour
{
    [Header("Hooking")]
    public float hookRange = 100f;
    public LayerMask hookable;
    public Transform hookPoint, camera, player;
    private Vector3 grapplePoint;
    private LineRenderer lr;
    private SpringJoint joint;
    private bool grappling;
    
    [Header("Joint Mod")]
    public float jointSpringForce = 4.5f;
    public float jointDamperForce = 7f;
    public float jointMassScale = 4.5f;

    [Header("Keybinds")] 
    public KeyCode grappleKey;
    
    // Start is called before the first frame update
    void Start()
    {
        grappling = false;
        
        lr = GetComponent<LineRenderer>();
        hookPoint = transform.GetChild(1).GetComponent<Transform>();
        camera = GameObject.FindWithTag("Camera").transform;
        player = GameObject.FindWithTag("Player").transform;
    }
    
    void FixedUpdate()
    {
        // toggle grapple
        if (Input.GetKeyDown(grappleKey) && !grappling)
        {
            StartCoroutine(Grapple(0.2f));
        }
    }

    // This code is inspired by Dani's YouTube tutorial on grapple guns. Stopped at 5:17
    // Currently can grapple with same key multiple times.
    IEnumerator Grapple(float waitTime)
    {
        grappling = true;
        
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, hookRange))
        {
            grapplePoint = hit.point;
            
            // Grapple joint
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);
            
            // The distance grapple will try to keep from the grapple point.
            joint.spring = jointSpringForce;
            joint.damper = jointDamperForce;
            joint.massScale = jointMassScale;
        }
        
        yield return new WaitForSeconds(waitTime);

        grappling = false;
    }
}
