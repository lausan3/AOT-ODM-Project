using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliding : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform playerObj;
    Rigidbody rb;
    PlayerController pc;

    [Header("Sliding")]
    public float maxSlideTime;
    public float slideForce;
    float slideTimer;

    public float slideYScale;
    float startYScale;

    [Header("Input")]
    public KeyCode slideKey = KeyCode.LeftControl;
    float horizontalInput;
    float verticalInput;

    private void Start()
    {
        pc = playerObj.GetComponent<PlayerController>();
        rb = playerObj.GetComponent<Rigidbody>();
        startYScale = playerObj.localScale.y;
    }

    private void Update() 
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // if sprinting and presses down slide key, start slide
        // NOTE: state checks if it's air because sprinting toggle won't work properly
        if (pc.state == PlayerController.MovementState.air && Input.GetKeyDown(slideKey) && (horizontalInput > 0)) {
            StartSlide();
        }

        if (Input.GetKeyUp(slideKey) && pc.sliding) {
            StopSlide();
        }
    }

    private void FixedUpdate() 
    {
        if (pc.sliding) {
            SlidingMovement();
        }
    }

    void StartSlide()
    {
        pc.sliding = true;

        playerObj.localScale = new Vector3(playerObj.localScale.x, slideYScale, playerObj.localScale.z);
        rb.AddForce(Vector3.down * 5F, ForceMode.Impulse);

        slideTimer = maxSlideTime;
    }

    void SlidingMovement()
    {
        Vector3 inputDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // normal sliding - if not on slope or if going up a slope
        if (!pc.OnSlope() || rb.velocity.y > -0.1f) {
            rb.AddForce(inputDirection.normalized * slideForce, ForceMode.Force);

            slideTimer -= Time.deltaTime;
        }

        // sliding down a slope
        else 
        {
            rb.AddForce(pc.GetSlopeMoveDirection(inputDirection) * slideForce, ForceMode.Force);
        }

        if (slideTimer <= 0) {
            StopSlide();
        }
    }

    void StopSlide()
    {
        pc.sliding = false;

        if (pc.ValidUncrouch())
        {
            playerObj.localScale = new Vector3(playerObj.localScale.x, startYScale, playerObj.localScale.z);
        }
    }
}
