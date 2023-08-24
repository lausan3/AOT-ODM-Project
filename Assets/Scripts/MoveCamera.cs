using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform cameraPosition;
    // Update is called once per frame
    void Update()
    {
        // camera moves with camera position object. which moves with player
        transform.position = cameraPosition.position;
    }
}
