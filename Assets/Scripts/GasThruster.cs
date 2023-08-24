using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasThruster : MonoBehaviour
{
    public KeyCode thrustKey = KeyCode.Mouse1;
    public float thrustStrength = 7f;
    public Transform player;
    private Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = player.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(thrustKey))
        {
            Thrust();
            Debug.Log("Thrusting!");
        }
    }

    private void Thrust()
    {
        Vector3 dir = Vector3.forward.normalized;
        
        // TODO: maybe need to change with camera direction instead for playability
        rb.AddForce(dir * thrustStrength, ForceMode.Force);
    }
}
