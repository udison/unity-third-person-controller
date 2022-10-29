using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Camera cam;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    void Update()
    {
        HandleMovement();
    }

    #region Movement

    [SerializeField] private float acceleration = 80.0f;
    [SerializeField] private float maxVelocity = 10.0f;
    
    private Vector3 motion = Vector3.zero;

    void HandleMovement()
    {
        motion.x = Input.GetAxis("Horizontal");
        motion.z = Input.GetAxis("Vertical");

        rb.velocity += motion * acceleration * Time.deltaTime;
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
    }

    #endregion
}
