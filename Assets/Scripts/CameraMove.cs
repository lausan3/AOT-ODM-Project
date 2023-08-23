using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform playerObj;

    // Update is called once per frame
    void Update()
    {
        transform.position = playerObj.position;
    }
}
