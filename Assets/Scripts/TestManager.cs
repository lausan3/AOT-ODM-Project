using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    public Transform testDummy;
    public float forceAmount;
    public KeyCode forceActivateKey;
    private Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = testDummy.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(forceActivateKey)) 
        {
            rb.AddForce(testDummy.forward * forceAmount, ForceMode.Force);
        }
    }
}
