using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Grappling : MonoBehaviour
{
    [Header("Hooking")]
    public float hookRange = 100f;
    public LayerMask hookable;
    public Transform hookPoint, camera, player;
    public RawImage crosshair;
    private SpringJoint joint;
    private Vector3 grapplePoint;
    private bool isGrappling;
    private bool grappleActive;
    
    [Header("Joint Mod")]
    public float jointSpringForce;
    public float jointDamperForce;
    public float jointMassScale;

    [Header("Keybinds")] 
    public KeyCode grappleDirectionKey;

    public KeyCode grappleKey = KeyCode.Mouse3;
    public KeyCode reelKey = KeyCode.Mouse4;
    
    // Start is called before the first frame update
    void Start()
    {
        isGrappling = false;
        grappleActive = false;
        
        hookPoint = transform.GetChild(1).GetComponent<Transform>();
        camera = GameObject.FindWithTag("Camera").transform;
        player = GameObject.FindWithTag("Player").transform;
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
        
        // Toggle grapple with grapple key held and grapple button (default thumb button front)
        if ((Input.GetKey(grappleDirectionKey) && Input.GetKeyDown(grappleKey)) && !isGrappling)
        {
            StartCoroutine(Grapple(0.2f));
        } 
        // Toggle ungrapple with grapple key held and grapple button (default thumb button front)
        else if (isGrappling)
        {
            Ungrapple();
        }
        // Reeling input: grapple key held down and set reel button (default thumb button back)
        else if ()
        {
            
        }
    }

    // This code is inspired by Dani's YouTube tutorial on grapple guns.
    // TODO: Currently can grapple with same key multiple times.
    IEnumerator Grapple(float waitTime)
    {
        isGrappling = true;
        
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, hookRange))
        {
            grappleActive = true;
            
            grapplePoint = hit.point;
            
            // Grapple joint
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);
            
            // Joint modification code:
            joint.spring = jointSpringForce;
            joint.damper = jointDamperForce;
            joint.massScale = jointMassScale;
        }
        
        yield return new WaitForSeconds(waitTime);
        
        isGrappling = false;
    }

    // Also inspired by Dani's YouTube tutorial on grapple guns.
    void DrawRope()
    {
        // If not grappling, don't draw rope
        if (!joint) return;

        
    }
    
    // Ungrapple, unhook joint
    void Ungrapple()
    {
        Destroy(joint);
    }

    // Reeling is done by pressing the side you want to reel and holding the button you set (default thumb button back)
    // Implementation wise, we need to do three things, push the player forwards, readjust the joint, and fix the line renderer.
    // For each FixedUpdate() call, we do these:
    // Push player forwards: push the player forwards with a force strength
    // Readjust joint: reset the joint anchor to the hook point and our current position
    // Fix line renderer: reset the line renderer to hook point and our current position
    void Reel()
    {
        
    }
}
