using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    // [x] 1. Center the camera on player
    // [x] 2. Move the camera around the pivot
    // [x] 3. Smooth?
    // [ ] 4. Prevent clipping through walls
    // [ ] 5. Variable distance
    // [ ] 6. Prevent not showing player

    [SerializeField] private float cameraDistance = 15.0f;
    [SerializeField] private Transform cameraAnchor;

    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Start()
    {
        // Sets the container position
        transform.position = cameraAnchor.position;
        
        // Offsets the camera
        cam.transform.localPosition = new Vector3(0, 0, -cameraDistance);
    }

    private void Update()
    {
        RotateCamera();
        FollowAnchor();
    }

    #region Camera Rotation

    [Header("Camera Rotation")]
    
    [SerializeField] private float minDegreesX = -89f;
    [SerializeField] private float maxDegreesX = 89f;
    [SerializeField, Range(0.1f, 10f)] private float sensitivity = 5.0f;
    [SerializeField, Range(0f, 5f)] private float smoothness = 2f;
    
    [Space(10)]

    private float targetRotY;
    private float targetRotX;

    private void RotateCamera()
    {
        // Stores the rotation
        targetRotY += Input.GetAxis("Mouse X") * sensitivity;
        targetRotX -= Input.GetAxis("Mouse Y") * sensitivity;

        // Clamps the rotation on X axis
        targetRotX = Mathf.Clamp(targetRotX, minDegreesX, maxDegreesX);

        Quaternion targetRotation = Quaternion.Euler(Vector3.up * targetRotY + Vector3.right * targetRotX);
        
        // Smoothness applied
        if (smoothness > 0) transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, (10 - smoothness) * Time.deltaTime);
        
        // Without smoothness
        else transform.rotation = targetRotation;
    }

    #endregion

    #region Camera Follow

    [Header("Camera Rotation")]
    
    [SerializeField, Range(1f, 20f)] private float cameraSpeed = 10.0f;

    private void FollowAnchor()
    {
        // Without smoothness
        // transform.position = cameraAnchor.position;
        
        // With smoothness
        // IMPORTANT: when lerping the camera position, the Rigidbody's interpolation setting must be set to Interpolate
        // or Extrapolate, otherwise will cause a jittering effect on the camera
        transform.position = Vector3.Lerp(transform.position, cameraAnchor.position, cameraSpeed * Time.deltaTime);
    }

    #endregion
}
