using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlug : MonoBehaviour
{
    public float spdWalk = 0.15f;
    public float spdRun = 1.0f;
    public float spdBooster = 2.0f;
    public float spdDampTime = 0.1f;
    public bool flagJumping;
    private int ckGrounded;
    public float jumpHeight = 1.5f;
    public float jumpFrontForce = 10f;
    public float spdMouse, spdSeeker;
    private bool flagColliding;
    private CapsuleCollider capsuleCollider;
    private Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = transform;
        capsuleCollider = GetComponent<CapsuleCollider>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
