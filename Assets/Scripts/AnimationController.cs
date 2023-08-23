using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Transform player;
    private Animator anim;
    private PlayerController pc;
    private Rigidbody rb;

    [HideInInspector] public bool jumping = false;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = player.GetComponent<Rigidbody>();
        pc = player.GetComponent<PlayerController>();
        anim = player.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // running check
        if (pc.state == PlayerController.MovementState.sprinting)
        {
            anim.SetBool("Walking", true);
        }
        else
        {
            anim.SetBool("Walking", false);
        }
        
        // jumping check
        anim.SetBool("Jumping", jumping);
        
        // falling set
        if (jumping && pc.state == PlayerController.MovementState.air)
        {
            anim.SetBool("Falling", true);
        }
        else
        {
            anim.SetBool("Falling", false);
        }
        
        // speed set
        anim.SetFloat("Input X", pc.horizontalInput);
        anim.SetFloat("Input Z", pc.verticalInput);
    }
}
